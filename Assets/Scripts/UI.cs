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
	Text radioParts;
	Text kello;
	Button eatFood;
	Button eatSeagul;
	Button eatSeal;
	Button eatWalrus;
	Button eatFox;
	Button eatPolarBear;
	Button closeMenu;
	Text SeagullMeatText;
	Text SealMeatText;
	Text WalrusMeatText;
	Text FoxMeatText;
	Text PolarBearMeatText;
	public PlayerStats PlayerStats;
	public GameTime GameTime;

	void Start ()
	{
		//haetaan eri gameobjectit
		//Tekstit
		currentHunger = GameObject.Find ("HungerMeter").GetComponent<Text> ();
		currentCold = GameObject.Find ("ColdMeter").GetComponent<Text> ();
		currentResistance = GameObject.Find ("Resistance").GetComponent<Text> ();
		radioParts = GameObject.Find ("RadioParts").GetComponent<Text> ();
		kello = GameObject.Find ("KELLO").GetComponent<Text> ();
		//painikkeet
		eatFood = GameObject.Find ("EatingButton").GetComponent<Button> ();
		eatSeagul = GameObject.Find ("EatSeagulMeat").GetComponent<Button> ();
		eatSeal = GameObject.Find ("EatSealMeat").GetComponent<Button> ();
		eatWalrus = GameObject.Find ("EatWalrusMeat").GetComponent<Button> ();
		eatFox = GameObject.Find ("EatArcticFoxMeat").GetComponent<Button> ();
		eatPolarBear = GameObject.Find ("EatPolarBearMeat").GetComponent<Button> ();
		closeMenu = GameObject.Find ("CloseMenu").GetComponent<Button> ();
		//painikkeiden tekstit
		SeagullMeatText = GameObject.Find ("SeagullMeatText").GetComponent<Text> ();
		SealMeatText = GameObject.Find ("SealMeatText").GetComponent<Text> ();
		WalrusMeatText = GameObject.Find ("WalrusMeatText").GetComponent<Text> ();
		FoxMeatText = GameObject.Find ("ArcticFoxMeatText").GetComponent<Text> ();
		PolarBearMeatText = GameObject.Find ("PolarBearMeatText").GetComponent<Text> ();
		//pelin alussa seuraavat painikkeet eivät ole näkyvissä
		closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
		eatFood.onClick.AddListener (() => openVisibility (eatFood)); //metodilla muut painikkeet näkyviin ja eatFood pois näkyvistä
		closeMenu.onClick.AddListener (() => closeVisibility (closeMenu)); //metodilla painikkeet pois näkyvistä ja eatFood näkyviin
		//onclick metodit eri lihojen syöntiin
		eatSeagul.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seagull Meat")));
		eatSeal.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seal Meat")));
		eatWalrus.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Walrus Meat")));
		eatFox.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("ArcticFox Meat")));
		eatPolarBear.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Polarbear Meat")));
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

	void closeVisibility (Button closemenu)
	{

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

		radioParts.text = "Radio Parts: " + PlayerStats.RadioPartCount + "/5";
		currentHunger.text = "Hunger: " + Mathf.Round (PlayerStats.Hunger * 1f) / 1f + "/100"; //näyttää nykyisen nälkäarvon 
		currentCold.text = "Cold: " + Mathf.Round (PlayerStats.Cold * 1f) / 1f + "/100"; //näyttää nykyisen kylmyysarvon
		currentResistance.text = "Cold Res.: " + PlayerStats.CurrentCoat.ColdResistance + ""; //näyttää nykysen kylmyydenkestävyyden
		kello.text = "Day " + Mathf.Round (GameTime.GlobalTime * 1f) / 1f;
		// If lausekkeet painikkeiden tekstien muuttamiseen
		//1. lokinlihapainike
		if (PlayerStats.NumberOfSeagullMeats > 0) {
			SeagullMeatText.text = PlayerStats.NumberOfSeagullMeats + " x Seagull meat (10)";
		} else if (PlayerStats.NumberOfSeagullMeats == 0) {
			SeagullMeatText.text = "Out of seagull meat!";
		}
		//2. hylkeenlihapainike
		if (PlayerStats.NumberOfSealMeats > 0) {
			SealMeatText.text = PlayerStats.NumberOfSealMeats + " x Seal meat (30)";
		} else if (PlayerStats.NumberOfSealMeats == 0) {
			SealMeatText.text = "Out of seal meat!";
		}
		//3. mursunlihapainike
		if (PlayerStats.NumberOfWalrusMeats > 0) {
			WalrusMeatText.text = PlayerStats.NumberOfWalrusMeats + " x Walrus meat (40)";
		} else if (PlayerStats.NumberOfWalrusMeats == 0) {
			WalrusMeatText.text = "Out of Walrus meat!";
		}
		//4. naalinlihapainike
		if (PlayerStats.NumberOfPolarFoxMeats > 0) {
			FoxMeatText.text = PlayerStats.NumberOfPolarFoxMeats + " x Arctic fox meat (20)";
		} else if (PlayerStats.NumberOfPolarFoxMeats == 0) {
			FoxMeatText.text = "Out of Arctic fox meat!";
		}
		//5. jääkarhunlihapainike
		if (PlayerStats.NumberOfPolarBearMeats > 0) {
			PolarBearMeatText.text = PlayerStats.NumberOfPolarBearMeats + " x Polarbear meat (60)";
		} else if (PlayerStats.NumberOfPolarBearMeats == 0) {
			PolarBearMeatText.text = "Out of Polarbear meat!";
		}

	}
}
