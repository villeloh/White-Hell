using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for managing the sound effect that occurs during the intro.
/// </summary>

public class IntroSound : MonoBehaviour {

    /*
     * A class for managing the sound effect that occurs during the intro.
     * Attached to: slideHolder
     * Author: Ville Lohkovuori
     */
    
    // For reference...
    public AudioClip introSound;
    public IntroLogic IntroLogic;

    private AudioSource source;

	/// <summary>
    /// Get the audio source and assign it to a variable.
    /// </summary>
	void Start ()
    {
        source = gameObject.GetComponent<AudioSource> ();
	}

    /// <summary>
    /// As soon as the title slide switches to slide #2, play the intro sound effect.
    /// </summary>
    void Update()
    {   
        if (IntroLogic.CurrentSprite == 2 && !source.isPlaying)
        {
            source.PlayOneShot(introSound, 1f);
        }
    }
}
