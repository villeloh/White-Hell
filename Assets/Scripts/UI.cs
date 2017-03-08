using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

	//kyseessä on tekstit ja painikkeet joissa tulee lukemaaan eri arvot ruudun ylälaidassa.
	private bool sounds = true;
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
	Text inventorySpace;
	Text ammoPouch;
	Text wearing;
	Text quitText;
	Button quitPrompt;
	Button quitNo;
	Button quitYes;
	Button menuButton;
	Button closeActualMenu;
	Button soundSwitch;
	public PlayerStats PlayerStats;
	public GameTime GameTime;
	public PlayerMove PlayerMove;
	public UI_Sound UI_Sound;
	Image menuBg;

	void Start ()
	{
		//haetaan eri gameobjectit
		//Tekstit
		currentHunger = GameObject.Find ("HungerMeter").GetComponent<Text> ();
		currentCold = GameObject.Find ("ColdMeter").GetComponent<Text> ();
		currentResistance = GameObject.Find ("Resistance").GetComponent<Text> ();
		radioParts = GameObject.Find ("RadioParts").GetComponent<Text> ();
		kello = GameObject.Find ("KELLO").GetComponent<Text> ();
		quitText = GameObject.Find ("QuitText").GetComponent<Text> ();
		wearing = GameObject.Find ("Wearing").GetComponent<Text> ();
		ammoPouch = GameObject.Find ("AmmoPouch").GetComponent<Text> ();
		inventorySpace = GameObject.Find ("InventorySpace").GetComponent<Text> ();
		menuBg = GameObject.Find ("MenuBg").GetComponent<Image> ();
		//painikkeet
		eatFood = GameObject.Find ("EatingButton").GetComponent<Button> ();
		eatSeagul = GameObject.Find ("EatSeagulMeat").GetComponent<Button> ();
		eatSeal = GameObject.Find ("EatSealMeat").GetComponent<Button> ();
		eatWalrus = GameObject.Find ("EatWalrusMeat").GetComponent<Button> ();
		eatFox = GameObject.Find ("EatArcticFoxMeat").GetComponent<Button> ();
		eatPolarBear = GameObject.Find ("EatPolarBearMeat").GetComponent<Button> ();
		closeMenu = GameObject.Find ("CloseMenu").GetComponent<Button> ();
		quitPrompt = GameObject.Find ("QuitPrompt").GetComponent<Button> ();
		quitYes = GameObject.Find ("QuitYes").GetComponent<Button> ();
		quitNo = GameObject.Find ("QuitNo").GetComponent<Button> ();
		menuButton = GameObject.Find ("MenuButton").GetComponent<Button> ();
		closeActualMenu = GameObject.Find ("CloseActualMenu").GetComponent<Button> ();
		soundSwitch = GameObject.Find ("SoundSwitch").GetComponent<Button> ();
		//painikkeiden tekstit
		SeagullMeatText = GameObject.Find ("SeagullMeatText").GetComponent<Text> ();
		SealMeatText = GameObject.Find ("SealMeatText").GetComponent<Text> ();
		WalrusMeatText = GameObject.Find ("WalrusMeatText").GetComponent<Text> ();
		FoxMeatText = GameObject.Find ("ArcticFoxMeatText").GetComponent<Text> ();
		PolarBearMeatText = GameObject.Find ("PolarBearMeatText").GetComponent<Text> ();
		//pelin alussa seuraavat gameobjectit eivät ole näkyvissä
		closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
		inventorySpace.gameObject.SetActive (false);
		ammoPouch.gameObject.SetActive (false);
		wearing.gameObject.SetActive (false);
		quitNo.gameObject.SetActive (false);
		quitYes.gameObject.SetActive (false);
		quitText.gameObject.SetActive (false);
		quitPrompt.gameObject.SetActive (false);
		menuBg.gameObject.SetActive (false);
		soundSwitch.gameObject.SetActive (false);
		closeActualMenu.gameObject.SetActive (false);
		eatFood.onClick.AddListener (() => openVisibility (eatFood)); //metodilla muut painikkeet näkyviin ja eatFood pois näkyvistä
		closeMenu.onClick.AddListener (() => closeVisibility (closeMenu)); //metodilla painikkeet pois näkyvistä ja eatFood näkyviin
		//onclick metodit eri lihojen syöntiin (+äänet syödessä lihaa)
		eatSeagul.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seagull Meat")));
		eatSeagul.onClick.AddListener (() => UI_Sound.PlayEatSound ());
		eatSeal.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seal Meat")));
		eatSeal.onClick.AddListener (() => UI_Sound.PlayEatSound ());
		eatWalrus.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Walrus Meat")));
		eatWalrus.onClick.AddListener (() => UI_Sound.PlayEatSound ());
		eatFox.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Arctic Fox Meat")));
		eatFox.onClick.AddListener (() => UI_Sound.PlayEatSound ());
		eatPolarBear.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Polar Bear Meat")));
		eatPolarBear.onClick.AddListener (() => UI_Sound.PlayEatSound ());
		//onClickit pelin lopetus nappuloille
		quitPrompt.onClick.AddListener (() => askToQuit (quitPrompt));
		quitNo.onClick.AddListener (() => noToQuit (quitNo));
		quitYes.onClick.AddListener (() => yesToQuit (quitYes));
		menuButton.onClick.AddListener (() => openMenu (menuButton));
		closeActualMenu.onClick.AddListener (() => backToGame (closeActualMenu));
		soundSwitch.onClick.AddListener (() => switchSounds (soundSwitch));
	}

	void openVisibility (Button eatFood)
	{

		UI_Sound.PlayClickSound ();

		PlayerMove.StopMove ();
		Debug.Log ("painoit nappia " + eatFood);
        

		eatFood.gameObject.SetActive (false);
		closeMenu.gameObject.SetActive (true);
		eatSeagul.gameObject.SetActive (true);
		eatFox.gameObject.SetActive (true);
		eatSeal.gameObject.SetActive (true);
		eatWalrus.gameObject.SetActive (true);
		eatPolarBear.gameObject.SetActive (true);
		inventorySpace.gameObject.SetActive (true);
		ammoPouch.gameObject.SetActive (true);
		wearing.gameObject.SetActive (true);
		menuButton.gameObject.SetActive (false);
		//PlayerMove.AllowMove = true;
		Debug.Log (PlayerMove.AllowMove);
	}

	void closeVisibility (Button closemenu)
	{
		UI_Sound.PlayClickSound ();

		eatFood.gameObject.SetActive (true);
		closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
		inventorySpace.gameObject.SetActive (false);
		ammoPouch.gameObject.SetActive (false);
		wearing.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (true);
		PlayerMove.AllowMove = true;
	}

	void openMenu (Button menuButton)
	{

		UI_Sound.PlayClickSound ();

		PlayerMove.StopMove ();
		menuBg.gameObject.SetActive (true);
		quitPrompt.gameObject.SetActive (true);
		soundSwitch.gameObject.SetActive (true);
		closeActualMenu.gameObject.SetActive (true);
		eatFood.gameObject.SetActive (false);
	}

	void backToGame (Button closeActualMenu)
	{

		UI_Sound.PlayClickSound ();
		eatFood.gameObject.SetActive (true);
		menuBg.gameObject.SetActive (false);
		quitPrompt.gameObject.SetActive (false);
		soundSwitch.gameObject.SetActive (false);
		closeActualMenu.gameObject.SetActive (false);
		PlayerMove.AllowMove = true;
	}

	void askToQuit (Button quitPrompt)
	{
		UI_Sound.PlayClickSound ();

		quitNo.gameObject.SetActive (true);
		quitText.gameObject.SetActive (true);
		quitYes.gameObject.SetActive (true);
		soundSwitch.gameObject.SetActive (false);
		closeActualMenu.gameObject.SetActive (false);
		quitPrompt.gameObject.SetActive (false);
	}

	void noToQuit (Button quitNo)
	{
		UI_Sound.PlayClickSound ();

		quitNo.gameObject.SetActive (false);
		quitText.gameObject.SetActive (false);
		quitYes.gameObject.SetActive (false);
		soundSwitch.gameObject.SetActive (true);
		closeActualMenu.gameObject.SetActive (true);
		quitPrompt.gameObject.SetActive (true);
	}

	//aplikaation sammuttaminen
	void yesToQuit (Button quitYes)
	{
		Debug.Log ("quitting");
		Application.Quit ();
	}

	//äänten vaihtaminen
	void switchSounds (Button soundSwitch)
	{

		UI_Sound.PlayClickSound ();

		if (sounds == true) {
			sounds = false;
			Debug.Log ("äänet =" + sounds);
			AudioListener.volume = 0.0f;
		} else {
			sounds = true;
			Debug.Log ("äänet =" + sounds);
			AudioListener.volume = 1.0f;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (PlayerStats.CarriedFood >= 0) {
			inventorySpace.text = "Inventory: " + PlayerStats.CarriedFood;
		} else if (PlayerStats.CarriedFood >= PlayerStats.MaxCarriedFood) {
			inventorySpace.text = "Inventory is full!";
		}

		if (PlayerStats.CarriedAmmo >= 0) {
			ammoPouch.text = "Ammo pouch: " + PlayerStats.CarriedAmmo.ToString () + "/" + PlayerStats.MaxCarriedAmmo.ToString ();
		} else if (PlayerStats.CarriedAmmo >= PlayerStats.MaxCarriedAmmo) {
			inventorySpace.text = "Ammo pouch is full";
		}
		wearing.text = "Currently wearing: " + PlayerStats.GetItemName (PlayerStats.CurrentCoat);
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
