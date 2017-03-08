using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
	/*
	 * Handles the logic of the shooting mini-game.
	 * Attached to: HuntManager
	 * 
	 */

	public Hunt Hunt;
	public PlayerStats PlayerStats;

	private WalrusBehaviour walrusB;

	// Needed to track the spent ammunition, for purposes of ending the encounter.
	private int spentAmmo = 0;

	// Update is called once per frame
	void Update ()
	{
		if (Hunt.ShootFlag == true) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began || Input.GetMouseButtonDown (0)) {
				spentAmmo++; // for tracking pistol shots as well, this needs to be before the next 'if' statement!
				if (PlayerStats.CarriedAmmo > 0) {
					PlayerStats.CarriedAmmo--;

				}
			}
		}

		// Check for ending the encounter if the player misses the shot 2 times.
		if (GameObject.Find ("walrus") != null && !Hunt.EndFlag) {
			GameObject walrusRefe = GameObject.Find ("walrus");
			walrusB = walrusRefe.GetComponent<WalrusBehaviour> ();

			if (spentAmmo >= 2 && walrusB.SpentHealth == 0) {
				Hunt.EndHunt ();
				Hunt.EndFlag = true;
				spentAmmo = 0;
			}
		}

	}
}

