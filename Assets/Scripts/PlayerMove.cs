using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
	/* The Player object's movement logic. 
	 * Attached to: GameObject 'Player'.
	 *	Author: Ville Lohkovuori
	 */
    
	// Used for storing the name and collision status of the object that the player collided with.
	private string collidedName;
	private string collidedTag;

	// Flag to check if the user has tapped / clicked.
	// Set to true on click. Reset to false on reaching destination, or if the Player object has already collided and the new movement location is invalid (sea). <-- Prevents movement stuttering.
	private bool clickFlag = false;

	// Flag to allow movement (really: to cast a ray to the point of mouse-click). Needed to prevent the player moving after clicking to exit a quest popup.
	private bool allowMove = true;

	// Checks whether the GameObject (Player) is already collided. Needed in order to prevent stuttering when collided.
	private bool collidedFlag = false;

	// Needed for internal reference; explained later.
	private RaycastHit2D hit;

	// Used for storing the clicked destination point (I *think* it must be a Vector3, even if we're dealing with 2D.).
	private Vector3 endPoint;
	// Alter this to change the speed of the movement of the Player GameObject. NOTE! If this is made public, Unity ignores it for some reason !!!
	// NOTE: currently overridden (as it should be) by the value defined in PlayerStats (according to hunger and cold values)!
	private float moveDuration = 50.0f;

	// Determines the start position (Player's x and y (and z) coordinates).
	private Vector3 startPos = new Vector3 (-2.8f, -2.4f, 0.0f);


	void Start ()
	{   
		// Puts the Player object in the right starting point.
		gameObject.transform.position = startPos;

		AllowMove = false;
	}


	// What happens when the Player GameObject collides with another GameObject. Called automatically when Player collides with something (Unity behavior).
	// Strictly speaking, this is not needed, because two bodies with colliders attached will stop automatically upon collision. However, it prevents
	// movement stuttering, so it's good to have imo.
	void OnCollisionEnter2D (Collision2D coll)
	{
		// Prints confirmation that a collision has happened, and the names of the two collided objects (first one should always be 'Player').
		print ("Collided!");
		print (GetComponent<CircleCollider2D> ().gameObject.name);
		print (coll.gameObject.name);

		// Store the name and tag of the GameObject that the Player collided with, for use in Triggerer.cs.
		collidedName = coll.gameObject.name;
		collidedTag = coll.gameObject.tag;

		// The Island surface has the 'passable' tag enabled; in case it's not there, stop movement.
		// Also sets collidedFlag to 'true'; this is needed in order to prevent movement stuttering.
		if ((coll.gameObject.tag != "passable")) {
			clickFlag = false;
			collidedFlag = true;

			// Debug message, just so we know the flags have been set.
			print ("Collided with non-passable!");
		}
	}

	// When the collision ends, set various stats to false/empty, to ensure appropriate behaviour when the next collision occurs.
	void OnCollisionExit2D (Collision2D coll2)
	{
		collidedFlag = false;
		collidedName = "";
		collidedTag = "";
		print ("No longer collided!"); // debug
	}


	// Properties for accessing various values from outside the class (mainly in Triggerer.cs).
	public string CollidedName {
		get { return collidedName; }
		set { collidedName = value; } // not used anywhere atm, but made as a precaution, and for consistency
	}

	public string CollidedTag {
		get { return collidedTag; }
		set { collidedTag = value; } // not used anywhere atm, but made as a precaution, and for consistency
	}

	public float MoveDuration {
		get { return moveDuration; } // not used atm
		set { moveDuration = value; }
	}

	public bool ClickFlag {
		get { return clickFlag; }
		set { clickFlag = value; }
	}

	public bool AllowMove {
		get { return allowMove; }
		set { allowMove = value; }
	}

	public bool CollidedFlag {
		get { return collidedFlag; }
		set { collidedFlag = value; }
	}


	// Update is called once per frame.
	void Update ()
	{
        
		// Check if the screen is touched / clicked.
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {

			// [[[ Leftover stuffs below; may be needed later, when we'll be using touch controls.

			//declare a variable of RaycastHit struct
			//Create a Ray on the tapped / clicked position
			// Ray ray; ]]]

			//Controls for unity editor. (#if on nimeltään pre-processor joku-jutska, joka kertoo, mille platformille sitä seuraava määrittely on voimassa.)
			#if UNITY_EDITOR
			// 'RaycastHit2D' contains the x and y coordinates of the place that the cast ray hit (it's cast from the Main Camera upon mouse click). 
			// Later on these will be stored in 'endPoint' and used for determining the direction of movement.
			if (allowMove == true) {
				hit = Physics2D.Raycast (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Vector2.zero, 0);
			}

			// [[[leftover: ray = Camera.main.ScreenPointToRay (Input.mousePosition); ]]]

			// for touch device
			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			#endif

			// Check if the cast ray hits any collider, and if so, print the name of the GameObject that it hit.
			if (hit) {
				print ("Object hit is: " + hit.collider.gameObject.name);
				// Set a flag to indicate to move the Player object.
				clickFlag = true;
				// Save the click / tap position, and print it.
				endPoint = hit.point;
				print (endPoint);
			}

		}

		// Stop movement if the Player object has collided and you're trying to click on a spot that isn't tagged as 'passable' (i.e. sea). This is needed to prevent movement stuttering.
		if (collidedFlag == true && hit.collider.gameObject.tag != "passable") {
			clickFlag = false;
		}

		// Check if the flag for movement is 'true' and the current Player object position is not same as the clicked / tapped position.
		if (clickFlag && !Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude)) { //&& !(V3Equal(transform.position, endPoint))) {
			
			// Move the Player object to the desired position
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, endPoint, (1 / (moveDuration * (Vector3.Distance (gameObject.transform.position, endPoint)))));
			// print (Vector3.Distance (gameObject.transform.position, endPoint));

		} // Set the movement indicator flag to 'false' if the endPoint and current Player object position are equal. Print a debug message.
		else if (clickFlag && Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude)) {
			clickFlag = false;
			print ("I am here"); // debug
		}

	}
}
