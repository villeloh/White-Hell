using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.


public class Walrus : MonoBehaviour {

	public WalrusMove WalrusMove;
	public GridManager GridManager;
	public PlayerStats PlayerStats;
	public CameraBehaviour CameraBehaviour;
	private Hunt hunt;
	private PlayerMove playerMove;
	private float AnimalHealth = 6.0f;

	/*
	void Start()
	{
		GameObject Walrus = GameObject.Find("walrus");
		Walrus.AddComponent<Hunt>();
		Walrus.AddComponent<PlayerMove> ();
		playerMove = Walrus.GetComponent<PlayerMove> ();
		hunt = Walrus.GetComponent<Hunt> ();
	}

	public void MakeWalrus ()
	{
		var obj = Instantiate(gameObject,new Vector3( -47, 26, 0), Quaternion.identity);
		obj.name = "walrus";

	}

	void OnMouseOver()
	{
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0)))
		{
			this.AnimalHealth--;
			if (this.AnimalHealth == 0)
				killwalrus ();
		}
	}

	public void killwalrus ()
	{
		

		hunt.EndHunt ();
		print("got Meat");
		Destroy (gameObject);


	}
*/
}