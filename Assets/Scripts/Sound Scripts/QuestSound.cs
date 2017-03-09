using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling the quest sound effects.
/// </summary>

public class QuestSound : MonoBehaviour
{

	/*
     * A class for handling the quest sound effects.
     * Attached to: every spawned QuestIcon object (this is handled in Quests.cs, when creating new quest icons).
     * Author: Ville Lohkovuori
     */

    // For internal reference...
	private PlayerMove playerMove;

	public AudioClip[] questPopupSounds = new AudioClip[4];
	public AudioClip shelterSound;
	public AudioClip nukeSound;

	private AudioSource source;

    // For controlling the pitch and volume of the audio source.
    private float questPitchLow = 0.75f;
	private float questPitchHigh = 1.25f;
	private float volumeMin = 0.7f;
	private float volumeMax = 1.0f;

    /// <summary>
    /// Find and assign the audio source. Find the PlayerMove script and assign it to this script as a reference. 
    /// (It seems you cannot do this in the inspector, even with prefabs,
    /// as the reference is lost when the prefab is spawned from code.)
    /// </summary>
    void Start ()
	{
		source = gameObject.GetComponent<AudioSource> ();
        
		PlayerMove script = GameObject.Find ("Player").GetComponent<PlayerMove> ();
		playerMove = script;
	}

    /// <summary>
    /// Upon collision of the quest icon with Player, play an appropriate sound effect, with varying pitch and volume.
    /// </summary>
    void OnCollisionEnter2D (Collision2D collision)
	{
		source.pitch = Random.Range (questPitchLow, questPitchHigh);

		// If Player collides with a quest icon, play one of 4 sound effects at random.
		if (playerMove.CollidedTag == "quest") {
			print ("collided with quest!");
			source.PlayOneShot (questPopupSounds [Random.Range (0, 3)], Random.Range (volumeMin, volumeMax));
		}

		// If Player collides with a shelter icon, a sound of fire burning is played.
		if (playerMove.CollidedTag == "shelter") {
			print ("collided with shelter!");
			source.PlayOneShot (shelterSound, Random.Range (volumeMin, volumeMax));
		}

        // The nuclear site (quest 12) has its own sound effect of a geiger counter ticking fiercely! :)
		if (playerMove.CollidedTag == "nuke_site") {
			source.pitch = questPitchHigh;
			source.PlayOneShot (nukeSound, volumeMax);
		}
	}

    /// <summary>
    /// Called in Triggerer.cs to silence the sounds when the outro is triggered.
    /// </summary>
    public void MuteQuestSound ()
	{
		source.Stop ();
	}

}
