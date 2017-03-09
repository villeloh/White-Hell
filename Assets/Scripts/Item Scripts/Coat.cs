using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for making 'Coat' items.
/// </summary>

public class Coat : Item
{

/*
 * Used for making 'Coat' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private float coldResistance;

    /// <summary>
    /// Create a new Item of type 'Coat', which has a coldResistance parameter.
    /// </summary>
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
