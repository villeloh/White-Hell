using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : MonoBehaviour
{

    /*
     * A class for handling sea sound effects.
     * Attached to: SeaBackground
     * Author: Ville Lohkovuori
     */ 

    public PlayerMove PlayerMove;

    public AudioClip[] seaSounds;

    private AudioSource source;

    private float seaPitchLow = 0.75f;
    private float seaPitchHigh = 1.25f;
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

        if (!source.isPlaying)
        {

            // If Player hits the sea-shore, a random sound of waves crashing is played.
            if (PlayerMove.CollidedName == "SeaBackground")
            {
                source.pitch = Random.Range (seaPitchLow, seaPitchHigh);
                source.PlayOneShot(seaSounds[Random.Range(0, 2)], Random.Range(volumeMin, volumeMax));
            }
        }
    }
}
