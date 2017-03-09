using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Tells Random to use the Unity Engine random number generator.


public class SealBehaviour : MonoBehaviour
{

	private float animalHealth = 4.0f;
	private float spentHealth = 0;
	private AnimalHandler animalHandler;
	private Hunt hunt;
	private PlayerStats playerStats;

	private GridManager gridManager;

	private float animalMoveMod = 47.0f;

	private Vector3 randomPosition;

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

	void OnMouseOver ()
	{
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			for (int i = 0; i < playerStats.CurrentWeapon.Damage; i++) {
				this.animalHealth--;
				this.spentHealth++;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

		if (this.animalHealth <= 0) { 
			hunt.EndHunt ();
			animalHandler.KillAnimal ();
			playerStats.AddToInv (new FoodItem (30), "Seal Meat");
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


