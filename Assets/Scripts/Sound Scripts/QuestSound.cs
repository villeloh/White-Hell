using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSound : MonoBehaviour {

    /*
     * A class for handling the quest sound effects.
     * Attached to: every spawned QuestIcon object (this is handled in Quests.cs, when creating new quest icons).
     * Author: Ville Lohkovuori
     */

    private PlayerMove playerMove;
    private Triggerer triggerer;

    private AudioClip[] questPopupSounds = new AudioClip[4];
    private AudioClip shelterSound;
    private AudioClip nukeSound;

    private AudioSource source;

    private float questPitchLow = 0.75f;
    private float questPitchHigh = 1.25f;
    private float volumeMin = 0.7f;
    private float volumeMax = 1.0f;

    // Use this for initialization
    void Start ()
    {

        // Fetch the correct script from the persistent 'Player' GameObject.
        GameObject player = GameObject.Find("Player");
        playerMove = player.GetComponent<PlayerMove>();
        triggerer = player.GetComponent<Triggerer> ();

        // Find the audio source and assign the various sounds to variables.
        source = gameObject.GetComponent<AudioSource>();

        shelterSound = Resources.Load("Sounds/ShelterBurn") as AudioClip;
        nukeSound = Resources.Load("Sounds/Geiger Counter") as AudioClip;

        questPopupSounds[0] = Resources.Load("Sounds/QuestSound_1") as AudioClip;
        questPopupSounds[1] = Resources.Load("Sounds/QuestSound_2") as AudioClip;
        questPopupSounds[2] = Resources.Load("Sounds/QuestSound_3") as AudioClip;
        questPopupSounds[3] = Resources.Load("Sounds/QuestSound_4") as AudioClip;

    }

    // Upon collision with Player, play an appropriate sound effect, with varying pitch and volume.
    void OnCollisionEnter2D(Collision2D collision)
    {
        source.pitch = Random.Range(questPitchLow, questPitchHigh);

        // If Player collides with a quest icon, play one of 4 sound effects at random.
        if (playerMove.CollidedTag == "quest")
        {
            print("collided with quest!");
            source.PlayOneShot(questPopupSounds[Random.Range(0, 3)], Random.Range(volumeMin, volumeMax));
        }

        // If Player collides with a shelter icon, a sound of fire burning is played.
        if (playerMove.CollidedTag == "shelter")
        {
            print("collided with shelter!");
            source.PlayOneShot(shelterSound, Random.Range(volumeMin, volumeMax));
        }

        if (playerMove.CollidedTag == "nuke_site")
        {
            source.pitch = questPitchHigh;
            source.PlayOneShot(nukeSound, volumeMax);
        }
    }

    // Update is called once per frame
    void Update () {

        // If a quest sound is playing and the outro is triggered, stop the sound.
        if (source.isPlaying) {

            if (triggerer.OutroFlag == true)
            {
                source.Stop();
            }
        }
    }
}
