using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling the walking sounds of the Player.
/// </summary>

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

    // For controlling the pitch and volume of the audio source.
    private float walkPitchLow = 0.75f;
    private float walkPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;

    /// <summary>
    /// Find and assign the audiosource.
    /// </summary>
    void Start ()
    {
        
        source = gameObject.GetComponent<AudioSource> ();
    }

    /// <summary>
    /// Called in Triggerer.cs and Hunt.cs, to silence the walking sounds.
    /// </summary>
    public void MuteWalkSound ()
    {
        source.Stop();
    }

    /// <summary>
    /// When the Player is moving, play one of two walking sound AudioClips, based on the hunger and cold stats 
    /// (heavier sound when the player is more cold / hungry). The sound will stop when the player has either 
    /// stopped or collided.
    /// </summary>
    void Update () {

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

            source.Stop ();
        }
    }
}
