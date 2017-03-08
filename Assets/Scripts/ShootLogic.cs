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
	public AnimalHandler AnimalHandler;

	private WalrusBehaviour walrusB;
	private SeagullBehaviour seagullB;
	private SealBehaviour sealB;
	private ArcticFoxBehaviour arcticB;
	private PolarBearBehaviour polarB;

	// Needed to track the spent ammunition, for purposes of ending the encounter.
	private int spentAmmo = 0;

	public int SpentAmmo {
		get { return spentAmmo; }
		set { spentAmmo = value; }
	}
		

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

			if (spentAmmo >= 2 && walrusB.SpentHealth - spentAmmo < -1) {
				AnimalHandler.KillAnimal ();
				Hunt.EndHunt ();
				Hunt.EndFlag = true;

			}
		} else if (GameObject.Find ("seagull") != null && !Hunt.EndFlag) {
			GameObject seagullRef = GameObject.Find ("seagull");
			seagullB = seagullRef.GetComponent<SeagullBehaviour> ();

			if (spentAmmo >= 2 && seagullB.SpentHealth - spentAmmo < -1) {
				AnimalHandler.KillAnimal ();
				Hunt.EndHunt ();
				Hunt.EndFlag = true;

			}
		} else if (GameObject.Find ("seal") != null && !Hunt.EndFlag) {
			GameObject sealRef = GameObject.Find ("seal");
			sealB = sealRef.GetComponent<SealBehaviour> ();

			if (spentAmmo >= 2 && sealB.SpentHealth - spentAmmo < -1) {
				AnimalHandler.KillAnimal ();
				Hunt.EndHunt ();
				Hunt.EndFlag = true;

			}
		} else if (GameObject.Find ("arcticFox") != null && !Hunt.EndFlag) {
			GameObject arcticRef = GameObject.Find ("arcticFox");
			arcticB = arcticRef.GetComponent<ArcticFoxBehaviour> ();

			if (spentAmmo >= 2 && arcticB.SpentHealth - spentAmmo < -1) {
				AnimalHandler.KillAnimal ();
				Hunt.EndHunt ();
				Hunt.EndFlag = true;

			}
		} else if (GameObject.Find ("polarBear") != null && !Hunt.EndFlag) {
			GameObject polarBearRef = GameObject.Find ("polarBear");
			polarB = polarBearRef.GetComponent<PolarBearBehaviour> ();

			if (spentAmmo >= 2 && polarB.SpentHealth - spentAmmo < -1) {
				AnimalHandler.KillAnimal ();
				Hunt.EndHunt ();
				Hunt.EndFlag = true;
				PlayerStats.PolarBearDeath = true;
				print ("kuolit karhuun! AAAARGH!");
			}
		}

	}
}

