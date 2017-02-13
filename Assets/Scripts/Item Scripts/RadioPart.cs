using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPart : Item
{

	private int partNumber;

	// create new Item of type RadioPart
	public RadioPart (int givenNumber)
	{
		this.partNumber = givenNumber;
	}

	// get and set partNumber from outside the class
	public int PartNumber {
		get { return partNumber; }
		set { partNumber = value; }
	}

}