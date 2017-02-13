using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    private float globalTime = 0.0f;
	private float gameTime = 0.0f;
	public PlayerMove PlayerMove;

	// Returns the gameTime, for use elsewhere (PlayerStats.cs).
    // Nimenä on edelleen GetGameTime, koska propertya ei voi nimetä samaksi kuin koko luokan nimi. -.- Setteriä ei käytetä missään, joten meh, it's ok I guess.
	public float GetGameTime
	{
		get { return gameTime; }
        set { gameTime = value; } // not used atm
	}

    // Returns the total elapsed time (in pseudo-units, not real seconds) since the start of the game. 
    // Unlike gameTime, it won't be reset when the Player stops moving, only frozen until movement resumes.
    public float GlobalTime
    {
        get { return globalTime; }
        set { globalTime = value; } // not used atm
    }
		
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

        // globalTime will be used in showing the time (in the UI overlay) that has elapsed since the start of the game. Its value will eventually be honed to the point 
        // where it gives a convinving illusion of elapsed 'days', given your average movement rate along the course of a typical game. Currently it has placeholder, dummy value.
        if (PlayerMove.MoveFlag == true)
        {
            globalTime += 4.0f; 
        }

		// If the player is moving, the gameTime increases. NOTE: It's not real seconds or anything, merely a value that increases with every frame 
		// so that hunger and cold can be altered accordingly in PlayerStats.cs.
		if (PlayerMove.MoveFlag == true) {
			gameTime += 0.0001f;
		} else {
            gameTime = 0.0f;
        }

	}
}
