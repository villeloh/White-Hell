using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSound : MonoBehaviour
{

	public PlayerStats PlayerStats;
	public Hunt Hunt;

	public AudioClip pistolSound;
	public AudioClip rifleSound;

	private AudioSource source;

	private float shootPitchLow = 0.75f;
	private float shootPitchHigh = 1.25f;
	private float volumeMin = 0.7f;
	private float volumeMax = 1.0f;

	void Start ()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}

	// Called when clicking / tapping in the hunt 'scene' (really: while Hunt.ShootFlag == true).
	public void PistolShot ()
	{
		source.pitch = Random.Range (shootPitchLow, shootPitchHigh);
		source.PlayOneShot (pistolSound, Random.Range (volumeMin, volumeMax));
	}

	public void RifleShot ()
	{
		source.pitch = Random.Range (shootPitchLow, shootPitchHigh);
		source.PlayOneShot (rifleSound, Random.Range (volumeMin, volumeMax));

	}

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