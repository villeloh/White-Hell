using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestNamespace;

public class Triggerer : MonoBehaviour
{
	// Needed for reference between the different scripts (Unity requirement for MonoBehaviours).
	public PlayerMove PlayerMove;
	public PlayerStats PlayerStats;
    public Quests Quests;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{

		// questFlag oli tässä turha... Objektin nimi, johon Player törmää, on riittävä ehto questin triggeröimiselle.
		// Yritin saada quest triggereitä toimimaan pelkän törmäyksen perusteella, mutta en onnistunut... nyt on pakko tsekata joka freimi, mikä on järjetötä resurssien tuhlausta -.-
		if (PlayerMove.CollidedName == "Quest_1" && Quests.Quest1_Completed == false) {
			Quests.ChooseQuest (Quests.QuestEnum.Quest_1);
		}

		if (PlayerMove.CollidedName == "Quest_2" && Quests.Quest2_Completed == false) {
			// Quest_2 ();
		}


		// When you get back to a shelter, your cold value gets reset to zero. (It being instant is a tad unrealistic, but it's also easy and convenient.)
		if (PlayerMove.CollidedName == "Shelter") {
			PlayerStats.Cold = 0.0f;
		}


		// Trigger Player's death if hunger or cold reaches the maximum allowed value.
		if (PlayerStats.Hunger == PlayerStats.DeathHunger || PlayerStats.Cold == PlayerStats.DeathCold) {
			PlayerStats.PlayerDeath ();
		}

		if (PlayerStats.RadioPartCount == 5) {
			// ++ lopeta peli ja triggeröi loppuintro
		}

	}
}
