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
		// Yritin saada quest triggereitä toimimaan pelkän törmäyksen perusteella, mutta en onnistunut... nyt on pakko tsekata joka freimi, mikä on järjetötä resurssien tuhlausta! -.-
        // Mahdollisesti auttaisi, jos Triggerer olisi Playerissa kiinni, mutta se on turhan epäloogista...
		if (PlayerMove.CollidedName == "Quest_1" && Quests.GetQuestBoolean(0) == false) {
			Quests.ChooseQuest (Quests.QuestEnum.Quest_1);
            Quests.SpawnQuestIcon("Quest_5", -11.6f, -5.5f, 5);           
        }

		if (PlayerMove.CollidedName == "Quest_2" && Quests.GetQuestBoolean(1) == false) {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_2);
        }

        if (PlayerMove.CollidedName == "Quest_3" && Quests.GetQuestBoolean(2) == false) {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_3);
            Quests.SpawnQuestIcon("Quest_8", 1.2f, -6.3f, 8);
            Quests.SpawnQuestIcon("Quest_9", 9.8f, 1.8f, 9);
        }

        if (PlayerMove.CollidedName == "Quest_4" && Quests.GetQuestBoolean(3) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_4);
            Quests.SpawnQuestIcon("Quest_6", -10.6f, 2.9f, 6);
            GameObject.Find("Quest_6").tag = "shelter";
            Quests.SpawnQuestIcon("Quest_7", -3.8f, 5.6f, 7);
        }

        if (PlayerMove.CollidedName == "Quest_5" && Quests.GetQuestBoolean(4) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_5);
            Quests.SpawnQuestIcon("Quest_10", 2.6f, 3.8f, 10);
            GameObject.Find("Quest_10").tag = "shelter";
        }

        if (PlayerMove.CollidedName == "Quest_6" && Quests.GetQuestBoolean(5) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_6);
        }

        if (PlayerMove.CollidedName == "Quest_7" && Quests.GetQuestBoolean(6) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_7);
            Quests.SpawnQuestIcon("Quest_11", -12.7f, -1.0f, 11);
        }

        if (PlayerMove.CollidedName == "Quest_8" && Quests.GetQuestBoolean(7) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_8);
        }

        if (PlayerMove.CollidedName == "Quest_9" && Quests.GetQuestBoolean(8) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_9);
        }

        if (PlayerMove.CollidedName == "Quest_10" && Quests.GetQuestBoolean(9) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_10);
        }

        if (PlayerMove.CollidedName == "Quest_11" && Quests.GetQuestBoolean(10) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_11);
            Quests.SpawnQuestIcon("Quest_12", 8.5f, 5.6f, 12);
        }

        if (PlayerMove.CollidedName == "Quest_12" && Quests.GetQuestBoolean(11) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_12);
        }





        // When you get back to a shelter, your cold value gets reset to zero. (It being instant is a tad unrealistic, but it's also easy and convenient.)
        if (PlayerMove.CollidedTag == "shelter") {
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
