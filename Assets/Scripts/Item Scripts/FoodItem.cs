using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{

/*
 * Used for making 'FoodItem' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private int eatValue;

	// For creating food items.
	public FoodItem (int givenEatValue)
	{
		this.eatValue = givenEatValue;
	}

	// Allows an item's eatValue to be accessed from outside the class.
	public int EatValue {
		get { return eatValue; }
		set { eatValue = value; }
	}

}
