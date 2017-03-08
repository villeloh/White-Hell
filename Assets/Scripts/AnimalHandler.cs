using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHandler : MonoBehaviour
{


	public GridManager GridManager;
	public Hunt Hunt;
	// public GridManager GridManager;
	// public PlayerStats PlayerStats;
	// public CameraBehaviour CameraBehaviour;

	public ShootSound ShootSound;

	public Transform WalrusIcon;

	private AnimalHandler animalHandler;
	private WalrusMove walrusMove;

	private GameObject walrusRef;

	private Vector3 spawnPos;

	// Use this for initialization
	void Start ()
	{
		
	}

	// -47.0f, 26.0f, 0)

	public void MakeWalrus ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (WalrusIcon, spawnPos, Quaternion.identity);
		obj.name = "walrus";
		print ("mursu luotu!");
		walrusRef = GameObject.Find ("walrus");
		walrusRef.AddComponent<WalrusMove> ();
		walrusRef.AddComponent<WalrusBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void KillWalrus ()
	{
		print ("got Meat");
		Destroy (walrusRef);
	}

	public Vector3 SpawnPos {
		get { return spawnPos; }
		set { spawnPos = value; }
	}


	// Update is called once per frame
	void Update ()
	{
		
	}
}
