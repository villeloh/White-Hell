using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHandler : MonoBehaviour {


    
    public Hunt Hunt;
    // public GridManager GridManager;
    // public PlayerStats PlayerStats;
    // public CameraBehaviour CameraBehaviour;

    public PlayerMove playerMove;

    public Transform WalrusIcon;

    private AnimalHandler animalHandler;
    private WalrusMove walrusMove;

    // Use this for initialization
    void Start () {
		
	}

    public void MakeWalrus()
    {
        
        Transform obj = Instantiate(WalrusIcon, new Vector3(-47.0f, 26.0f, 0), Quaternion.identity);
        obj.name = "walrus";
        print("mursu luotu!");

        GameObject walrusRefe = GameObject.Find("walrus");
        walrusRefe.AddComponent<WalrusMove>();
        walrusRefe.AddComponent<WalrusBehaviour> ();
    }

    public void KillWalrus()
    {
        Hunt.EndHunt();
        print("got Meat");
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
