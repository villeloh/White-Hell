using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling the shoot sounds of the weapons.
/// </summary>

public class ShootSound : MonoBehaviour
{

/*
 * A class for handling the shoot sounds of the weapons.
 * Attached to: HuntManager
 * Author: Ville Lohkovuori
 */

    // For internal reference.
    public PlayerStats PlayerStats;
	public Hunt Hunt;

	public AudioClip pistolSound;
	public AudioClip rifleSound;

	private AudioSource source;

    // For controlling the pitch and volume of the audio source.
    private float shootPitchLow = 0.75f;
	private float shootPitchHigh = 1.25f;
	private float volumeMin = 0.7f;
	private float volumeMax = 1.0f;

    /// <summary>
    /// Find and assign the audio source.
    /// </summary>
	void Start ()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}

    /// <summary>
    /// Pistol sound effect. Called when clicking / tapping without ammo in the hunt 'scene' (really: while Hunt.ShootFlag == true).
    /// </summary>
    public void PistolShot ()
	{
		source.pitch = Random.Range (shootPitchLow, shootPitchHigh);
		source.PlayOneShot (pistolSound, Random.Range (volumeMin, volumeMax));
	}

    /// <summary>
    /// Rifle sound effect. Called when clicking / tapping while carrying ammo in the hunt 'scene' (really: while Hunt.ShootFlag == true).
    /// </summary>
    public void RifleShot ()
	{
		source.pitch = Random.Range (shootPitchLow, shootPitchHigh);
		source.PlayOneShot (rifleSound, Random.Range (volumeMin, volumeMax));

	}

    /// <summary>
    /// Switches between rifle and pistol sounds based on whether the player is carrying any ammo or not.
    /// </summary>
	void Update ()
	{
		if (Hunt.ShootFlag == true) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began || Input.GetMouseButtonDown (0)) {
			
				if (PlayerStats.CarriedAmmo == 0) {
					print ("pistolshot!");
					PistolShot ();
				} else
					RifleShot ();
			}
		}
	}

}