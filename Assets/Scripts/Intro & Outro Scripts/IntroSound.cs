using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization.
	void Start ()
    {
        source = gameObject.GetComponent<AudioSource> ();
	}

    void Update()
    {   
        // As soon as the title slide switches to slide 2, play the sound effect.
        if (IntroLogic.CurrentSprite == 2 && !source.isPlaying)
        {
            source.PlayOneShot(introSound, 1f);
        }
    }
}
