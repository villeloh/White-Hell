using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideSwitcher : MonoBehaviour
{
    private Image slides;
    public Sprite[] sprites;

    private int currentSprite = 1;

    void Start()
    {
        // Locate the image subobject of the holder object; assign the first slide to it
        slides = GameObject.Find("slideHolder").GetComponent<Image>();
        slides.sprite = sprites[0];
    }

    void Update()
    {
        // Switches to the next slide on Enter-press or left mouseclick
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            slides.sprite = sprites[currentSprite];
            currentSprite++;
        }

        // Loads the next scene, i.e. starts the actual game.
        if (currentSprite == 2 && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) )
        {
            SceneManager.LoadScene("Main");
        }
    }

}
