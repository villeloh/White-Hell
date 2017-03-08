using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHandler : MonoBehaviour
{


    
	public Hunt Hunt;
	// public GridManager GridManager;
	// public PlayerStats PlayerStats;
	// public CameraBehaviour CameraBehaviour;

	public ShootSound ShootSound;

	public Transform WalrusIcon;

	private AnimalHandler animalHandler;
	private WalrusMove walrusMove;

	private GameObject walrusRef;

	// Use this for initialization
	void Start ()
	{
		
	}

	public void MakeWalrus ()
	{
        
		Transform obj = Instantiate (WalrusIcon, new Vector3 (-47.0f, 26.0f, 0), Quaternion.identity);
		obj.name = "walrus";
		print ("mursu luotu!");

		walrusRef = GameObject.Find ("walrus");
		walrusRef.AddComponent<WalrusMove> ();
		walrusRef.AddComponent<WalrusBehaviour> ();

		ShootSound.ShootFlag = true;
	}

	public void KillWalrus ()
	{
		Hunt.EndHunt ();
		print ("got Meat");
		Destroy (walrusRef);

		ShootSound.ShootFlag = false;
	}


	// Update is called once per frame
	void Update ()
	{
		
	}
}
