using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for determining UI behaviour.
/// Author: Niko Eklund
/// </summary>

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
    Button eatTiger;

    Button shotgunButton;
    Button pistolButton;

	Button closeMenu;
	Text SeagullMeatText;
	Text SealMeatText;
	Text WalrusMeatText;
	Text FoxMeatText;
	Text PolarBearMeatText;
    Text TigerMeatText;
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
    Button LeaveCombatButton;
	public PlayerStats PlayerStats;
	public GameTime GameTime;
	public PlayerMove PlayerMove;
	public UI_Sound UI_Sound;
    public ShootLogic ShootLogic;
    public Hunt Hunt;
    public AnimalHandler AnimalHandler;
	Image menuBg;
    private Image shotgunImage;
    private Image pistolImage;
    public Sprite shotgunNotSelected;
    public Sprite pistolNotSelected;
    public Sprite shotgunSelected;
    public Sprite pistolSelected;

    public Image AmmoImage;
    public Image MeatImage;
    public Image CoatImage;
    public Image quitLabel;


    /// <summary>
    /// Assign the different GameObjects / components to variables. Alter the active status of certain GameObjects (for making them invisible).
    /// Add listeners for clicks on the menu buttons.
    /// </summary>
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
        eatTiger = GameObject.Find("EatTigerMeat").GetComponent<Button>();
        closeMenu = GameObject.Find ("CloseMenu").GetComponent<Button> ();
		quitPrompt = GameObject.Find ("QuitPrompt").GetComponent<Button> ();
		quitYes = GameObject.Find ("QuitYes").GetComponent<Button> ();
		quitNo = GameObject.Find ("QuitNo").GetComponent<Button> ();
		menuButton = GameObject.Find ("MenuButton").GetComponent<Button> ();
		closeActualMenu = GameObject.Find ("CloseActualMenu").GetComponent<Button> ();
		soundSwitch = GameObject.Find ("SoundSwitch").GetComponent<Button> ();
        LeaveCombatButton = GameObject.Find("LeaveCombatButton").GetComponent<Button>();


        shotgunButton = GameObject.Find("Shotgun").GetComponent<Button>();
        pistolButton = GameObject.Find("Pistol").GetComponent<Button>();
        shotgunImage = GameObject.Find("Shotgun").GetComponent<Image>();
        pistolImage = GameObject.Find("Pistol").GetComponent<Image>();

        //painikkeiden tekstit
        SeagullMeatText = GameObject.Find ("SeagullMeatText").GetComponent<Text> ();
		SealMeatText = GameObject.Find ("SealMeatText").GetComponent<Text> ();
		WalrusMeatText = GameObject.Find ("WalrusMeatText").GetComponent<Text> ();
		FoxMeatText = GameObject.Find ("ArcticFoxMeatText").GetComponent<Text> ();
		PolarBearMeatText = GameObject.Find ("PolarBearMeatText").GetComponent<Text> ();
        TigerMeatText = GameObject.Find("TigerMeatText").GetComponent<Text>();

        //pelin alussa seuraavat gameobjectit eivät ole näkyvissä
        quitLabel.gameObject.SetActive(false);
        AmmoImage.gameObject.SetActive(false);
        MeatImage.gameObject.SetActive(false);
        CoatImage.gameObject.SetActive(false);
        LeaveCombatButton.gameObject.SetActive(false);
        shotgunButton.gameObject.SetActive (false);
        pistolButton.gameObject.SetActive(false);
        closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
        eatTiger.gameObject.SetActive(false);
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
        eatSeagul.onClick.AddListener(() => UI_Sound.PlayEatSound("Seagull Meat"));
        eatSeagul.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seagull Meat")));

        eatSeal.onClick.AddListener(() => UI_Sound.PlayEatSound("Seal Meat"));
        eatSeal.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Seal Meat")));

        eatWalrus.onClick.AddListener(() => UI_Sound.PlayEatSound("Walrus Meat"));
        eatWalrus.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Walrus Meat")));

        eatFox.onClick.AddListener(() => UI_Sound.PlayEatSound("Arctic Fox Meat"));
        eatFox.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Arctic Fox Meat")));

        eatPolarBear.onClick.AddListener(() => UI_Sound.PlayEatSound("Polar Bear Meat"));
        eatPolarBear.onClick.AddListener (() => PlayerStats.EatFoodItem (PlayerStats.GetFoodItem ("Polar Bear Meat")));

        eatTiger.onClick.AddListener(() => UI_Sound.PlayEatSound("Tiger Meat"));
        eatTiger.onClick.AddListener(() => PlayerStats.EatFoodItem(PlayerStats.GetFoodItem("Tiger Meat")));

        shotgunButton.onClick.AddListener(() => ShootLogic.SwitchToRifle());
        shotgunButton.onClick.AddListener(() => SwitchWeaponImage());

        pistolButton.onClick.AddListener(() => ShootLogic.SwitchToPistol());
        pistolButton.onClick.AddListener(() => SwitchWeaponImage());

        LeaveCombatButton.onClick.AddListener(() => LeaveCombat());
        
        //onClickit pelin lopetus nappuloille
        quitPrompt.onClick.AddListener (() => askToQuit (quitPrompt));
		quitNo.onClick.AddListener (() => noToQuit (quitNo));
		quitYes.onClick.AddListener (() => yesToQuit (quitYes));
		menuButton.onClick.AddListener (() => openMenu (menuButton));
		closeActualMenu.onClick.AddListener (() => backToGame (closeActualMenu));
		soundSwitch.onClick.AddListener (() => switchSounds (soundSwitch));

        SwitchWeaponImage ();

    }

    /// <summary>
    /// When the 'Open Inventory' button is clicked, open the inventory menu.
    /// </summary>
	void openVisibility (Button eatFood)
	{

		UI_Sound.PlayClickSound ();

		PlayerMove.StopMove ();
		Debug.Log ("painoit nappia " + eatFood);
        
		eatFood.gameObject.SetActive (false);
        AmmoImage.gameObject.SetActive(true);
        MeatImage.gameObject.SetActive(true);
        CoatImage.gameObject.SetActive(true);
        closeMenu.gameObject.SetActive (true);
		eatSeagul.gameObject.SetActive (true);
		eatFox.gameObject.SetActive (true);
		eatSeal.gameObject.SetActive (true);
		eatWalrus.gameObject.SetActive (true);
		eatPolarBear.gameObject.SetActive (true);
        eatTiger.gameObject.SetActive(true);
        inventorySpace.gameObject.SetActive (true);
		ammoPouch.gameObject.SetActive (true);
		wearing.gameObject.SetActive (true);
		menuButton.gameObject.SetActive (false);
		Debug.Log (PlayerMove.AllowMove);
	}

    /// <summary>
    /// When the 'Close Inventory' button is clicked, close the inventory menu.
    /// </summary>
    void closeVisibility (Button closemenu)
	{
		UI_Sound.PlayClickSound ();

		eatFood.gameObject.SetActive (true);
        AmmoImage.gameObject.SetActive(false);
        MeatImage.gameObject.SetActive(false);
        CoatImage.gameObject.SetActive(false);
        closeMenu.gameObject.SetActive (false);
		eatSeagul.gameObject.SetActive (false);
		eatFox.gameObject.SetActive (false);
		eatSeal.gameObject.SetActive (false);
		eatWalrus.gameObject.SetActive (false);
		eatPolarBear.gameObject.SetActive (false);
        eatTiger.gameObject.SetActive(false);
        inventorySpace.gameObject.SetActive (false);
		ammoPouch.gameObject.SetActive (false);
		wearing.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (true);
		PlayerMove.AllowMove = true;
	}

    /// <summary>
    /// Open the game options menu.
    /// </summary>
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

    /// <summary>
    /// Close the game options menu.
    /// </summary>
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

    /// <summary>
    /// Open the prompt which asks if the player is sure to quit or not.
    /// </summary>
	void askToQuit (Button quitPrompt)
	{
		UI_Sound.PlayClickSound ();

        quitLabel.gameObject.SetActive(true);
        quitNo.gameObject.SetActive (true);
		quitText.gameObject.SetActive (true);
		quitYes.gameObject.SetActive (true);
		soundSwitch.gameObject.SetActive (false);
		closeActualMenu.gameObject.SetActive (false);
		quitPrompt.gameObject.SetActive (false);
        menuButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Close the prompt which asks if they player is sure to quit or not.
    /// </summary>
    void noToQuit (Button quitNo)
	{
		UI_Sound.PlayClickSound ();

        quitLabel.gameObject.SetActive(false);
        quitNo.gameObject.SetActive (false);
		quitText.gameObject.SetActive (false);
		quitYes.gameObject.SetActive (false);
		soundSwitch.gameObject.SetActive (true);
		closeActualMenu.gameObject.SetActive (true);
		quitPrompt.gameObject.SetActive (true);
        menuButton.gameObject.SetActive(true);
    }

	/// <summary>
    /// Quits the game.
    /// </summary>
	void yesToQuit (Button quitYes)
	{
		Debug.Log ("quitting");
		Application.Quit ();
	}

    // A method for leaving combat by pressing a button (to avoid wasting ammo if you have a lot of meat already).
    private void LeaveCombat ()
    {
        AnimalHandler.KillAnimal();
        Hunt.EndHunt();
        Hunt.EndFlag = true;
        Hunt.ShootFlag = false;
        UI_Sound.PlayClickSound();
    }

    // Upon button click (defined earlier in this class), switch to the appropriate weapon image.
    private void SwitchWeaponImage ()
    {
        if (PlayerStats.CurrentWeapon.Damage == 1)
        {
            shotgunImage.sprite = shotgunNotSelected;
            pistolImage.sprite = pistolSelected;
        } else if (PlayerStats.CurrentWeapon.Damage == 3) {
            pistolImage.sprite = pistolNotSelected;
            shotgunImage.sprite = shotgunSelected;
        }
    }

	/// <summary>
    /// Toggles the game's sounds on or off.
    /// </summary>
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



    /// <summary>
    /// Logic for displaying the amounts of meat and ammo that the player has available, 
    /// as well as the cold, hunger, radio part count, cold resistance, worn coat, and elapsed game day stats.
    /// </summary>
    void Update ()
	{
			inventorySpace.text = " = " + PlayerStats.CarriedFood + "/" + PlayerStats.MaxCarriedFood;
	
			ammoPouch.text = " = " + PlayerStats.CarriedAmmo.ToString () + "/" + PlayerStats.MaxCarriedAmmo.ToString ();

		wearing.text = " = " + PlayerStats.GetItemName (PlayerStats.CurrentCoat);
		radioParts.text = "Radio Parts: " + PlayerStats.RadioPartCount + "/5";
		currentHunger.text = "Hunger: " + Mathf.Round (PlayerStats.Hunger * 1f) / 1f + "/100"; //näyttää nykyisen nälkäarvon 
		currentCold.text = "Cold: " + Mathf.Round (PlayerStats.Cold * 1f) / 1f + "/100"; //näyttää nykyisen kylmyysarvon
		currentResistance.text = "Cold Res.: " + PlayerStats.CurrentCoat.ColdResistance + ""; //näyttää nykysen kylmyydenkestävyyden
		kello.text = "Day " + GameTime.GetDays ();
		// If lausekkeet painikkeiden tekstien muuttamiseen
		//1. lokinlihapainike
		if (PlayerStats.NumberOfSeagullMeats > 0) {
			SeagullMeatText.text = PlayerStats.NumberOfSeagullMeats + " x Seagull Meat (" + PlayerStats.GetFoodItem("Seagull Meat").EatValue + ")";
            SeagullMeatText.fontStyle = FontStyle.Bold;
        } else if (PlayerStats.NumberOfSeagullMeats == 0) {
			SeagullMeatText.text = "Out of Seagull Meat";
            SeagullMeatText.fontStyle = FontStyle.Italic;
        }
		//2. hylkeenlihapainike
		if (PlayerStats.NumberOfSealMeats > 0) {
			SealMeatText.text = PlayerStats.NumberOfSealMeats + " x Seal Meat (" + PlayerStats.GetFoodItem("Seal Meat").EatValue + ")";
            SealMeatText.fontStyle = FontStyle.Bold;
        } else if (PlayerStats.NumberOfSealMeats == 0) {
			SealMeatText.text = "Out of Seal Meat";
            SealMeatText.fontStyle = FontStyle.Italic;
        }
		//3. mursunlihapainike
		if (PlayerStats.NumberOfWalrusMeats > 0) {
			WalrusMeatText.text = PlayerStats.NumberOfWalrusMeats + " x Walrus Meat (" + PlayerStats.GetFoodItem("Walrus Meat").EatValue + ")";
            WalrusMeatText.fontStyle = FontStyle.Bold;
        } else if (PlayerStats.NumberOfWalrusMeats == 0) {
			WalrusMeatText.text = "Out of Walrus Meat";
            WalrusMeatText.fontStyle = FontStyle.Italic;
        }
		//4. naalinlihapainike
		if (PlayerStats.NumberOfPolarFoxMeats > 0) {
			FoxMeatText.text = PlayerStats.NumberOfPolarFoxMeats + " x Arctic Fox Meat (" + PlayerStats.GetFoodItem("Arctic Fox Meat").EatValue + ")";
            FoxMeatText.fontStyle = FontStyle.Bold;
        } else if (PlayerStats.NumberOfPolarFoxMeats == 0) {
			FoxMeatText.text = "Out of Arctic Fox Meat";
            FoxMeatText.fontStyle = FontStyle.Italic;
        }
		//5. jääkarhunlihapainike
		if (PlayerStats.NumberOfPolarBearMeats > 0) {
			PolarBearMeatText.text = PlayerStats.NumberOfPolarBearMeats + " x Polar Bear Meat (" + PlayerStats.GetFoodItem("Polar Bear Meat").EatValue + ")";
            PolarBearMeatText.fontStyle = FontStyle.Bold;
        } else if (PlayerStats.NumberOfPolarBearMeats == 0) {
			PolarBearMeatText.text = "Out of Polar Bear Meat";
            PolarBearMeatText.fontStyle = FontStyle.Italic;
		}
        //3. tiger meat button
        if (PlayerStats.NumberOfTigerMeats > 0)
        {
            TigerMeatText.text = PlayerStats.NumberOfTigerMeats + " x Tiger Meat (" + PlayerStats.GetFoodItem("Tiger Meat").EatValue + ")";
            TigerMeatText.fontStyle = FontStyle.Bold;
        }
        else if (PlayerStats.NumberOfTigerMeats == 0)
        {
            TigerMeatText.text = "Out of Tiger Meat";
            TigerMeatText.fontStyle = FontStyle.Italic;
        }

        // If the player has no ammo left, hide the weapon switch buttons.
        // On hunt exit, hide them as well.
        if (Hunt.EndFlag == false && PlayerStats.CarriedAmmo > 0)
        {
            shotgunButton.gameObject.SetActive(true);
            pistolButton.gameObject.SetActive(true);
        } else if (Hunt.EndFlag == true || PlayerStats.CarriedAmmo == 0) {
            shotgunButton.gameObject.SetActive(false);
            pistolButton.gameObject.SetActive(false);
        }

        // Make the leave combat button visible as the hunt begins.
        // Also make the eat menu invisible, apart from the ammo and carried meat stats.
        if (Hunt.EndFlag == false)
        {
            LeaveCombatButton.gameObject.SetActive(true);
            eatFood.gameObject.SetActive(false);
            closeMenu.gameObject.SetActive(false);
            eatSeagul.gameObject.SetActive(false);
            eatFox.gameObject.SetActive(false);
            eatSeal.gameObject.SetActive(false);
            eatWalrus.gameObject.SetActive(false);
            eatPolarBear.gameObject.SetActive(false);
            eatTiger.gameObject.SetActive(false);

            ammoPouch.gameObject.SetActive(true);
            inventorySpace.gameObject.SetActive(true);
            AmmoImage.gameObject.SetActive(true);
            MeatImage.gameObject.SetActive(true);
        }

        // If an animal is killed (either by gun or by leaving combat with the 'Leave' button),
        // reverse the above operation(s) upon returning to the main map view.
        if (AnimalHandler.KillFlagForMenu == true)
        {
            
            LeaveCombatButton.gameObject.SetActive(false);
            eatFood.gameObject.SetActive(true);
            ammoPouch.gameObject.SetActive(false);
            inventorySpace.gameObject.SetActive(false);
            AmmoImage.gameObject.SetActive(false);
            MeatImage.gameObject.SetActive(false);
            CoatImage.gameObject.SetActive(false);

            AnimalHandler.KillFlagForMenu = false;
        }

        if (GameObject.Find("tiger") != null || GameObject.Find("walrus") != null || GameObject.Find("polarBear") != null)
        {
            LeaveCombatButton.gameObject.SetActive(false);
        }

    }

}
