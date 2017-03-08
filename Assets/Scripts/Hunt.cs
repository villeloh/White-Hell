using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hunt : MonoBehaviour
{


	private bool endFlag = false;

	public AnimalHandler AnimalHandler;
	public GridManager GridManager;
	// public PlayerMove PlayerMove;
	public PlayerSound PlayerSound;
	public CameraBehaviour CameraBehaviour;
	public int randomNumber;
	private int repeatCombatTrigger = 1;
	/// <summary>
	/// Start this instance.
	/// </summary>
	/// 

	private PlayerMove playerMove;

	private bool shootFlag = false;

	void Start ()
	{
		// Starts function CombatTrigger() that repeats once every second.
		InvokeRepeating ("CombatTrigger", 1.0f, 1.0f);

		GameObject player = GameObject.Find ("Player");
		playerMove = player.GetComponent<PlayerMove> ();
	}

	public void CombatTrigger ()
	{
		//When the player is moving there is a chance that a hunting encounter plays out.
		if (playerMove.ClickFlag == true && repeatCombatTrigger == 1) {
			int randomNumber = UnityEngine.Random.Range (0, 3);
			print (randomNumber); // debug
			if (randomNumber == 1) {

				CameraBehaviour.MoveToMiniGame ();

				shootFlag = true;

				//Stops player from moving
				playerMove.StopMove ();
				print ("Hunt Triggers");        
				//Stops walking sounds                  	
				// GridManager.CreateBoard ();	
				print ("board created");
				AnimalHandler.MakeWalrus (); 

				endFlag = false;
			}	                                	                                      	
		}                                       	
	}

	public void EndHunt ()
	{                                           		
		//Should Return the camera to the main s
		AnimalHandler.KillWalrus ();
		shootFlag = false;
		CameraBehaviour.MoveToMainGame ();                                  	
		playerMove.AllowMove = true;
		print ("Hunt Ends");
	}

	// Called in AnimalHandler upon spawning animals, to enable the shoot sounds.
	public bool ShootFlag {
		get { return shootFlag; }
		set { shootFlag = value; }
	}

	public bool EndFlag {
		get { return endFlag; }
		set { endFlag = value; }
	}

}

