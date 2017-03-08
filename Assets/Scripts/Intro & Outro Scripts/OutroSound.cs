using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroSound : MonoBehaviour {

    /*
     * A class for handling the sound effects that are played during the outro.
     * Attached to: OutroCanvas
     * Author: Ville Lohkovuori
     */

    private PlayerStats playerStats2;

    public AudioClip[] savedSounds;
    public AudioClip coldDeathSound;
    public AudioClip hungerDeathSound;
    public AudioClip polarBearDeathSound;
    public AudioClip creditsMusic;

    private AudioSource source;

    // Needed for playing two sound effects in succession, and for moving on to the credits.
    private bool playedFirst = false;
    private bool stopFlag = false;

    // Use this for initialization
    void Start () {

        source = gameObject.GetComponent<AudioSource> ();

        // Fetch the correct script from the persistent 'Player' GameObject.
        GameObject player2 = GameObject.Find("Player");
        playerStats2 = player2.GetComponent<PlayerStats>();

    }
	
	// Play the various death and save sound effects, based on the same conditions as switching to the correct sprites and texts.
    // Technically, this class' functionality could be contained in OutroLogic.cs. I'm not sure whether it's a good idea to put 
    // everything in one class, however. Sounds seems different enough from text and images to be in their own class.
	void Update ()
    {
        // The clip is cut short if there's a tap / mouse click. This is needed because there's no scene change between these slides and the credits.
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
            source.Stop();
        }

        if (!stopFlag) {
            if (playerStats2.Hunger >= playerStats2.DeathHunger && !source.isPlaying)
            {
                source.PlayOneShot(hungerDeathSound, 1f);
                stopFlag = true;
            }
            else if (playerStats2.Cold >= playerStats2.DeathCold && !source.isPlaying)
            {
                source.PlayOneShot(coldDeathSound, 1f);
                stopFlag = true;

            } else if (playerStats2.PolarBearDeath == true)
            {
                source.PlayOneShot(polarBearDeathSound, 1f);
                stopFlag = true;

            } else if (playerStats2.RadioPartCount == 5 && !source.isPlaying && !playedFirst)
            {
                
                    source.PlayOneShot(savedSounds[0], 1f);
                    playedFirst = true;
                }
                else if (playerStats2.RadioPartCount == 5 && !source.isPlaying) {
                    source.PlayOneShot(savedSounds[1], 1f);
                    stopFlag = true;                
                }
            }
        
        // After the main logic has run its course, move on to playing the credits music.
        if (stopFlag == true && !source.isPlaying)
        {
            source.PlayOneShot (creditsMusic, 1f);
        }

    }
}
