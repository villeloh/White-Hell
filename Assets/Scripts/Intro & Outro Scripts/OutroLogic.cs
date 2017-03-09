using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutroLogic : MonoBehaviour
{

	/* 
     * Handles the switching of texts and images for the outro slides.
     * Attached to: OutroCanvas
     * Author: Ville Lohkovuori
     */

	// Variables for internal reference.
	private PlayerStats playerStats;
	private GameTime gameTime;
	private Image slide;
	private Text outroText;
	public Sprite[] sprites;
	private bool quitFlag = false;

	private SpriteRenderer playerRenderer;

	private string saved_text;
	private string death_day_text;

	// Use this for initialization.
	void Start ()
	{

		// Fetch the correct script from the persistent 'Player' GameObject.
		GameObject player = GameObject.Find ("Player");
		playerStats = player.GetComponent<PlayerStats> ();

		// Make Player invisible (on mobile, the Player object could still be seen in the Outro for some reason).
		playerRenderer = player.GetComponent<SpriteRenderer> ();
		playerRenderer.sprite = null;

		GameObject gameHolder = GameObject.Find ("GameHolder");
		gameTime = gameHolder.GetComponent<GameTime> ();

		// Locate the image and text subobjects of the holder object and assign them to variables.
		slide = GameObject.Find ("OutroHolder").GetComponent<Image> ();
		outroText = GameObject.Find ("OutroText").GetComponent<Text> ();

		// Needs to be done here, because dynamic text is used from PlayerStats.cs.
		saved_text = @"[ RADIO CONTACT ! ]

( *STATIC* ) '... this is ZQ-one-eight-niner, go ahead, over.'

'Thank GOD! I'm stranded on Alice Island, have been for AGES now! Please, HURRY! My coordinates are the following ...'

'Roger that. Are you captain " + playerStats.PlayerName + @", by any chance? Over.'

'Yes... Has there been a search for me?'

'That's affirmative. One hell of a search it was. Welcome home, " + playerStats.PlayerName + @". We'll arrive in about 10 hours. Over.'

'Bless you! THANK you! Oh, and over.'

HOME, after so long..! I can hardly BELIEVE it ... 

[ You were rescued on DAY " + gameTime.GetDays () + @"! Congratulations! ]";

		death_day_text = "\n\n[ You died on DAY " + gameTime.GetDays () + @"! Better luck next time! ]";

		// Vary the text and image based on different conditions.
		if (playerStats.Cold >= playerStats.DeathCold) {

			slide.sprite = sprites [0];
			outroText.text = coldDeath_text + death_day_text;
		} else if (playerStats.Hunger >= playerStats.DeathHunger) {

			slide.sprite = sprites [0];
			outroText.text = hungerDeath_text + death_day_text;
		} else if (playerStats.PolarBearDeath == true) {

			slide.sprite = sprites [0];
			outroText.text = polarBearDeath_text + death_day_text;
		} else {

			slide.sprite = sprites [1];
			outroText.text = saved_text;
		}
	}

	void Update ()
	{
		// Move to the credits slide + text on mouse / enter click / finger tap.
		// The weird code structure is needed in order to prevent a premature application exit upon the mouseclick that switches to the credits slide.
		// This way, only the second, distinct click performs the exit. There's probably a more elegant way to do this ...
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetMouseButtonDown (0)) {

			if (quitFlag == true) {
				print ("Quitting!"); // debug
				Application.Quit ();
			}

			slide.sprite = sprites [2];
			outroText.text = credits_text;
			outroText.color = Color.white;
			quitFlag = true;

		}
	}

	// Static texts can reside here at file bottom.
	private string coldDeath_text = @"[ HYPOTHERMIA ! ]

Strange..! I feel very warm all of a sudden. Hot, in fact!

... ... 

I'm BURNING UP! Maybe I should take off some clothes, 
it's so very WARM now... Almost as warm as HOME ...";

	private string hungerDeath_text = @"[ STARVATION ! ]

I-I... I'm so HUNGRY that I can't go on..! 
This is the end. Goodbye everyone..!";

	private string polarBearDeath_text = @"AAAARGH! OH MY GOD! NOOOOOOOOO !!!";

	private string credits_text = @"Ville Lohkovuori 
	
* Plot / Quest logic
* Map visuals
* Player Movement
* Game Balance
* Sounds

Niko Eklund

* UI Design & Implementation
* Camera Controls
* Game Balance

Jimi Nikander

* Shooting Mini-Game
* Shooting Encounter Trigger Logic";

}
