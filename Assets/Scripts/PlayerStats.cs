using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// Needed for reference between classes in Unity.
	public PlayerMove PlayerMove;
	public GameTime GameTime;
    
	// Internal reference variables for various stats.
	private float hunger = 0.0f;
	private float cold = 0.0f;
	private float deathCold = 100.0f;
	private float deathHunger = 100.0f;
	private int carriedFood = 0;
	private int maxCarriedFood = 100;
	private int carriedAmmo;
	private int maxCarriedAmmo = 50;
	private int radioPartCount = 0;
	private Coat currentCoat;
	private Dictionary<Item, string> inventory;

	// Stores the name of the player that is given via an input field, at game start.
	private string playerName = "";

	// Stores the game score.
	private float score = 0;
    


	// Use this for initialization
	void Start ()
	{


		// create the Player's inventory (NOTE: it stores REGULAR objects, not GameObjects)
		inventory = new Dictionary<Item, string> ();

		// create initial coat, add it to inventory and make the player 'wear' it
		Coat alkutakki = new Coat (20.0f);
		inventory.Add (alkutakki, "perus anorakki (20)");
		FoodItem SeagullMeat = new FoodItem (20);
		AddToInv (SeagullMeat, "Seagull Meat");
		currentCoat = alkutakki;

		print ("Wearing: " + inventory [alkutakki]);

		// give the Player 20 rifle bullets to start with
		carriedAmmo = 20;

	}






	// The method to add items to inventory.
	// As typically only a small number of items will be created at once, imo it's not worth it to give a 'number' parameter to the method.
	// One can simply call the method for x number of times, if multiple items need to be created.

	// I originally tried to make this method also *create* the Item object, but that lead to a virtual horde of issues... Best to keep the creation process separate (for now at least).
	public void AddToInv (Item item, string itemName)
	{

		if (item is FoodItem) {

			inventory.Add (item, itemName + " (" + ((FoodItem)item).EatValue + ")");
			FoodItemCheck ((FoodItem)item);

		} else if (item is Coat) {
			inventory.Add (item, itemName + " (" + ((Coat)item).ColdResistance + ")");
			CoatCheck ((Coat)item);
		}

		//IMPORTANT NOTE: As it is, you cannot add items to inv other than Coats and FoodItems! Separate conditions for Weapons will 
		// be added if needed. As for RadioParts, they might be kept as a simple int, since they have no other usable attributes 
		// than the number (all text relating to them can be handled in the quest popups).

	}
    

	// Called upon adding new food items to inventory. Makes it so that if the picking up of a new food item would put the Player
	// over the max carryable food amount, the 'extra' food gets substracted from the item's eatValue stat. As a further check,
	// if the procedure in question would result in the food item having an eatValue of zero, it gets removed from the inventory
	// instead. So, all the food items in the inventory will have an eatValue of at least one (1) at all times.

	// Made into its own method simply for clarity's sake.
    
	private void FoodItemCheck (FoodItem foodItem)
	{
		if ((carriedFood + foodItem.EatValue) > maxCarriedFood) {
			if ((maxCarriedFood - carriedFood) != 0) {
				foodItem.EatValue = maxCarriedFood - carriedFood;
			} else {
				RemoveFromInv (foodItem);
			}
		}
		carriedFood += foodItem.EatValue;
	}

	// As far as I recall, this check already existed somewhere... I cannot find it, though, so I've made it into its own method, similarly to FoodItemCheck().
	// It's called after adding a Coat item to inventory. If the new Coat's ColdResistance value is bigger than the current Coat's, it replaces the current one as the 'active' coat.
	// If not, it gets discarded instead.
	private void CoatCheck (Coat coat)
	{
		if (currentCoat.ColdResistance < coat.ColdResistance) {
			currentCoat = coat;
		} else {
			RemoveFromInv (coat);
		}
	}

	//  Ensures that the player's carried amount of ammunition never rises above the designated max value. Called manually whenever ammo is increased (in Quests.cs).
	// Unlike FoodItemCheck() and CoatCheck(), this method is public because the ammo stat is a bare int and so the method cannot be 'concealed' within AddToInv().
	public void MaxAmmoCheck ()
	{ 
		if (carriedAmmo > maxCarriedAmmo) {
			carriedAmmo = maxCarriedAmmo;
		}
	}






	// Removes item from inventory
	public void RemoveFromInv (Item item)
	{
		inventory.Remove (item);
	}
		
	// Gets the name of the item from outside the class.
	// (I don't know the right syntax for a property in this context, so I made this into a regular method, instead.)
	public string GetItemName (Item item)
	{
		return inventory [item];
	}



	// Makes the player eat a specified food item.
	// The placement of this method is debatable. It alters a player stat, so I put it here, but it could be in Item.cs instead.
	public void EatFoodItem (FoodItem meat)
	{
		print ("hunger ennen syöntiä:" + hunger); // debug

		// Added a check to ensure that hunger won't go below zero under any circumstances.
		if (hunger >= meat.EatValue) {
			hunger -= meat.EatValue;
		} else if (hunger < meat.EatValue) {
			hunger = 0.0f;
		}

		carriedFood -= meat.EatValue; // carriedFoodin ei pitäisi koskaan mennä alle nollan, koska sitä lisätään aina ensin samalla määrällä (kun food item laitetaan inventoryyn)
		RemoveFromInv (meat);

		print ("hunger syönnin jälkeen: " + hunger); // debug
	}

	public void PlayerDeath ()
	{
		// ++ invoke different outros based on the cause of death (hunger, cold, polar bear). will be called from Triggerer.cs
	}


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



	// Update is called once per frame
	void Update ()
	{
		// Set hunger and cold values according to elapsed 'game time' (time-like value that increases with movement frames).
		// In addition, make it so that the cold value is affected by the ColdResistance stat on the worn coat as well.
		// cold += GameTime.GetGameTime * (60.0f / currentCoat.ColdResistance); // 60.0f is simply a constant that may be tweaked until optimal results are achieved.
		// hunger += 2.0f * GameTime.GetGameTime; // 2.0f = tweaking constant

		// Set movement rate according to cold and hunger values (the first value is the initial rate, as hunger and cold are zero in the beginning).
		// The larger this value is, the slower the Player's movement speed becomes.
		// This could also be done in PlayerMove.cs. It's a matter of taste where the logic is located.
		PlayerMove.MoveDuration = (50.0f + cold + hunger);

	}

}
