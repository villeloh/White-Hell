using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSound : MonoBehaviour {

    public AudioClip[] windSounds;

    private AudioSource source;

    // Use this for initialization
    void Start ()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Plays one of four wind sounds at random, at all times.
		if (!source.isPlaying)
        {
            source.PlayOneShot(windSounds[Random.Range(0, 3)], 1f);
        }
	}
}
