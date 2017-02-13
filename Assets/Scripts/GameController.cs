using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerMove PlayerMove;

    private SpriteRenderer iconRenderer;
    private CircleCollider2D iconCollider;

    private GameObject mapIcon;

    private GameObject Quest_2;

    public Sprite[] iconSprites;


	// Use this for initialization
	void Start ()
	{
        // ++ luo Player, Island, SeaBackground, UI (instantioidaan prefabeista... jahka kaikki statsit on lyöty lopullisesti lukkoon)

        SpawnMapIcon("Shelter", -2.0f, -2.0f, iconSprites[0]);
	}

	// Luo uuden MapIconin syötetyllä nimellä kartan kohtaan (x,y). Sprite valitaan listasta, jonka voi koota drag & dropilla Unityssa.
	public void SpawnMapIcon (string iconName, float x, float y, Sprite givenSprite)
	{
		mapIcon = new GameObject ();
		mapIcon.transform.position = new Vector3 (x, y, 0.0f);
        iconRenderer = mapIcon.AddComponent<SpriteRenderer> ();
        iconRenderer.sortingOrder = 3;
        iconRenderer.sprite = givenSprite;
		iconCollider = mapIcon.AddComponent<CircleCollider2D> ();
		iconCollider.radius = 0.15f;
        mapIcon.name = iconName;
    }

    // needed in order to prevent buggy behavior when the icon is destroyed before stopping to be collided... otherwise the regular Destroy() method would've sufficed.
public void DestroyMapIcon (GameObject icon)
    {
        PlayerMove.CollidedFlag = false;
        print("No longer collided!");
        Destroy(icon);
    }


	// Update is called once per frame
	void Update ()
	{
		
	}
}
