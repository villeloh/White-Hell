using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    public PlayerMove PlayerMove;
    public PlayerStats PlayerStats;
    public Triggerer Triggerer;

    public AudioClip[] walkSounds;
    public AudioClip[] seaSounds;
    public AudioClip[] questPopupSounds;
    public AudioClip shelterSound;
    public AudioClip nukeSound;

    private AudioSource source;

	// Use this for initialization
	void Start ()
    {
        source = gameObject.GetComponent<AudioSource> ();
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        // If Player hits the sea-shore, a random sound of waves crashing is played.
        if (PlayerMove.CollidedName == "SeaBackground")
        {
            print("hit seabackground!");
            source.PlayOneShot (seaSounds[Random.Range(0, 2)], 0.5f);
        }

        // If Player collides with a quest icon, play one of 4 sound effects at random.
        if (PlayerMove.CollidedTag == "quest")
        {
            print("collided with quest!");
            source.PlayOneShot (questPopupSounds[Random.Range(0, 3)], 1f);
        }

        // If Player collides with a shelter icon, a sound of fire burning is played.
        if (PlayerMove.CollidedTag == "shelter")
        {
            source.PlayOneShot(shelterSound, 1f);
        }

        if (PlayerMove.CollidedTag == "nuke_site")
        {
            source.PlayOneShot (nukeSound, 1f);
        }
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
            if (PlayerStats.Hunger > 50 || PlayerStats.Cold > 50)
            {
                source.PlayOneShot(walkSounds[1], 0.7f);
                print("playing clip 2");
            } else {
                source.PlayOneShot(walkSounds[0], 0.7f);
                print("playing clip 1");
            } 
        } else if (!PlayerMove.ClickFlag || Triggerer.OutroFlag == true) { source.Stop (); }
    }
}
