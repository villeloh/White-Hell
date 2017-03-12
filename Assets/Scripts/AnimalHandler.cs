using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing the methods that are used for making and killing different animals (called in Hunt.cs)
/// Authors: Jimi Nikander + Ville Lohkovuori
/// </summary>

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
    public Transform TigerIcon;


	private AnimalHandler animalHandler;

	private GameObject walrusRef;
	private GameObject seagullRef;
	private GameObject sealRef;
	private GameObject arcticFoxRef;
	private GameObject polarBearRef;
    private GameObject tigerRef;

	private Vector3 spawnPos;

    private bool killFlagForMenu = false;

    /// <summary>
    /// Spawn a new Walrus animal.
    /// </summary>
	public void MakeWalrus ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (WalrusIcon, spawnPos, Quaternion.identity);
		obj.name = "walrus";
		print ("mursu luotu!");
		walrusRef = GameObject.Find ("walrus");
		walrusRef.AddComponent<WalrusBehaviour> ();
	}

    /// <summary>
    /// Spawn a new Seagull animal.
    /// </summary>
    public void MakeSeagull ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SeagullIcon, spawnPos, Quaternion.identity);
		obj.name = "seagull";
		seagullRef = GameObject.Find ("seagull");
		seagullRef.AddComponent<SeagullBehaviour> ();

	}

    /// <summary>
    /// Spawn a new Seal animal.
    /// </summary>
    public void MakeSeal ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (SealIcon, spawnPos, Quaternion.identity);
		obj.name = "seal";
		sealRef = GameObject.Find ("seal");
		sealRef.AddComponent<SealBehaviour> ();

	}

    /// <summary>
    /// Spawn a new Arctic Fox animal.
    /// </summary>
    public void MakeArcticFox ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (ArcticFoxIcon, spawnPos, Quaternion.identity);
		obj.name = "arcticFox";
		arcticFoxRef = GameObject.Find ("arcticFox");
		arcticFoxRef.AddComponent<ArcticFoxBehaviour> ();

	}

    /// <summary>
    /// Spawn a new Polar Bear animal.
    /// </summary>
    public void MakePolarBear ()
	{
		spawnPos = GridManager.RandomPosition ();
		Transform obj = Instantiate (PolarBearIcon, spawnPos, Quaternion.identity);
		obj.name = "polarBear";
		polarBearRef = GameObject.Find ("polarBear");
		polarBearRef.AddComponent<PolarBearBehaviour> ();

	}

    /// <summary>
    /// Spawn a new Tiger animal.
    /// </summary>
    public void MakeTiger()
    {
        spawnPos = GridManager.RandomPosition();
        Transform obj = Instantiate(TigerIcon, spawnPos, Quaternion.identity);
        obj.name = "tiger";
        tigerRef = GameObject.Find("tiger");
        tigerRef.AddComponent<TigerBehaviour>();

    }

    /// <summary>
    /// Destroy any animal that exists at the moment.
    /// </summary>
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
        Destroy (tigerRef);
        killFlagForMenu = true;
	}

	public Vector3 SpawnPos {
		get { return spawnPos; }
		set { spawnPos = value; }
	}

    public bool KillFlagForMenu
    {
        get { return killFlagForMenu; }
        set { killFlagForMenu = value; }
    }

}
