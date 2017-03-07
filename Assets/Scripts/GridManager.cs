using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {



	public Hunt Hunt;
	public int randomNumber;
	public int randomnuber2;
	public PlayerMove PlayerMove;
	public PlayerSound PlayerSound;
	private static int boardSize   =  5;								//Array of grud prefabs.							
	public List <Vector3> gridPositions = new List <Vector3> ();
	public GameObject WhiteGridPiece;
	private int repeatCombatTrigger = 1;


	void Start()
	{
	}

	//RandomPosition returns a random position from our list gridPositions.
	public Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);

		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];

		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);

		//Return the randomly selected Vector3 position.
		return randomPosition;
	}



	//Make a grid for the hunting game
	public void CreateBoard()
	{
		repeatCombatTrigger = 0;
		//Create The board
		for(int i = 0; i < boardSize; i++)
		{
			for(int j = 0; j < boardSize; j++)
			{
				Object.Instantiate(WhiteGridPiece,new Vector3(i + -47, j + 26, 0), Quaternion.identity);	
				gridPositions.Add(new Vector3(i + -47, j + 26, 0));
			}

		}

	}
}
