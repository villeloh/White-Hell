using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{

/*
 * Stores a time-like value that keeps track of the elapsed time since the start of the game.
 * Attached to: 'GameHolder' GameObject
 * Author: Ville Lohkovuori
 */

	private float globalTime = 0.0f;

	public PlayerMove PlayerMove;

    void Start ()
    {
        DontDestroyOnLoad (gameObject);
    }

    // Returns the total elapsed time (in pseudo-units, not real seconds) since the start of the game.
	public float GlobalTime {
		get { return globalTime; }
		set { globalTime = value; } // not used atm
	}

    // For use in UI.cs and OutroLogic.cs. Returns the time in pseudo-numbers that resemble days spent in the game world.
    public float GetDays()
    {
        float days = Mathf.Round(globalTime * 1f) / 1f;
        return days;
    }


	void Update ()
	{
		// globalTime will be used in showing the time (in the UI overlay) that has elapsed since the start of the game. Its value has been honed to the point 
		// where it gives a convincing illusion of elapsed 'days', given your average movement rate along the course of a typical game.
		if (PlayerMove.ClickFlag == true) {

			globalTime += 0.004f; 
		}

	}
}
