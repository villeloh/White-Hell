using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Tells Random to use the Unity Engine random number generator.


public class WalrusBehaviour : MonoBehaviour
{

	private float walrusHealth = 6.0f;
	private AnimalHandler animalHandler;

	void Start ()
	{
		GameObject huntManager = GameObject.Find ("HuntManager");
		animalHandler = huntManager.GetComponent<AnimalHandler> ();
	}

	void OnMouseOver ()
	{
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			this.walrusHealth--;
			if (this.walrusHealth == 0)
				animalHandler.KillWalrus ();
		}
	}
}
