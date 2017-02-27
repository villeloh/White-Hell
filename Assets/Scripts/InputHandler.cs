using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

	/* Stores the player name that is typed in the beginning, for use elsewhere. 
	 * Attached to: GameObject 'PlayerNameInputField'.
	 *	Author: Ville Lohkovuori
	 */

	// Needed for reference between the two classes.
	public PlayerStats PlayerStats;
	public PlayerMove PlayerMove;
    
	// Automatically called when the player name is typed into the input field that's present at game start (Unity behaviour, when it's defined as an event in the editor).
	// When the name has been stored in PlayerStats, destroy the InputField object.
	public void GetInputPlayerName (string inputPlayerName)
	{
		PlayerStats.PlayerName = inputPlayerName;
		print ("nimi otettu talteen"); // debug
		print (PlayerStats.PlayerName); // debug
		PlayerMove.AllowMove = true;
		Destroy (gameObject);
	}

}
