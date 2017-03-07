using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalrusMove : MonoBehaviour {


	public GridManager GridManager;
	private bool moveWalrus = false;
	private float speed = 1.0f;

	/*
	void Start()
	{
	}
	// Update is called once per frame
	void update()
	{					

		//Should Move the object on grid
		Vector3 randomPosition = GridManager.RandomPosition ();
		Vector3 randomPosition2= GridManager.RandomPosition ();

		gameObject.transform.position = Vector3.Lerp (randomPosition, randomPosition2, Mathf.PingPong(Time.time*speed, 1.0f));

	}

*/

}
