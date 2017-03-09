using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling sea sound effects.
/// </summary>

public class SeaSound : MonoBehaviour
{

    /*
     * A class for handling sea sound effects.
     * Attached to: SeaBackground
     * Author: Ville Lohkovuori
     */ 

    // For internal reference.
    public PlayerMove PlayerMove;

    public AudioClip[] seaSounds;

    private AudioSource source;

    // For controlling the pitch and volume of the audio source.
    private float seaPitchLow = 0.75f;
    private float seaPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;


    /// <summary>
    /// Find and assign the audio source.
    /// </summary>
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// If Player hits the sea-shore, a random sound of waves crashing is played.
    /// </summary>
    void Update()
    {

        if (!source.isPlaying)
        {
   
            if (PlayerMove.CollidedName == "SeaBackground")
            {
                source.pitch = Random.Range (seaPitchLow, seaPitchHigh);
                source.PlayOneShot(seaSounds[Random.Range(0, 2)], Random.Range(volumeMin, volumeMax));
            }
        }
    }
}
