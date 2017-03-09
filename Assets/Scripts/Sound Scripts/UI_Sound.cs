using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for managing UI sounds (button click & eat sounds).
/// </summary>

public class UI_Sound : MonoBehaviour
{

	/*
     * Class for managing UI sounds (button click & eat sounds).
     * Attached to: Canvas
     * Author: Ville Lohkovuori
     */
   
    // For internal reference.
	public AudioClip clickSound;
	public AudioClip[] eatSounds;

	public PlayerStats PlayerStats;

	private AudioSource source;


    /// <summary>
    /// Find and assign the audio source.
    /// </summary>
    void Start ()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}


    /// <summary>
    /// Method for playing the eating sounds (called from UI.cs on each button click).
    /// </summary>
    public void PlayEatSound ()
	{
        // The check is needed in order for the eating sound not play when hunger is already zero at the start of eating.
        if (PlayerStats.HungerBeforeEat > 0.0f) {
			source.PlayOneShot (eatSounds [Random.Range (0, 1)], 1f);
		}
	}

    /// <summary>
    /// Method for playing the clicking sound (called from UI.cs on each button click).
    /// </summary>
	public void PlayClickSound ()
	{
		source.PlayOneShot (clickSound, 1f);
	}

}
