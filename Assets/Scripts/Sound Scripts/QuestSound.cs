using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSound : MonoBehaviour
{

	/*
     * A class for handling the quest sound effects.
     * Attached to: every spawned QuestIcon object (this is handled in Quests.cs, when creating new quest icons).
     * Author: Ville Lohkovuori
     */

	private PlayerMove playerMove;

	public AudioClip[] questPopupSounds = new AudioClip[4];
	public AudioClip shelterSound;
	public AudioClip nukeSound;

	private AudioSource source;

	private float questPitchLow = 0.75f;
	private float questPitchHigh = 1.25f;
	private float volumeMin = 0.7f;
	private float volumeMax = 1.0f;

	// Use this for initialization
	void Start ()
	{

		// Find the audio source.
		source = gameObject.GetComponent<AudioSource> ();
        
		// Find the PlayerMove script and assign it to this script as a reference... It seems you cannot do this in the inspector, even with prefabs,
		// as the reference is lost when the prefab is spawned from code.
		PlayerMove script = GameObject.Find ("Player").GetComponent<PlayerMove> ();
		playerMove = script;
	}

	// Upon collision with Player, play an appropriate sound effect, with varying pitch and volume.
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

		if (playerMove.CollidedTag == "nuke_site") {
			source.pitch = questPitchHigh;
			source.PlayOneShot (nukeSound, volumeMax);
		}
	}

	// Called in Triggerer.cs to silence the sounds when the outro is triggered.
	public void MuteQuestSound ()
	{
		source.Stop ();
	}

}
