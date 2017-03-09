using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for determining the elapsed 'game days'.
/// </summary>

public class GameTime : MonoBehaviour
{

/*
 * Stores a time-like value that keeps track of the elapsed time since the start of the game.
 * Attached to: 'GameHolder' GameObject
 * Author: Ville Lohkovuori
 */

	private float globalTime = 0.0f;

	public PlayerMove PlayerMove;

    /// <summary>
    /// Make the script's parent object indestructible. This is needed because the 'days' value will be used in the Outro scene.
    /// </summary>
    void Start ()
    {
        DontDestroyOnLoad (gameObject);
    }

    /// <summary>
    /// For use in UI.cs and OutroLogic.cs. Returns the time in pseudo-numbers that resemble days spent in the game world.
    /// </summary>
    public float GetDays()
    {
        float days = Mathf.Round(globalTime * 1f) / 1f;
        return days;
    }

    /// <summary>
    /// Increase globalTime's value with the player's movement frames (used in determining the elapsed game days).
    /// </summary>
	void Update ()
	{
		
		if (PlayerMove.ClickFlag == true) {

			globalTime += 0.004f; 
		}

	}
}
