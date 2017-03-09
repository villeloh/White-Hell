using UnityEngine;
using System.Collections;

/// <summary>
/// Class for controlling camera behaviour (zooming and zoom speed + position).
/// Author: Niko Eklund
/// </summary>

public class CameraBehaviour : MonoBehaviour
{
	// Tarvitaan, jotta voidaan viitata Playeriin. Huom. myös kiinnitettävä Player-objekti CameraBehaviouriin Unityssa.
	public GameObject Player;

	private float zoomSpeed = 1.0f;
	private float androidZoomSpeed = 0.04f;
	private float targetOrtho;
	private float smoothSpeed = 2.0f;
	private float minOrtho = 1.0f;
	private float maxOrtho = 7.25f;
	private bool CameraMovement = false;


    /// <summary>
    /// Assign values that will be needed later on.
    /// </summary>
	void Start ()
	{
		targetOrtho = Camera.main.orthographicSize;
		CameraMovement = true;
	}

    /// <summary>
    /// Different zoom controls for desktop and mobile devices.
    /// </summary>
	void Update ()
	{
		if (CameraMovement) {
			// Ensin haetaan kameran koordinaatit Playerilta. Sitten lisätään uuden 'vektorin' avulla -10 Z-arvoon, jotta kamera ei ole maassa kiinni. 
			// Voi näyttää monimutkaiselta, mutta C#:ssa tämä on pakollista, koska 'Vector3' on siinä tyypiltään struct, jonka yksittäisen 'osa-arvon' 
			// muuttaminen ei ole suoraan mahdollista (esim. gameObject.transform.position.x = Player.transform.position.x heittää erroria).
			gameObject.transform.position = Player.transform.position;
			gameObject.transform.position += new Vector3 (0.0f, 0.0f, -10.0f);

			#if UNITY_EDITOR_WIN
			float scroll = Input.GetAxis ("Mouse ScrollWheel");
			if (scroll != 0.0f) {
				targetOrtho -= scroll * zoomSpeed;
				targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
			}

			Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
			#endif
		}
		#if UNITY_EDITOR_WIN
		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
		#endif
		
		// pich to zoom scripti
		if (Input.touchCount == 2) { //tarkistaa jos kaksi koskestusta samaanaikaan ruudulla
			//Tallentaa kosketukset
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize - (deltaMagnitudeDiff * androidZoomSpeed), minOrtho, maxOrtho);


		}
	}

    /// <summary>
    /// Disables zoom.
    /// </summary>
	public void StopCameraBehaviour ()
	{
		CameraMovement = false;
	}

    /// <summary>
    /// Enables zoom.
    /// </summary>
	public void StartCameraBehaviour ()
	{
		CameraMovement = true;
	}

    /// <summary>
    /// Moves the camera to a new position over the minigame board and disables zoom.
    /// </summary>
	public void MoveToMiniGame ()
	{
		Camera.main.transform.position = new Vector3 (-46f, 26f, -10f);
		StopCameraBehaviour ();
	}

    /// <summary>
    /// Moves the camera back to the main map view and re-enables zoom.
    /// </summary>
	public void MoveToMainGame ()
	{
		StartCameraBehaviour ();
		Camera.main.transform.position = new Vector3 (-0f, 0f, -10f);

	}

}