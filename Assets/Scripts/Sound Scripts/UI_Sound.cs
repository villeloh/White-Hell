using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sound : MonoBehaviour {

    /*
     * Class for managing UI sounds (button clicks).
     * Attached to: Canvas
     * Author: Ville Lohkovuori
     */

    public AudioClip clickSound;
    public AudioClip[] eatSounds;

    private AudioSource source;


    // Use this for initialization
    void Start () {

        // Find the audio source.
        source = gameObject.GetComponent<AudioSource>();
    }
	
    // Methods for playing the sounds (called from UI.cs on each button click).
    public void PlayEatSound ()
    {
        source.PlayOneShot(eatSounds[Random.Range(0, 1)], 1f);
    }

    public void PlayClickSound ()
    {
        source.PlayOneShot (clickSound, 1f);
    }

}
