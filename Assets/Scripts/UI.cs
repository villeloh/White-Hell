using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

	//kyseessä on tekstit ja painikkeet joissa tulee lukemaaan eri arvot ruudun ylälaidassa.
	Text currentHunger;
	Text currentCold;
	Text currentResistance;
	Button eatFood;
	Button eatSeagul;
	Button eatSeal;
	Button eatWalrus;
	Button eatFox;
	Button eatPolarBear;
	Button closeMenu;
	public PlayerStats PlayerStats;




	void Start ()
	{
		//haetaan eri gameobjectit
		currentHunger = GameObject.Find ("HungerMeter").GetComponent<Text> ();
		currentCold = GameObject.Find ("ColdMeter").GetComponent<Text> ();
		currentResistance = GameObject.Find ("Resistance").GetComponent<Text> ();
		eatFood = GameObject.Find ("EatingButton").GetComponent<Button> ();
		eatSeagul = GameObject.Find ("EatSeagulMeat").GetComponent<Button> ();
		eatSeal = GameObject.Find ("EatSealMeat").GetComponent<Button> ();
		eatWalrus = GameObject.Find ("EatWalrusMeat").GetComponent<Button> ();
		eatFox = GameObject.Find ("EatArcticFoxMeat").GetComponent<Button> ();
		eatPolarBear = GameObject.Find ("EatPolarBearMeat").GetComponent<Button> ();
		closeMenu = GameObject.Find ("CloseMenu").GetComponent<Button> ();
		//pelin alussa seuraavat painikkeet eivät ole näkyvissä
		closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
		eatFood.onClick.AddListener (() => openVisibility (eatFood)); //metodilla muut painikkeet näkyviin ja eatFood pois näkyvistä
		closeMenu.onClick.AddListener (() => closeVisibility (closeMenu)); //metodilla painikkeet pois näkyvistä ja eatFood näkyviin


	}

	void openVisibility (Button eatFood)
	{
		Debug.Log ("painoit nappia " + eatFood);


		eatFood.gameObject.SetActive (false);
		closeMenu.gameObject.SetActive (true);
		eatSeagul.gameObject.SetActive (true);
		eatFox.gameObject.SetActive (true);
		eatSeal.gameObject.SetActive (true);
		eatWalrus.gameObject.SetActive (true);
		eatPolarBear.gameObject.SetActive (true);
	}
	void closeVisibility (Button closemenu) {

			eatFood.gameObject.SetActive (true);
			closeMenu.gameObject.SetActive (false);
			eatSeagul.gameObject.SetActive (false);
			eatFox.gameObject.SetActive (false);
			eatSeal.gameObject.SetActive (false);
			eatWalrus.gameObject.SetActive (false);
			eatPolarBear.gameObject.SetActive (false);


	}

	 
	// Update is called once per frame
	void Update ()
	{
		currentHunger.text = PlayerStats.Hunger + "/100"; //näyttää nykyisen nälkäarvon 
		currentCold.text = PlayerStats.Cold + "/100"; //näyttää nykyisen kylmyysarvon
		currentResistance.text = PlayerStats.CurrentCoat.ColdResistance + ""; //näyttää nykysen kylmyydenkestävyyden


	}
}
