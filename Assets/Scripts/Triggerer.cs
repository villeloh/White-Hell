using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour
{
	// Needed for reference between the different scripts (Unity requirement for MonoBehaviours).
	public PlayerMove PlayerMove;
	public PlayerStats PlayerStats;
	public GameController GameController;

	// Used for setting the status of quests... I'm not sure whether there's a more elegant way to do this? This will work, at any rate.
	private bool quest1_Completed = false;
	private bool quest2_Completed = false;


	// Use this for initialization
	void Start ()
	{
	}




	// Quest methods (tidier to have them as separate methods than to jam everything in Update ()).
	private void Quest_1 ()
	{
		// testing quest reward
		quest1_Completed = true;
		FoodItem polarBearMeat = new FoodItem (400);
		PlayerStats.AddToInv (polarBearMeat, "Jääkarhunliha");
		print ("Poimit: " + PlayerStats.GetItemName (polarBearMeat));
		GameController.DestroyMapIcon (GameObject.Find ("Quest_1"));
		print ("EatValue while in inventory: " + polarBearMeat.EatValue);
		print ("CarriedFood: " + PlayerStats.CarriedFood);
		PlayerStats.EatFoodItem (polarBearMeat);


		// ++ Change the sprite and text on a by-default-invisible 'PopUp' UI object to the appropriate ones, and wait for the player to click the PopUp away.
		// The ChangePopUp () method should be in UI.cs, probably. It could take the to-be-displayed text as a parameter, making it easy to store quest texts in 
		// their appropriate location (i.e., here in Triggerer.cs).
	}

	private void Quest_2 ()
	{ 
		quest2_Completed = true;
		Coat sairaannopee_tuulitakki = new Coat (70.0f);
		if (PlayerStats.CurrentCoat.ColdResistance < sairaannopee_tuulitakki.ColdResistance) {
			PlayerStats.AddToInv (sairaannopee_tuulitakki, "Sairaan nopee Adidas");
			PlayerStats.CurrentCoat = sairaannopee_tuulitakki;
			print ("Uuden takin cold res: " + PlayerStats.CurrentCoat.ColdResistance);
		}
	}






	// Update is called once per frame
	void Update ()
	{

		// questFlag oli tässä turha... Objektin nimi, johon Player törmää, on riittävä ehto questin triggeröimiselle.
		// Yritin saada quest triggereitä toimimaan pelkän törmäyksen perusteella, mutta en onnistunut... nyt on pakko tsekata joka freimi, mikä on järjetötä resurssien tuhlausta -.-
		if (PlayerMove.CollidedName == "Quest_1" && quest1_Completed == false) {
			Quest_1 ();
		}

		if (PlayerMove.CollidedName == "Quest_2" && quest2_Completed == false) {
			Quest_2 ();
		}


		// When you get back to a shelter, your cold value gets reset to zero. (It being instant is a tad unrealistic, but it's also easy and convenient.)
		if (PlayerMove.CollidedName == "Shelter") {
			PlayerStats.Cold = 0.0f;
		}


		// Trigger Player's death if hunger or cold reaches the maximum allowed value.
		// This could've been located in PlayerStats... Less need for getters that way. But as this is a triggered event, yet deathHunger 
		// and deathCold are 'player stats', I thought this order of things to be the most appropriate.
		if (PlayerStats.Hunger == PlayerStats.DeathHunger || PlayerStats.Cold == PlayerStats.DeathCold) {
			PlayerStats.PlayerDeath ();
		}

		if (PlayerStats.RadioPartCount == 5) {
			// ++ lopeta peli ja triggeröi loppuintro
		}

	}
}
