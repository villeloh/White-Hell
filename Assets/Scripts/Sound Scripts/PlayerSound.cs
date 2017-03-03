using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    /*
     * A class for handling the walking sounds of the Player.
     * Attached to: Player
     * Author: Ville Lohkovuori
     */

    public PlayerMove PlayerMove;
    public PlayerStats PlayerStats;
    public Triggerer Triggerer;

    public AudioClip[] walkSounds;

    private AudioSource source;

    private float walkPitchLow = 0.75f;
    private float walkPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;

    // Use this for initialization
    void Start ()
    {
        source = gameObject.GetComponent<AudioSource> ();
    }

    // Update is called once per frame
	void Update () {

        // When the Player is moving, play one of three walking sound AudioClips.
        // A hunger or cold value in excess of 50 will lead to a heavier, more strained walking sound.
        // NOTE: the reverse logic where a value in Triggerer triggers something here in PlayerSound is non-ideal, to say the least...
        // But it's the easiest way to resolve a complication that results from Player being a persistent object, namely that the 
        // walking sounds will continue to play on into the intro unless the flag is checked.
        // -- A persistent object to hold global variables would've been very useful... Oh well. For the next project! :)
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
        } else if (!PlayerMove.ClickFlag || PlayerMove.CollidedTag != null || Triggerer.OutroFlag == true) {

            source.Stop (); }
    }
}
