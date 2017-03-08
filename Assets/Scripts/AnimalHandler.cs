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
	public Transform SeagullIcon;
	public Transform SealIcon;
	public Transform ArcticFoxIcon;
	public Transform PolarBearIcon;


	private AnimalHandler animalHandler;

	private GameObject walrusRef;
	private GameObject seagullRef;
	private GameObject sealRef;
	private GameObject arcticFoxRef;
	private GameObject polarBearRef;

	private Vector3 spawnPos;

	public void MakeWalrus ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (WalrusIcon, spawnPos, Quaternion.identity);
		obj.name = "walrus";
		print ("mursu luotu!");
		walrusRef = GameObject.Find ("walrus");
		walrusRef.AddComponent<WalrusBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void MakeSeagull ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SeagullIcon, spawnPos, Quaternion.identity);
		obj.name = "seagull";
		seagullRef = GameObject.Find ("seagull");
		seagullRef.AddComponent<SeagullBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void MakeSeal ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SealIcon, spawnPos, Quaternion.identity);
		obj.name = "seal";
		sealRef = GameObject.Find ("seal");
		sealRef.AddComponent<SealBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void MakeArcticFox ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (ArcticFoxIcon, spawnPos, Quaternion.identity);
		obj.name = "arcticFox";
		arcticFoxRef = GameObject.Find ("arcticFox");
		arcticFoxRef.AddComponent<ArcticFoxBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void MakePolarBear ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (PolarBearIcon, spawnPos, Quaternion.identity);
		obj.name = "polarBear";
		polarBearRef = GameObject.Find ("polarBear");
		polarBearRef.AddComponent<PolarBearBehaviour> ();

		Hunt.ShootFlag = true;
	}

	public void KillAnimal ()
	{
		print ("got Meat");
		Destroy (walrusRef);
		Destroy (seagullRef);
		Destroy (sealRef);
		Destroy (arcticFoxRef);
		Destroy (polarBearRef);
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
