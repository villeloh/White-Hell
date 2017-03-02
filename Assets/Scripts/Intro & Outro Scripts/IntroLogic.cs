using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLogic : MonoBehaviour
{

    /* 
     * Handles the switching of texts and images for the intro slides, and the starting of the actual game.
     * Attached to: slideHolder
     * Author: Ville Lohkovuori
     */

    // Variables for internal reference.
    private Image slides;
    private Text introText;
    public Sprite[] sprites;

    private int currentSprite = 1;
    private int currentText = 0;

    private List<string> introTexts = new List<string>();

    void Start()
    {
        // Locate the image subobject of the holder object; assign the first slide to it.
        slides = GameObject.Find("slideHolder").GetComponent<Image>();
        slides.sprite = sprites[0];

        // Locate the text subobject of the holder object (a text is not assigned to it yet, because the first slide will be the title slide, with a special, 'baked-in' text).
        introText = GameObject.Find("IntroText").GetComponent<Text>();
        // Add all the intro texts to a list, for easy future use.
        introTexts.InsertRange(introTexts.Count, new List<string> { introText_1, introText_2, introText_3 });

    }

    // Needed for playing a sound effect at the correct time, in IntroSound.cs.
    public int CurrentSprite
    {
        get { return currentSprite; }
        set { currentSprite = value; }
    }

    void Update()
    {

        // Loads the next scene, i.e. starts the actual game.
        if (currentSprite == 4 && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene("Main");

        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {

            // Switches to the next slide + text on Enter-press or left mouseclick
            slides.sprite = sprites[currentSprite];
            currentSprite++;
            introText.text = introTexts[currentText];
            currentText++;
        }

    }

    private string introText_1 = @"[ SHIPWRECKED ! ] 

A heavy storm took hold of the boat and there was nothing I could do to steer it clear of the rocks...

My worst nightmare has come true. There is no one else on this island, I have little in the way of supplies, and as a coup de grâce the radio has broken into a thousand pieces. There's almost no hope of rescue, as the island lies far off from all major shipping lanes.";

    private string introText_2 = @"Way I figure it, my only chance is to try and locate new parts for the radio. On my map I've marked four locations where I might be able to salvage some.

I've done some scouting around the wreck, but I hesitate to head into the interior. There's blinding snow and howling, bitter wind that goes through the bones, all the way to the marrow. My coat may look good, but it's no proof against the weather! 

Thankfully, the boat is a fine shelter, even as a wreck. If I don't stray too far and return here for each night, I should be ok.

There are some old structures on the island that may serve as additional shelters. I shall aim to locate these asap, to widen my search for more parts.

Meanwhile, I must hunt for wild animals in order to survive. If only I had on me a little more ammunition...";

    private string introText_3 = @"As I venture out into the white hell that surrounds me, I pray that those who'll eventually find me will do so while I'm alive. If not, this journal will serve as proof of what happened to me, to warn other travelers and to provide a measure of closure to my family.

On ALICE ISLAND
**.**.19**,

Cpt. .........";

}
