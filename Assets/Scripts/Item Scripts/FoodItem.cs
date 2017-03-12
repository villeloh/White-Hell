using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for making 'FoodItem' items.
/// </summary>

public class FoodItem : Item
{

/*
 * Used for making 'FoodItem' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private float eatValue;

    /// <summary>
    /// Create a new Item of type 'FoodItem', which has an eatValue parameter.
    /// </summary>
    public FoodItem (float givenEatValue)
	{
		this.eatValue = givenEatValue;
	}

	// Allows an item's eatValue to be accessed from outside the class.
	public float EatValue {
		get { return eatValue; }
		set { eatValue = value; }
	}

}
