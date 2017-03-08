using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Tells Random to use the Unity Engine random number generator.


public class WalrusBehaviour : MonoBehaviour
{

	private float walrusHealth = 6.0f;
	private float spentHealth = 0;
	private AnimalHandler animalHandler;

	void Start ()
	{
		GameObject huntManager = GameObject.Find ("HuntManager");
		animalHandler = huntManager.GetComponent<AnimalHandler> ();
	}

	public float WalrusHealth {
		get { return walrusHealth; }
		set { walrusHealth = value; }
	}

	public float SpentHealth {
		get { return spentHealth; }
		set { spentHealth = value; }
	}

	void OnMouseOver ()
	{
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			this.walrusHealth--;
			this.spentHealth++;
			if (this.walrusHealth == 0)
				animalHandler.KillWalrus ();
		}
	}
}
