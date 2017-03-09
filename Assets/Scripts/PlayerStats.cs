using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStats : MonoBehaviour
{

	/* 
     * Stores all the Player GameObject's statuses. 
     * Attached to: GameObject 'Player'.
     * Author: Ville Lohkovuori
     */

	// Needed for reference between classes in Unity.
	public PlayerMove PlayerMove;
	public GameTime GameTime;

	// Internal (and outer) reference variables for various stats.
	private float hunger = 0.0f;
	private float cold = 0.0f;
	private float deathCold = 100.0f;
	private float deathHunger = 100.0f;
	private bool polarBearDeath = false;
	private int carriedFood = 0;
	private int maxCarriedFood = 100;
	private int carriedAmmo;
	private int maxCarriedAmmo = 50;
	private int radioPartCount = 0;
	private Coat currentCoat;
	private Weapon currentWeapon;
	private Dictionary<Item, string> inventory;

	// For tracking the numbers of different food items that the player has.
	private int numberOfSeagullMeats = 0;
	private int numberOfPolarFoxMeats = 0;
	private int numberOfWalrusMeats = 0;
	private int numberOfSealMeats = 0;
	private int numberOfPolarBearMeats = 0;

	// These ints need to be constants because the switch-case thingy won't work otherwise.
	private const int seagullEatValue = 10;
	private const int polarFoxEatValue = 20;
	private const int sealEatValue = 30;
	private const int walrusEatValue = 40;
	private const int polarBearEatValue = 60;

	private bool removeFlag = false;

	private Weapon pistol;
	private Weapon rifle;

	// Stores the name of the player that is given via an input field, at game start.
	private string playerName = "Default";


	void Start ()
	{
		// Ensure that the Player object continues to exist in the next scene. This is needed because its stored info will need to be utilized in the OutroLogic script.
		// (A dedicated object for storing global values seems to be a standard policy when working with Unity. We'll remember it for the next project.)
		DontDestroyOnLoad (transform.gameObject);

		// Create the Player's inventory (NOTE: it stores REGULAR objects, not GameObjects).
		inventory = new Dictionary<Item, string> ();

		// Create initial coat, add it to inventory and make the player 'wear' it.
		Coat alkutakki = new Coat (20.0f);
		inventory.Add (alkutakki, "Old Anorak (20)");
		currentCoat = alkutakki;

		print ("Wearing: " + inventory [alkutakki]); // debug

		// Give the Player 20 rifle bullets to start with.
		carriedAmmo = 20;

		// Testing food item creation.
		AddToInv (new FoodItem (10), "Seagull Meat");

		pistol = new Weapon (1);
		rifle = new Weapon (3);
		currentWeapon = rifle;

	}

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   INVENTORY METHODS   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


	// The method to add items to inventory.
	// As typically only a small number of items will be created at once, imo it's not worth it to give a 'number' parameter to the method.
	// One can simply call the method x number of times, if multiple items need to be created.
	public void AddToInv (Item item, string itemName)
	{

		if (item is FoodItem) {

			inventory.Add ((FoodItem)item, itemName);
			FoodItemCheck ((FoodItem)item);

			// The 'numberOf' values are needed in UI.cs, for displaying the amount of different food items that the player is carrying.
			if (removeFlag == false) {
				switch (GetItemName ((FoodItem)item)) {

				case "Seagull Meat":

					numberOfSeagullMeats++;
					print ("number of seagull meat items + 1!"); // debug

					break;

				case "Arctic Fox Meat":

					numberOfPolarFoxMeats++;

					break;

				case "Seal Meat":

					numberOfSealMeats++;

					break;

				case "Walrus Meat":

					numberOfWalrusMeats++;

					break;

				case "Polar Bear Meat":

					numberOfPolarBearMeats++;

					break;
				}
			}
		} else if (item is Coat) {
			inventory.Add (item, itemName + " (" + ((Coat)item).ColdResistance + ")");
			CoatCheck ((Coat)item);
		}

		//IMPORTANT NOTE: As it is, you cannot add items to inv other than Coats and FoodItems! Separate conditions for Weapons will 
		// be added if needed. As for RadioParts, they will be handled as a simple int, since they have no other usable attributes 
		// than the number (all text relating to them can be handled in the quest popups).

	}

	// Removes item from inventory.
	public void RemoveFromInv (Item item)
	{
		inventory.Remove (item);
	}

	// Makes the player eat a specified food item.
	public void EatFoodItem (FoodItem meat)
	{
		// The null check is needed for when the method is called from UI.cs (since the click is always possible, regardless if you have any food items or not).
		// The hunger check makes eating impossible if hunger is already zero, preventing the player from wasting any meat items.
		if (meat != null && hunger > 0) {
			print ("hunger ennen syöntiä:" + hunger); // debug

			// Added a check to ensure that hunger won't go below zero under any circumstances.
			if (hunger >= meat.EatValue) {
				hunger -= meat.EatValue;
			} else if (hunger < meat.EatValue) {
				hunger = 0.0f;
			}

			// even without any checks, carriedFood should never go below zero, because it has previously been increased by the same amount (when adding the item to inventory)
			carriedFood -= meat.EatValue;

			switch (GetItemName (meat)) {

			case "Seagull Meat":

				numberOfSeagullMeats--;
				print ("number of seagull meat items - 1!"); // debug

				break;

			case "Arctic Fox Meat":

				numberOfPolarFoxMeats--;

				break;

			case "Seal Meat":

				numberOfSealMeats--;

				break;

			case "Walrus Meat":

				numberOfWalrusMeats--;

				break;

			case "Polar Bear Meat":

				numberOfPolarBearMeats--;

				break;
			}

			RemoveFromInv (meat);

			print ("hunger syönnin jälkeen: " + hunger); // debug
		}
	}

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   HELPER METHODS   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


	// Called upon adding new food items to inventory. Makes it so that if the picking up of a new food item would put the Player
	// over the max carryable food amount, the 'extra' food gets substracted from the item's eatValue stat. As a further check,
	// if the procedure in question would result in the food item having an eatValue of zero, it gets removed from the inventory
	// instead. So, all the food items in the inventory will have an eatValue of at least one (1) at all times.

	// Made into its own method simply for clarity's sake.

	private void FoodItemCheck (FoodItem foodItem)
	{
		if ((carriedFood + foodItem.EatValue) > maxCarriedFood) {
			if (carriedFood == maxCarriedFood) {
				RemoveFromInv (foodItem);
				removeFlag = true;
				return;
			} else {
				foodItem.EatValue = maxCarriedFood - carriedFood;
			}
		}
		carriedFood += foodItem.EatValue;
	}

	// Similarly to FoodItemCheck(), this check is called after adding a Coat item to inventory. If the new Coat's coldResistance value
	// is bigger than the current Coat's, it replaces the current one as the 'active' coat. If not, it gets discarded instead.
	private void CoatCheck (Coat coat)
	{
		if (currentCoat.ColdResistance < coat.ColdResistance) {
			currentCoat = coat;
		} else {
			RemoveFromInv (coat);
		}
	}

	// Ensures that the player's carried amount of ammunition never rises above the designated max value. Called manually whenever ammo is increased (in Quests.cs).
	// Unlike FoodItemCheck() and CoatCheck(), this method is public because the ammo stat is a bare int and so the method cannot be 'concealed' within AddToInv().
	public void MaxAmmoCheck ()
	{
		if (carriedAmmo > maxCarriedAmmo) {
			carriedAmmo = maxCarriedAmmo;
		}
	}

	// Gets the name of the item from outside the class.
	// (I don't know the right syntax for a property in this context, so I made this into a regular method instead.)
	public string GetItemName (Item item)
	{
		return inventory [item];
	}

	// Returns the item (key) by its given name (value), from the inventory.
	// This procedure is apparently frought with considerable peril when it comes to Dictionaries.
	// Let's just hope that this thing works (I found it on the internet...), and get along, as time is of the essence.
	public FoodItem GetFoodItem (string itemName)
	{
		FoodItem key = (FoodItem)inventory.FirstOrDefault (x => x.Value == itemName).Key;
		return key;
	}

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

	/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   ACCESSIBLE PROPERTIES + UPDATE ()  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


	// Properties for accessing various values from outside the class (mainly in Triggerer.cs).
	public Coat CurrentCoat {
		get { return currentCoat; }
		set { currentCoat = value; }
	}

	public float Hunger {
		get { return hunger; }
		set { hunger = value; }
	}

	public float Cold {
		get { return cold; }
		set { cold = value; }
	}

	public float DeathCold {
		get { return deathCold; }
		set { deathCold = value; }
	}

	public float DeathHunger {
		get { return deathHunger; }
		set { deathHunger = value; }
	}

	public int CarriedAmmo {
		get { return carriedAmmo; }
		set { carriedAmmo = value; }
	}

	public int CarriedFood {
		get { return carriedFood; }
		set { carriedFood = value; }
	}

	public int RadioPartCount {
		get { return radioPartCount; }
		set { radioPartCount = value; }
	}

	public string PlayerName {
		get { return playerName; }
		set { playerName = value; }
	}

	public int NumberOfSeagullMeats {
		get { return numberOfSeagullMeats; }
		set { numberOfSeagullMeats = value; }
	}

	public int NumberOfPolarFoxMeats {
		get { return numberOfPolarFoxMeats; }
		set { numberOfPolarFoxMeats = value; }
	}

	public int NumberOfWalrusMeats {
		get { return numberOfWalrusMeats; }
		set { numberOfWalrusMeats = value; }
	}

	public int NumberOfSealMeats {
		get { return numberOfSealMeats; }
		set { numberOfSealMeats = value; }
	}

	public int NumberOfPolarBearMeats {
		get { return numberOfPolarBearMeats; }
		set { numberOfPolarBearMeats = value; }
	}

	public int MaxCarriedAmmo {
		get { return maxCarriedAmmo; }
		set { maxCarriedAmmo = value; }
	}

	public int MaxCarriedFood {
		get { return maxCarriedFood; }
		set { maxCarriedFood = value; }
	}

	public Weapon CurrentWeapon {
		get { return currentWeapon; }
		set { currentWeapon = value; }
	}

	public bool PolarBearDeath {
		get { return polarBearDeath; }
		set { polarBearDeath = value; }
	}


	void Update ()
	{
		// Increase the hunger and cold values with elapsed movement frames.
		// In addition, make it so that the cold value is affected by the coldResistance stat on the worn coat as well.
		if (PlayerMove.ClickFlag == true) {
			cold += 0.005f * (70.0f / currentCoat.ColdResistance); // 70.0f is the value of the best Coat, which should act to nullify the 'extra' effect of cold completely.
			hunger += 0.01f; // hunger grows at about half the speed that cold does, which is ok
		}

		// Set movement rate according to cold and hunger values (the first value is the initial rate, as hunger and cold are zero in the beginning).
		// The larger this value is, the slower the Player's movement speed becomes.
		PlayerMove.MoveDuration = (50.0f + cold + hunger);

		if (carriedAmmo == 0) {
			currentWeapon = pistol;
		}

		if (carriedAmmo > 0) {
			currentWeapon = rifle;
		}

	}

}
