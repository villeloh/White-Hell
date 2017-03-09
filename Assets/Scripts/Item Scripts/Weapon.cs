using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for making 'Weapon' items.
/// </summary>

public class Weapon : Item
{

/*
 * Used for making 'Weapon' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private int damage;

    /// <summary>
    /// Create a new Item of type 'Weapon', with a 'damage' parameter.
    /// </summary>
    public Weapon (int givenDamage)
	{
		this.damage = givenDamage;
	}

	// Get and set 'damage' from outside the class.
	public int Damage {
		get { return damage; }
		set { damage = value; }
	}

}
