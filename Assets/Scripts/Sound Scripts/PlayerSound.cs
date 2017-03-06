using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    /*
     * A class for handling the walking sounds of the Player.
     * Attached to: Player
     * Author: Ville Lohkovuori
     */
    
    // For reference...
    public PlayerMove PlayerMove;
    public PlayerStats PlayerStats;
    public Triggerer Triggerer;

    public AudioClip[] walkSounds;

    private AudioSource source;

    private float walkPitchLow = 0.75f;
    private float walkPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;
    
    void Start ()
    {
        // Find the audiosource.
        source = gameObject.GetComponent<AudioSource> ();
    }

    // Called in Triggerer.cs when the outro is triggered, to silence the walking sounds.
    public void MuteWalkSound ()
    {
        source.Stop();
    }


    void Update () {

        // When the Player is moving, play one of three walking sound AudioClips.
        if (PlayerMove.ClickFlag == true && !source.isPlaying)
        {
            source.pitch = Random.Range(walkPitchLow, walkPitchHigh);

            if (PlayerStats.Hunger > 50 || PlayerStats.Cold > 50)
            {
                source.PlayOneShot(walkSounds[1], volumeMax);
                print("playing clip 2");
            } else {
                source.PlayOneShot(walkSounds[0], Random.Range(volumeMin, volumeMax));
                print("playing clip 1");
            } 
        } else if (!PlayerMove.ClickFlag || PlayerMove.CollidedTag != null) {

            source.Stop (); }
    }
}
