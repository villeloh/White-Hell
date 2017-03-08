using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

/*
 * Used for making 'Weapon' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private int damage;

	// Create new Item of type 'Weapon'.
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
