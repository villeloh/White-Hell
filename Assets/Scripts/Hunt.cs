using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hunt : MonoBehaviour
{


	private bool endFlag = false;

	public ShootLogic ShootLogic;
	public AnimalHandler AnimalHandler;
	public GridManager GridManager;
	// public PlayerMove PlayerMove;
	public PlayerSound PlayerSound;
	public CameraBehaviour CameraBehaviour;
	public int randomNumber;
	public int randomAnimal;
	private bool repeatCombatTrigger = true;
	/// <summary>
	/// Start this instance.
	/// </summary>
	/// 

	private PlayerMove playerMove;
	private WalrusBehaviour walrusBehaviour;

	private bool shootFlag = false;
    private float timeOfLastHunt = 0.0f;

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
        // The time conditions make it so that encounters may only spawn every 10 seconds, and one will be forced to spawn 
        // if there have been none in 100 seconds.
		if (playerMove.ClickFlag == true && repeatCombatTrigger == true) {
			int randomNumber = UnityEngine.Random.Range (0, 100);
			print (randomNumber); // debug
			if ((randomNumber < 20 && (Time.time - timeOfLastHunt) > 10.0f) || (Time.time - timeOfLastHunt) > 100.0f) {

                timeOfLastHunt = Time.time;

                repeatCombatTrigger = false;

				CameraBehaviour.MoveToMiniGame ();

				shootFlag = true;

				//Stops player from moving
				playerMove.StopMove ();
				print ("Hunt Triggers");        
				//Stops walking sounds                  	

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
               /* if (randomAnimal <= 100)
                {
                    AnimalHandler.MakePolarBear();
                } */
                    

            }
        }                                       	
	}

	public void EndHunt ()
	{                                           		
		// Returns the camera to the main play area and stops the shooting sounds from playing.
		CameraBehaviour.MoveToMainGame ();                                  	
		playerMove.AllowMove = true;
		print ("Hunt Ends");
		ShootLogic.SpentAmmo = 0;
        Invoke("LastShot", 0.1f);
        repeatCombatTrigger = true;
	}

    // In order for the last shot sound to play, this method is needed (bare statements cannot be invoked with the Invoke() method).
    public void LastShot()
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

