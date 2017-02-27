using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coat : Item
{

/*
 * Used for making 'Coat' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private float coldResistance;

	// Create new Item of type 'Coat'.
	public Coat (float givenRes)
	{
		this.coldResistance = givenRes;
	}

	// Get and set coldResistance from outside the class.
	public float ColdResistance {
		get { return coldResistance; }
		set { coldResistance = value; }
	}

}
