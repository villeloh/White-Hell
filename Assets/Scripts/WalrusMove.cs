using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalrusMove : MonoBehaviour
{
	private GridManager gridManager;
	private AnimalHandler animalHandler;
	private bool moveWalrus = false;
	private float speed = 1.0f;

	private float walrusMoveMod = 50.0f;

	private Vector3 randomPosition;


	void Start ()
	{
		// Get reference thingies.
		GameObject huntManager = GameObject.Find ("HuntManager");
		gridManager = huntManager.GetComponent<GridManager> ();
		animalHandler = huntManager.GetComponent<AnimalHandler> ();
		randomPosition = gridManager.RandomPosition ();
	}

	// Update is called once per frame
	void Update ()
	{					
		// throws a random destination when the hunt begins.
		if (gameObject.transform.position == randomPosition) {
			randomPosition = gridManager.RandomPosition ();
		}

		// Moves the walrus between two random positions.
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, randomPosition, 
			(1 / (walrusMoveMod * (Vector3.Distance (gameObject.transform.position, randomPosition)))));

	}
		
}
