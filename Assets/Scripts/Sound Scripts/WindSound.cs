using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A class for handling wind sounds.
/// </summary>

public class WindSound : MonoBehaviour
{

    /*
     * A class for handling wind sounds.
     * Attached to: GameHolder
     * Author: Ville Lohkovuori
     */
    
    // For internal reference.
    public PlayerMove PlayerMove;

    public AudioClip[] windSounds;

    private AudioSource source;

    // For controlling the pitch and volume of the audio source.
    private float windPitchLow = 0.75f;
    private float windPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;

    /// <summary>
    /// Find and assign to audio source.
    /// </summary>
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// The logic plays one of four wind sounds at random, at all times (in the main scene).
    /// </summary>
    void Update()
    {
        // The pitch and volume of the wind sounds vary slightly at random, to give a more natural impression.
        if (!source.isPlaying)
        {
            source.pitch = Random.Range(windPitchLow, windPitchHigh);
            source.PlayOneShot(windSounds[Random.Range(0, 3)], Random.Range(volumeMin, volumeMax));
        }

        if (PlayerMove.CollidedTag != null)
        {
            source.Stop();
        }
    }
}
