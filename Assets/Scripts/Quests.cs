using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestNamespace { 

public class Quests : MonoBehaviour
{

    // Needed for reference between MonoBehaviours (I tried to make Quests into a regular class, but Unity wasn't having it... So it's attached to QuestHolder as a MonoBehaviour atm.
    // While that makes a certain amount of sense, it's ultimately unnecessary.)
    public PlayerStats PlayerStats;
    public PlayerMove PlayerMove;
    
    // Stores the map icons that are used for the various quests.
    public Sprite[] iconSprites;

    private SpriteRenderer iconRenderer;
    private CircleCollider2D iconCollider;
    
    // Needed for switching a quest's background sprite and its displayed text
    private Image questImage;
    public Sprite blankQuestImage;
    public Sprite[] questSprites;
    private Text questText;

    // Used for setting the status of quests... I'm not sure whether there's a more elegant way to do this? This will work, at any rate.
    private bool quest1_Completed = false;
    private bool quest2_Completed = false;

    // Getters and setters to access and alter the quest booleans from outside the class. There must be an easier/better way to do this, but figuring it out would be too time-consuming,
    // compared to simply using this 'tried and true' method.
    public bool Quest1_Completed
    {
        get { return quest1_Completed; }
        set { quest1_Completed = value; }
    }

    public bool Quest2_Completed
    {
        get { return quest2_Completed; }
        set { quest2_Completed = value; }
    }

        // Define a list of quest booleans, for use later on.
        List<bool> questBooleans = new List<bool>();

        // Define a list of quest texts, for use later on.
        List<string> questTexts = new List<string>();





        // There must be a better way to store long strings (database?), but I'd rather spend the time on something else. Ugly as it is, this seems to work for our purposes.
        private string questText_1 = @"PLANE CRASH

Lying high over a vast plain of shimmering ice, the orange postal plane is impossible to ignore. The crash occurred only a few years ago. The pilot's corpse lies frozen with an eerily serene expression on what used to be his face.

Snapping his frozen fingers in two, I pry open the mail-bag, locating the following:

[REWARD]

Alas, the plane is too cold to be used as a shelter, nor is there any fire-wood available.

The pilot's map has only one new location marked on it, located far to the Southwest.";

        private string questText_2 = "Quest 2:n teksti näyttää tältä, jou";




        void Start()
        {
            // Add all the quest booleans to a list (for easy manipulation by the RoutineQuestActions() method).
            questBooleans.InsertRange(questBooleans.Count, new List<bool> { quest1_Completed, quest2_Completed });

            // Add all the quest texts to a list (for easy manipulation by the RoutineQuestActions() method).
            questTexts.InsertRange(questTexts.Count, new List<string> { questText_1, questText_2 });

            // Find the quest popup's image and text components and assign them to variables
            questImage = GameObject.Find("QuestHolder").GetComponent<Image>();
            questText = GameObject.Find("QuestText").GetComponent<Text>();

            // Spawn the quests that exist at game start (or their icons, more precisely, but logically they're pretty much equivalent)
            SpawnQuestIcon("Shelter", -2.0f, -2.0f, iconSprites[0]);
            SpawnQuestIcon("Quest_1", -3.17f, 0.59f, iconSprites[1]);
        }


        // I wanted to try using enum + a custom namespace, instead of writing a dozen different quest methods (one for each new quest).
        // I'm not sure if everything is done in a standard way, but this seems to work, so I'm leaving it as it is for now.
        public enum QuestEnum { Quest_1, Quest_2 };

        public void ChooseQuest (QuestEnum givenQuest)
            {
                RoutineQuestActions(givenQuest);

                switch (givenQuest)
                {
                    case QuestEnum.Quest_1:

                        quest1_Completed = true;
                        FoodItem polarBearMeat = new FoodItem(400);
                        PlayerStats.AddToInv(polarBearMeat, "Jääkarhunliha");
                        Debug.Log("Poimit: " + PlayerStats.GetItemName(polarBearMeat));
                        DestroyQuestIcon(GameObject.Find("Quest_1"));
                        Debug.Log("EatValue while in inventory: " + polarBearMeat.EatValue);
                        Debug.Log("CarriedFood: " + PlayerStats.CarriedFood);
                        PlayerStats.EatFoodItem(polarBearMeat);

                    break;


                    case QuestEnum.Quest_2:

                        // quest 2

                    break;



            }
        }

        // Certain things should be done whenever a quest is triggered. Therefore, to save on typing, it makes sense to have them as their own method and call it within each 'actual' quest case.
        private void RoutineQuestActions (QuestEnum givenQuest)
        {       
            // Sets the 'quest_Completed' status flag of the quest in question to 'true', preventing the quest from triggering ever again
            questBooleans[(int)givenQuest] = true;

            // Switches the quest's background sprite to the appropriate one
            questImage.sprite = questSprites[(int)givenQuest];

            // Switches the quest's text to the appropriate one
            questText.text = questTexts[(int)givenQuest];
        }










    // Creates a new quest icon with the given name in spot (x,y). The sprite is chosen from a list that can be made manually in Unity (drag & drop the sprites).
    public void SpawnQuestIcon(string iconName, float x, float y, Sprite givenSprite)
        {
            GameObject questIcon = new GameObject();
            questIcon.transform.position = new Vector3(x, y, 0.0f);
            iconRenderer = questIcon.AddComponent<SpriteRenderer>();
            iconRenderer.sortingOrder = 3;
            iconRenderer.sprite = givenSprite;
            iconCollider = questIcon.AddComponent<CircleCollider2D>();
            iconCollider.radius = 0.15f;
            questIcon.name = iconName;
        }

        // Needed in order to prevent buggy behavior when the icon is destroyed before stopping to be collided... Otherwise the regular Destroy() method would've sufficed.
        public void DestroyQuestIcon(GameObject givenIcon)
        {
            PlayerMove.CollidedFlag = false;
            print("No longer collided!");
            Destroy(givenIcon);
        }

        void Update()
        {
            // I see no other way to check for a mouse-click than to put it in 'update'... I couldn't get this to work by clicking on the image object that's attached to QuestHolder;
            // the click always falls through and hits 'Island' instead. This will work to close the quest window, but it's a clumsy waste of resources. As a bonus, this works 
            // regardless of the clicked location. As a bad side, though, you'll begin moving towards where you clicked, which is highly undesirable, to say the least...
            // There is a class called 'EventTrigger' that could be useful in this situation, but it seems fairly complex and time is of the essence.
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                questImage.sprite = blankQuestImage;
                questText.text = "";
            }
        } 

    }
}



