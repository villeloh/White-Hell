using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for creating the game-board for the shooting mini-game.
/// Author: Jimi Nikander
/// </summary>

public class GridManager : MonoBehaviour
{

	public Hunt Hunt;
	public int randomNumber;
	public int randomnuber2;
	public PlayerMove PlayerMove;
	public PlayerSound PlayerSound;
	private static int boardSize = 8;
	//Array of grid prefabs.
	public List <Vector3> gridPositions = new List <Vector3> ();
	public GameObject WhiteGridPiece;
    public GameObject[] RockPrefabs;

    private List<Vector3> rockPositions = new List<Vector3>();

    /// <summary>
    /// Create the game board (called in Awake() because of issues with the animal spawns).
    /// </summary>
	void Awake ()
	{
		CreateBoard ();
        RemoveRockPointsFromList();

    }

    /// <summary>
    /// Returns a random position from our list gridPositions.
    /// </summary>
    public Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);

		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions [randomIndex];

		//Remove the entry at randomIndex from the list so that it can't be re-used.
		// gridPositions.RemoveAt (randomIndex);

		//Return the randomly selected Vector3 position.
		return randomPosition;
	}



    /// <summary>
    /// Make a grid for the shooting mini-game.
    /// </summary>
    public void CreateBoard()
    {
        //Create The board
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                Object.Instantiate(WhiteGridPiece, new Vector3(i + -47, j + 26, 0), Quaternion.identity);
                gridPositions.Add(new Vector3(i + -47, j + 26, 0));
                print("position: " + gridPositions[i]);
            }
        }

        // Spawn 16 rocks in random positions and store their locations in a list.
        for (int k = 0; k < 16; k++)
        {
            int randomNumber = Random.Range(0, boardSize * boardSize);
            Vector3 rockVector = gridPositions[randomNumber];
            Instantiate(RockPrefabs[Random.Range(0,3)], rockVector, Quaternion.identity);
            rockPositions.Add(rockVector);
            print("number of rocks: " + rockPositions.Count);
            print("number of pos vectors: " + gridPositions.Count);
        }
        
	}

    // Searches for rock positions that overlap with movement points and removes those movements points from the list of grid positions,
    // preventing movement to them by animals. The effect should be that animals avoid stepping on the rocks at all times.
    private void RemoveRockPointsFromList ()
    {
        int huu = 64;
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < huu; j++)
            {
                if (rockPositions[i] == gridPositions[j])
                {
                    gridPositions.RemoveAt(j);
                    huu--;            
                }
            }
        }
    }



}
