using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// needed for reference in Unity
	public PlayerMove PlayerMove;
	public GameTime GameTime;

	// stores the name that the player inputs for his/her character
	private string playerName;

	// internal reference variables for various stats
	private float hunger = 0.0f;
	private float cold = 0.0f;
	private float deathCold = 100.0f;
	private float deathHunger = 100.0f;
	private int carriedFood;
	private int maxCarriedFood = 100;
	private int carriedAmmo;
	private int maxCarriedAmmo = 50;
	private int radioPartCount = 0;
	private Coat currentCoat;
	private Dictionary<Item, string> inventory;

	// stores the game score
	private float score = 0;
    


	// Use this for initialization
	void Start ()
	{
		// ++ prompt for player name and store it in the UI object (UpperBar)

		// create the Player's inventory (NOTE: it stores REGULAR objects, not GameObjects)
		inventory = new Dictionary<Item, string> ();

		// create initial coat, add it to inventory and make the player 'wear' it
		Coat alkutakki = new Coat (20.0f);
		FoodItem liha = new FoodItem (10);
		AddToInv (alkutakki, "perus anorakki");
		AddToInv (liha, "lihaa!");
		currentCoat = alkutakki;
		print ("Wearing: " + inventory [alkutakki]);

		// give the Player 20 rifle bullets to start with
		carriedAmmo = 20;

		// give the Player zero food to start with
		carriedFood = 0;

	}

	// Methods for adding and removing items to inventory from outside the class. Also names the item when adding it.
	public void AddToInv (Item item, string itemName)
	{
		inventory.Add (item, itemName);

		// This condition makes it so that if the picking up of a new food item would put the Player over the max carryable food amount,
		// the 'extra' food gets substracted from the item's eatValue stat. As a further check, if the procudure in question would result 
		// in the food item having an eatValue of zero, it gets removed from the inventory instead. So, all food items in the inventory
		// will have an eatValue of at least one (1) at all times.
		if (item is FoodItem) {
			if ((carriedFood + ((FoodItem)item).EatValue) > maxCarriedFood) {
				if ((maxCarriedFood - carriedFood) != 0) {
					((FoodItem)item).EatValue = maxCarriedFood - carriedFood;
				} else {
					inventory.Remove (item);
				}
			}
			carriedFood += ((FoodItem)item).EatValue;
		}
	}

	// Removes item from inventory
	public void RemoveFromInv (Item item)
	{
		inventory.Remove (item);
	}
		
	// Gets the name of the item from outside the class.
	// (En tiedä propertyn oikeaa syntaksia tässä tapauksessa, joten siks tää on tavallinen metodi.)
	public string GetItemName (Item item)
	{
		return inventory [item];
	}

	/*

    // Method for killing animals, to be used in combat encounters. (May be relocated to a more suitable class, likely Animal.cs)
    // Animals should be GameObjects, since they will need to have a sprite and move on the shooting game's minimap
    // AnimalKill() will be called from Triggerer.cs once an animal's hitpoints reach zero
    public void AnimalKill (GameObject animal)
    {

        // Animals should have an 'eatValue' stat. This way, the meat item that is added to inventory upon animal kill can directly inherit the animal's eatValue.
        Item animalMeat = new Item(animal.EatValue); 
        AddToInv(animalMeat, "animal's meat"); // I'm not sure how to automate this to give the correct meat item name to each different type of killed animal...
                                               // Maybe a switch-case type of thingy might work, based on different tags for the types of killed animals?

        Destroy(animal); // done after the meat is added to inventory, to avoid a Null Reference Exception
        CloseEncounter (); // closes the minimap screen and returns to the main map view
        DisplayKillResultPopUp(); // displays a pop-up that tells you what you obtained from the kill and how much ammo you spent (goes away on click/tap).
                              // This could be done outside the AnimalKill() method, but as this will always result, I don't see why it can't be put here.

    }

    // Called from Triggerer.cs if the player presses 'Esc' upon (or during) an animal encounter.
    // Likewise, this should be moved to Animal.cs (once it exists)
    public void EscapeCombatEncounter ()
    {

        // Polar Bears may not be escaped! :)
        if (animal.tag != PolarBear) {
            Destroy(animal)
            CloseEncounter ();
            DisplayEscapePopUp (); // a simple message that you ran away, opting to preserve your bullets
        }
    }

    */

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
		inventory.Remove (meat);
		// meat = null; // esineet eivät näemmä katoa syödessä tästä huolimatta, mikä voi olla ongelma...

		print ("hunger syönnin jälkeen: " + hunger); // debug
	}

	public void PlayerDeath ()
	{
		// ++ invoke different outros based on the cause of death (hunger, cold, polar bear)
	}





	// getter and setter to access currentCoat from outside the class
	public Coat CurrentCoat {
		get { return currentCoat; }
		set { currentCoat = value; }
	}
		
	// Property that is used for getting and setting the hunger value from outside the class.
	public float Hunger {
		get { return hunger; }
		set { hunger = value; }
	}

	// Ditto for cold.
	public float Cold {
		get { return cold; }
		set { cold = value; }
	}

	// Property for accessing deathCold from outside the class (used in Triggerer.cs to tigger player's death from cold).
	public float DeathCold {
		get { return deathCold; }
		set { deathCold = value; }
	}

	// Property for accessing deathHunger from outside the class (used in Triggerer.cs to tigger player's death from hunger).
	public float DeathHunger {
		get { return deathHunger; }
		set { deathHunger = value; }
	}

	// Property for accessing the carriedAmmo stat from outside the class (by Triggerer.cs).
	// Ammo doesn't have to be an actual Item; it can simply be a variable whose count is altered.
	public int CarriedAmmo {
		get { return carriedAmmo; }
		set { carriedAmmo = value; }
	}

	// Property for accessing the carriedFood stat from outside the class (by Triggerer.cs).
	public int CarriedFood {
		get { return carriedFood; }
		set { carriedFood = value; }
	}

	// Property for accessing the radioPartCount stat from outside the class (by Triggerer.cs).
	public int RadioPartCount {
		get { return radioPartCount; }
		set { radioPartCount = value; }
	}




	// Update is called once per frame
	void Update ()
	{
		// Set hunger and cold values according to elapsed 'game time' (time-like value that increases with movement frames).
		// In addition, make it so that the cold value is affected by the ColdResistance stat on the worn coat as well.
		cold += GameTime.GetGameTime * (60.0f / currentCoat.ColdResistance); // 60.0f is simply a constant that may be tweaked until optimal results are achieved.
		hunger += 2.0f * GameTime.GetGameTime; // 2.0f = tweaking constant

		// Set movement rate according to cold and hunger values (the first value is the initial rate, as hunger and cold are zero in the beginning).
		// The larger this value is, the slower the Player's movement speed becomes.
		// This could also be done in PlayerMove.cs. It's a matter of taste where the logic located.
		PlayerMove.MoveDuration = (50.0f + cold + hunger);



	}

}
