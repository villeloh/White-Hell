using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for managing UI sounds (button click & eat sounds).
/// </summary>

public class UI_Sound : MonoBehaviour
{

	/*
     * Class for managing UI sounds (button click & eat sounds).
     * Attached to: Canvas
     * Author: Ville Lohkovuori
     */
   
    // For internal reference.
	public AudioClip clickSound;
	public AudioClip[] eatSounds;
    public AudioClip reloadPistol;
    public AudioClip reloadRifle;

	public PlayerStats PlayerStats;

	private AudioSource source;


    /// <summary>
    /// Find and assign the audio source.
    /// </summary>
    void Start ()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}

    // sound for switching the weapon to pistol
    public void ReloadPistol ()
    {
        source.PlayOneShot(reloadPistol, 1f);
    }

    // sound for switching the weapon to rifle/shotgun
    public void ReloadRifle ()
    {
        source.PlayOneShot(reloadRifle, 1f);
    }

    /// <summary>
    /// Method for playing the eating sounds (called from UI.cs on each button click).
    /// </summary>
    public void PlayEatSound (string animalMeat)
	{
        
        switch (animalMeat)
        {
            case "Seagull Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfSeagullMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;

            case "Arctic Fox Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfPolarFoxMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;

            case "Seal Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfSealMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;

            case "Walrus Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfWalrusMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;

            case "Polar Bear Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfPolarBearMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;

            case "Tiger Meat":

                if (PlayerStats.Hunger >= 1.0f && PlayerStats.NumberOfTigerMeats > 0 && !source.isPlaying)
                {
                    source.PlayOneShot(eatSounds[Random.Range(0, 2)], 1f);
                }

                break;
        }
 
	}

    /// <summary>
    /// Method for playing the clicking sound (called from UI.cs on each button click).
    /// </summary>
	public void PlayClickSound ()
	{
		source.PlayOneShot (clickSound, 1f);
	}

}
