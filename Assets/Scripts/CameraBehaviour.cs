using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
    // Tarvitaan, jotta voidaan viitata Playeriin. Huom. myös kiinnitettävä Player-objekti CameraBehaviouriin Unityssa.
    public GameObject Player;

	public float zoomSpeed = 1;
	public float targetOrtho;
	public float smoothSpeed = 2.0f;
	public float minOrtho = 1.0f;
	public float maxOrtho = 7.25f;

    

	void Start ()
	{
		targetOrtho = Camera.main.orthographicSize;
    }

	void Update ()
	{
        // Ensin haetaan kameran koordinaatit Playerilta. Sitten lisätään uuden 'vektorin' avulla -10 Z-arvoon, jotta kamera ei ole maassa kiinni. 
        // Voi näyttää monimutkaiselta, mutta C#:ssa tämä on pakollista, koska 'Vector3' on siinä tyypiltään struct, jonka yksittäisen 'osa-arvon' 
        // muuttaminen ei ole suoraan mahdollista (esim. gameObject.transform.position.x = Player.transform.position.x heittää erroria).
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.position += new Vector3(0.0f, 0.0f, -10.0f);

		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0.0f) {
			targetOrtho -= scroll * zoomSpeed;
			targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
		}

		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
	}
}

