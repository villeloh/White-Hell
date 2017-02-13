using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	private int eatValue;

	public PlayerStats PlayerStats;


	public Item ()
	{
	}

	// For creating food items.
	public Item (int givenEatValue)
	{
		this.eatValue = givenEatValue;
	}

	// allows an item's eatValue to be accessed from outside the class (in PlayerStats.cs).
	public int EatValue {
		get { return eatValue; }
		set { eatValue = value; }
	}






}
