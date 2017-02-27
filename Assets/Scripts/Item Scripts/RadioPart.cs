using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPart : Item
{

/*
 * Used for making 'RadioPart' items (regular objects, not GameObjects).
 * Attached to: nothing
 * Author: Ville Lohkovuori
 */

    private int partNumber;

	// Create new Item of type 'RadioPart'.
	public RadioPart (int givenNumber)
	{
		this.partNumber = givenNumber;
	}

	// Get and set 'partNumber' from outside the class.
	public int PartNumber {
		get { return partNumber; }
		set { partNumber = value; }
	}

}