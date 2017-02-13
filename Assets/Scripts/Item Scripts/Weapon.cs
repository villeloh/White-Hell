using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

	private float damage;

	// create new Item of type Weapon
	public Weapon (float givenDamage)
	{
		this.damage = givenDamage;
	}

	// get and set damage from outside the class
	public float Damage {
		get { return damage; }
		set { damage = value; }
	}

}
