using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coat : Item
{

	private float coldResistance;

	// create new Item of type Coat
	public Coat (float givenRes)
	{
		this.coldResistance = givenRes;
	}

	// get and set coldResistance from outside the class
	public float ColdResistance {
		get { return coldResistance; }
		set { coldResistance = value; }
	}

}
