using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLogic : MonoBehaviour
{

    // Variables for internal reference.
    private Image slides;
    private Text introText;
    public Sprite[] sprites;

    private int currentSprite = 1;
    private int currentText = 0;

    private List<string> introTexts = new List<string>();

    private string introText_1 = @"juupa juu, tää on eka teksti";
    private string introText_2 = @"toka tekstihän se tässä";

    void Start()
    {
        // Locate the image subobject of the holder object; assign the first slide to it
        slides = GameObject.Find("slideHolder").GetComponent<Image>();
        slides.sprite = sprites[0];

        // Locate the text subobject of the holder object (a text is not assigned to it yet, because the first slide will be the title slide, with special, 'baked-in' text).
        introText = GameObject.Find("IntroText").GetComponent<Text>();
        // Add all the intro texts to a list, for easy future use.
        introTexts.InsertRange(introTexts.Count, new List<string> { introText_1, introText_2 });

    }

    void Update()
    {

        // Loads the next scene, i.e. starts the actual game.
        if (currentSprite == 3 && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene("Main");

        } else if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {

            // Switches to the next slide + text on Enter-press or left mouseclick
            slides.sprite = sprites[currentSprite];
            currentSprite++;
            introText.text = introTexts[currentText];
            currentText++;
        }

    }

}
