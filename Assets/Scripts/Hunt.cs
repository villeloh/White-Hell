using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for controlling the spawning and ending of random animal encounters.
/// Authors: Jimi Nikander + Ville Lohkovuori
/// </summary>

public class Hunt : MonoBehaviour
{

	private bool endFlag = false;

	public ShootLogic ShootLogic;
	public AnimalHandler AnimalHandler;
	public GridManager GridManager;
	public PlayerSound PlayerSound;
	public CameraBehaviour CameraBehaviour;
	public int randomNumber;
	public int randomAnimal;
	private bool repeatCombatTrigger = true;

	private PlayerMove playerMove;
	private WalrusBehaviour walrusBehaviour;

	private bool shootFlag = false;
	private float timeOfLastHunt = 0.0f;

    /// <summary>
    /// Repeat the random dice roll for animal spawn once every second.
    /// </summary>
	void Start ()
	{
		// Starts function CombatTrigger() that repeats once every second.
		InvokeRepeating ("CombatTrigger", 1.0f, 1.0f);

		GameObject player = GameObject.Find ("Player");
		playerMove = player.GetComponent<PlayerMove> ();
	}

    /// <summary>
    /// Contains the logic for spawning random animal encounters.
    /// </summary>
	public void CombatTrigger ()
	{
		//When the player is moving there is a chance that a hunting encounter plays out.
		// The time conditions make it so that encounters may only spawn every 10 seconds, and one will be forced to spawn 
		// if there have been none in 100 seconds.
		if (playerMove.ClickFlag == true && repeatCombatTrigger == true) {
			int randomNumber = UnityEngine.Random.Range (0, 100);
			print (randomNumber); // debug
			if ((randomNumber < 20 && (Time.time - timeOfLastHunt) > 10.0f) || (Time.time - timeOfLastHunt) > 100.0f) {

                // Stop the player's walking sound.
                PlayerSound.MuteWalkSound ();

                timeOfLastHunt = Time.time;

				repeatCombatTrigger = false;

				CameraBehaviour.MoveToMiniGame ();

				shootFlag = true;

				//Stops player from moving
				playerMove.StopMove ();
				print ("Hunt Triggers");            	

				endFlag = false;
				randomAnimal = Random.Range (0, 100);
				 
				if (randomAnimal < 30) {
					AnimalHandler.MakeSeagull (); 
				} else if (randomAnimal >= 30 && randomAnimal < 45) {
					AnimalHandler.MakeWalrus ();
				} else if (randomAnimal >= 45 && randomAnimal < 70) {
					AnimalHandler.MakeSeal ();
				} else if (randomAnimal >= 70 && randomAnimal < 95) {
					AnimalHandler.MakeArcticFox ();
				} else if (randomAnimal > 95) {
					AnimalHandler.MakePolarBear ();
				}

				// test case
				/*
				if (randomAnimal <= 100) {
					AnimalHandler.MakePolarBear ();
				} */
                    

			}
		}                                       	
	}

    /// <summary>
    /// Ends the hunting mini-game and returns to the main map view.
    /// </summary>
	public void EndHunt ()
	{                                           		
		// Returns the camera to the main play area and stops the shooting sounds from playing.
		CameraBehaviour.MoveToMainGame ();                                  	
		playerMove.AllowMove = true;
		print ("Hunt Ends");
		ShootLogic.SpentAmmo = 0;
		Invoke ("LastShot", 0.1f);
		repeatCombatTrigger = true;
	}

	// In order for the last shot sound to play, this method is needed (bare statements cannot be invoked with the Invoke() method).
	public void LastShot ()
	{
		ShootFlag = false;
	}

	public bool ShootFlag {
		get { return shootFlag; }
		set { shootFlag = value; }
	}

	public bool EndFlag {
		get { return endFlag; }
		set { endFlag = value; }
	}

}

