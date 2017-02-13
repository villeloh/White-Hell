using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	//kyseessä on tekstit joissa tulee lukemaaan eri arvot ruudun ylälaidassa.
	Text currentHunger;
	Text currentCold;
	Text currentResistance;
	Button eatFood;
	public PlayerStats PlayerStats;


	void Start () {
		currentHunger = GameObject.Find ("HungerMeter").GetComponent<Text> ();
		currentCold = GameObject.Find ("ColdMeter").GetComponent<Text> ();
		currentResistance = GameObject.Find ("Resistance").GetComponent<Text> ();
		eatFood = GameObject.Find ("EatingButton").GetComponent<Button> ();
	}
	// Update is called once per frame
	void Update () {
		currentHunger.text = PlayerStats.Hunger  + "/100";
		currentCold.text = PlayerStats.Cold + "/100";
		currentResistance.text = PlayerStats.CurrentCoat.ColdResistance + "";
	}
}
