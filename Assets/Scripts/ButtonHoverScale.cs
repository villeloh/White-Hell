using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverScale : MonoBehaviour {

    // Scales UI buttons when hovering over them (event). A little visual touch.
    public void ScaleButtonHoverEntry()
    {
        gameObject.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
    }

    public void ScaleButtonHoverExit ()
    {
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }


}
