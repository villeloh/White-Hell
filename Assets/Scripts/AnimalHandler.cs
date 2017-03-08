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
	}

	public void MakeSeagull ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SeagullIcon, spawnPos, Quaternion.identity);
		obj.name = "seagull";
		seagullRef = GameObject.Find ("seagull");
		seagullRef.AddComponent<SeagullBehaviour> ();

	}

	public void MakeSeal ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SealIcon, spawnPos, Quaternion.identity);
		obj.name = "seal";
		sealRef = GameObject.Find ("seal");
		sealRef.AddComponent<SealBehaviour> ();

	}

	public void MakeArcticFox ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (ArcticFoxIcon, spawnPos, Quaternion.identity);
		obj.name = "arcticFox";
		arcticFoxRef = GameObject.Find ("arcticFox");
		arcticFoxRef.AddComponent<ArcticFoxBehaviour> ();

	}

	public void MakePolarBear ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (PolarBearIcon, spawnPos, Quaternion.identity);
		obj.name = "polarBear";
		polarBearRef = GameObject.Find ("polarBear");
		polarBearRef.AddComponent<PolarBearBehaviour> ();

	}

    // To avoid making so many Destroy()s, each animal should belong to a base class called 'Animal'... But the animal scripts are already MonoBehaviours, 
    // and afaik, multiple inheritance is impossible in C#. Thus, we have to try to destroy every animal whenever one of them is destroyed.
	public void KillAnimal ()
	{
		print ("animal killed!");
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

}
