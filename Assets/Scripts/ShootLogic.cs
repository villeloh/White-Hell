using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for handling the logic of the shooting mini-game.
/// Authors: Jimi Nikander + Ville Lohkovuori
/// </summary>

public class ShootLogic : MonoBehaviour
{
	/*
	 * Handles the logic of the shooting mini-game.
	 * Attached to: HuntManager
	 * Authors: Jimi Nikander + Ville Lohkovuori
	 */

    // Declare reference variables...
	public Hunt Hunt;
	public PlayerStats PlayerStats;
	public AnimalHandler AnimalHandler;

	private WalrusBehaviour walrusB;
	private SeagullBehaviour seagullB;
	private SealBehaviour sealB;
	private ArcticFoxBehaviour arcticB;
	private PolarBearBehaviour polarB;
    private TigerBehaviour tigerB;

    private Weapon pistol = new Weapon(1);
    private Weapon rifle = new Weapon(3);
   
    private CircleCollider2D circleColl;
    
    // Needed to track the spent ammunition, for purposes of ending the encounter.
    private int spentAmmo = 0;

    // Property for accessing spentAmmo from outside the class.
	public int SpentAmmo {
		get { return spentAmmo; }
		set { spentAmmo = value; }
	}
		

    public void SwitchToRifle ()
    {
        if (PlayerStats.CarriedAmmo > 0 && PlayerStats.CurrentWeapon.Damage == 1)
        {
            PlayerStats.CurrentWeapon = rifle;

            // Switch the animal's collider type according to the player's active weapon (shotgun has a circle collider applied to the animal because irl 
            // its shells spread, making it much easier to hit with).
            GameObject animal = GameObject.FindGameObjectWithTag("animal");
            animal.GetComponent<PolygonCollider2D>().enabled = false;
            animal.GetComponent<CircleCollider2D>().enabled = true;

            print("switched to shotgun!");
        }
    }

    public void SwitchToPistol()
    {
        if (PlayerStats.CurrentWeapon.Damage == 3)
        {
            PlayerStats.CurrentWeapon = pistol;
     
            GameObject animal = GameObject.FindGameObjectWithTag("animal");
            animal.GetComponent<CircleCollider2D>().enabled = false;
            animal.GetComponent<PolygonCollider2D>().enabled = true;

            print("switched to pistol!");
        }
    }

	/// <summary>
    /// If the player has any ammo, clicking/tapping to shoot reduces it by 1. If the player misses a shot two times in total, end the encounter and kill the animal.
    /// </summary>
    // NOTE: The reason all the animals needed a separate statement is simply hurry towards the end of the project. There must be a better way to do this.
    // NOTE #2: It's debatable whether this class should control the *killing* of animals, instead of just *shooting*. It's too late to change it now though.
	void Update ()
	{

        if (PlayerStats.CarriedAmmo == 0)
        {
            SwitchToPistol ();
        }

        if (Hunt.ShootFlag == true) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began || Input.GetMouseButtonDown (0)) {
				spentAmmo++; // for tracking pistol shots as well, this needs to be before the next 'if' statement!
				if (PlayerStats.CarriedAmmo > 0 && PlayerStats.CurrentWeapon.Damage == 3) {
					PlayerStats.CarriedAmmo--;
				}
			}
		}

        // Check for ending the encounter if the player misses the shot 2 times.
        if (GameObject.Find("walrus") != null && !Hunt.EndFlag)
        {
            GameObject walrusRefe = GameObject.Find("walrus");
            walrusB = walrusRefe.GetComponent<WalrusBehaviour>();

            if (spentAmmo >= 2 && walrusB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;
                PlayerStats.PolarBearDeath = true;
            }
        }
        else if (GameObject.Find("seagull") != null && !Hunt.EndFlag)
        {
            GameObject seagullRef = GameObject.Find("seagull");
            seagullB = seagullRef.GetComponent<SeagullBehaviour>();

            if (spentAmmo >= 2 && seagullB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;

            }
        }
        else if (GameObject.Find("seal") != null && !Hunt.EndFlag)
        {
            GameObject sealRef = GameObject.Find("seal");
            sealB = sealRef.GetComponent<SealBehaviour>();

            if (spentAmmo >= 2 && sealB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;

            }
        }
        else if (GameObject.Find("arcticFox") != null && !Hunt.EndFlag)
        {
            GameObject arcticRef = GameObject.Find("arcticFox");
            arcticB = arcticRef.GetComponent<ArcticFoxBehaviour>();

            if (spentAmmo >= 2 && arcticB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;

            }
        }
        else if (GameObject.Find("polarBear") != null && !Hunt.EndFlag)
        {
            GameObject polarBearRef = GameObject.Find("polarBear");
            polarB = polarBearRef.GetComponent<PolarBearBehaviour>();

            if (spentAmmo >= 2 && polarB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;
                PlayerStats.PolarBearDeath = true;
            }
        } else if (GameObject.Find("tiger") != null && !Hunt.EndFlag) {
            GameObject tigerRef = GameObject.Find("tiger");
            tigerB = tigerRef.GetComponent<TigerBehaviour>();

            if (spentAmmo >= 2 && tigerB.SpentHealth - spentAmmo < -1)
            {
                AnimalHandler.KillAnimal();
                Hunt.EndHunt();
                Hunt.EndFlag = true;
                Hunt.ShootFlag = false;
                PlayerStats.PolarBearDeath = true;
            }
        }
	}
}

