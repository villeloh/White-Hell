﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestNamespace;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for triggering events based on various conditions.
/// </summary>

public class Triggerer : MonoBehaviour
{
    /*
     * Triggers events based on various conditions.
     * Attached to: 'Player' GameObject
     * Author: Ville Lohkovuori
     */

    // Needed for reference between the different scripts.
    public PlayerMove PlayerMove;
    public PlayerStats PlayerStats;
    public Quests Quests;
    public PlayerSound PlayerSound;
    private QuestSound questSound;

    // Needed for triggering the outro.
    private bool outroFlag = false;

    /// <summary>
    /// The quests are triggered based on the name of the object that Player collided with.
    /// </summary>
    // To make the quest triggering work without putting the logic into 'Update()', I've attached Triggerer to the Player GameObject (instead of GameHolder). 
    void OnCollisionEnter2D(Collision2D coll)
    {

        GameObject hitQuest = GameObject.FindGameObjectWithTag(PlayerMove.CollidedTag);
        questSound = hitQuest.GetComponent<QuestSound> ();

        if (PlayerMove.CollidedName == "Quest_1" && Quests.GetQuestBoolean(0) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_1);
            Quests.SpawnQuestIcon("Quest_5", -11.6f, -6.2f, 5);
        }

        if (PlayerMove.CollidedName == "Quest_2" && Quests.GetQuestBoolean(1) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_2);
        }

        if (PlayerMove.CollidedName == "Quest_3" && Quests.GetQuestBoolean(2) == false)
        {
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

            // If quest 7 is not yet done, spawn quest 11.
            if (Quests.GetQuestBoolean(6) == false)
            {
                Quests.SpawnQuestIcon("Quest_11", -12.7f, -1.0f, 11);
            }

        }

        if (PlayerMove.CollidedName == "Quest_7" && Quests.GetQuestBoolean(6) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_7);

            // If quest 6 is not yet done, spawn quest 11.
            if (Quests.GetQuestBoolean(5) == false)
            {
                Quests.SpawnQuestIcon("Quest_11", -12.7f, -1.0f, 11);
            }

        }

        if (PlayerMove.CollidedName == "Quest_8" && Quests.GetQuestBoolean(7) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_8);

            // If quest 11 is already done, spawn quest 12.
            if (Quests.GetQuestBoolean(10) == true)
            {
                Quests.SpawnQuestIcon("Quest_12", 8.5f, 5.6f, 12);
                GameObject.Find("Quest_12").tag = "nuke_site";
            }

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

            // If quest 8 is already done, spawn quest 12.
            if (Quests.GetQuestBoolean(7) == true)
            {
                Quests.SpawnQuestIcon("Quest_12", 8.5f, 5.6f, 12);
                GameObject.Find("Quest_12").tag = "nuke_site";
            }
        }

        if (PlayerMove.CollidedName == "Quest_12" && Quests.GetQuestBoolean(11) == false)
        {
            Quests.ChooseQuest(Quests.QuestEnum.Quest_12);
        }

        // When you get back to a shelter, your cold value gets reset to zero. (It being instant is a tad unrealistic, but it's also easy and convenient.)
        if (PlayerMove.CollidedTag == "shelter")
        {
            PlayerStats.Cold = 0.0f;
        }
    }

    /// <summary>
    /// Trigger the player's death if hunger or cold reaches the maximum allowed value, or the player has missed 2 times while shooting a polar bear.
    /// When the player has enough Radio Parts, the game ends in victory instead.
    /// </summary>
    void Update()
    {
        // The boolean 'outroFlag' is needed because since the Player object continues to exist, so will the scripts that are attached to it... 
        // Meaning that the Outro scene will be loaded every frame(!) unless that's prevented by using a flag such as this.
        if ((PlayerStats.Hunger >= PlayerStats.DeathHunger || PlayerStats.Cold >= PlayerStats.DeathCold || 
            (PlayerStats.RadioPartCount == 5 && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) 
            || PlayerStats.PolarBearDeath == true) && outroFlag == false)
        {
            outroFlag = true;
            PlayerSound.MuteWalkSound ();

            // Added a null check, for the case of death by animal (questSound doesn't exist in that case).
            if (questSound != null)
            {
                questSound.MuteQuestSound();
            }

            SceneManager.LoadScene("Outro");
        }

    } 

}
