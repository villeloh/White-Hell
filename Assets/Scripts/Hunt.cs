using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hunt : MonoBehaviour
{


	public Walrus Walrus;
	public GridManager GridManager;
	public PlayerMove PlayerMove;
	public PlayerSound PlayerSound;
	public CameraBehaviour CameraBehaviour;
	public int randomNumber;
	private int repeatCombatTrigger = 1;
	/// <summary>
	/// Start this instance.
	/// </summary>
	/// 

	/*
	void Start()
	{
		
		// Starts function CombatTrigger() that repeats once every second.
		InvokeRepeating("CombatTrigger", 1.0f, 1.0f);
	}

	/*public void CombatTrigger()
	{
		//When the player is moving there is a chance that a hunting encounter plays out.
		if (PlayerMove.ClickFlag == true && repeatCombatTrigger == 1)
		{
			int randomNumber = UnityEngine.Random.Range(0, 3);
			if (randomNumber == 2)
			{

				CameraBehaviour.MoveToMiniGame ();
				//Stops player from moving
				PlayerMove.StopMove ();
				print ("Hunt Triggers");        
				//Stops walking sounds                  	
				GridManager.CreateBoard ();	
				print ("board created");
				Walrus.MakeWalrus ();     

			}	                                	                                      	
		}                                       	
	}                                           					
                                               		
	public void EndHunt()                       		
	{                                           		
		//Should Return the camera to the main s			
		print ("Hunt Ends");                                  			
		CameraBehaviour.MoveToMainGame ();                                  	
		PlayerMove.AllowMove = true;
		print ("Hunt Ends");                    
	}                                          
*/
}

