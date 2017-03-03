using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSound : MonoBehaviour
{

    /*
     * A class for handling wind sounds. (Could have a more descriptive name, I suppose.)
     * Attached to: Canvas
     * Author: Ville Lohkovuori
     */

    public PlayerMove PlayerMove;

    public AudioClip[] windSounds;

    private AudioSource source;
    private float windPitchLow = 0.75f;
    private float windPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;

    // Use this for initialization
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Plays one of four wind sounds at random, at all times.
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
