using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class for controlling Seagull behaviour.
/// </summary>

public class SeagullBehaviour : MonoBehaviour
{

	private float animalHealth = 1.0f;
	private float spentHealth = 0;
	private AnimalHandler animalHandler;
	private Hunt hunt;
	private PlayerStats playerStats;

	private GridManager gridManager;

	private float animalMoveMod = 20.0f;

	private Vector3 randomPosition;

    /// <summary>
    /// Get the needed GameObjects for internal reference.
    /// </summary>
    void Start ()
	{
		GameObject huntManager = GameObject.Find ("HuntManager");
		animalHandler = huntManager.GetComponent<AnimalHandler> ();
		hunt = huntManager.GetComponent<Hunt> ();

		GameObject player = GameObject.Find ("Player");
		playerStats = player.GetComponent<PlayerStats> ();

		gridManager = huntManager.GetComponent<GridManager> ();
		randomPosition = gridManager.RandomPosition ();

	}

	public float SpentHealth {
		get { return spentHealth; }
		set { spentHealth = value; }
	}

    /// <summary>
    /// If a click/tap is registered while hovering over the animal, reduce its health by the weapon's damage stat.
    /// </summary>
    void OnMouseOver ()
	{
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
            for (int i = 0; i < playerStats.CurrentWeapon.Damage; i++)
            {
                this.animalHealth--;
                this.spentHealth++;
            }
        }
    }

    /// <summary>
    /// If animal's health reaches zero, kill it and the minigame.
    /// Also causes the animal to move perpetually between random positions.
    /// </summary>
    void Update ()
	{

        if (this.animalHealth <= 0)
        {
            hunt.EndHunt();
            animalHandler.KillAnimal();
            playerStats.AddToInv(new FoodItem(10), "Seagull Meat");
        }

        // throws a new random destination whenever the animal reaches its destination.
        if (gameObject.transform.position == randomPosition) {
			randomPosition = gridManager.RandomPosition ();
		}

		// Moves the walrus between two random positions.
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, randomPosition, 
			(1 / (animalMoveMod * (Vector3.Distance (gameObject.transform.position, randomPosition)))));
	}
}

