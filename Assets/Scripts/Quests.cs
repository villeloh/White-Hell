using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestNamespace
{

	public class Quests : MonoBehaviour
	{

        /* Stores all the info relating to quests (pop-up events which occur when hitting various GameObjects). 
        * Attached to: GameObject 'QuestHolder'.
        * Author: Ville Lohkovuori
        */

        // Needed for reference between MonoBehaviours (I tried to make Quests into a regular class, but Unity wasn't having it... So it's attached to QuestHolder as a MonoBehaviour atm.
        // While that makes a certain amount of sense, it seems to be ultimately unnecessary.)
        public PlayerStats PlayerStats;
		public PlayerMove PlayerMove;
    
		// For storing the map icons & labels that are used for the various quests, and their changed versions.
		public Sprite[] questIconSprites;
		public Sprite[] changedQISprites;
		public Sprite[] questLabelSprites;

        // Stores the quests' background images (currently the same one is used for all quests).
        public Sprite[] questBackgroundSprites;

        // Needed when spawning text icons on the map from a prefab (the logic is a bit fuzzy to me still, but this seems to work).
        public Transform TextIcon;

        // Needed for internal reference.
        private SpriteRenderer iconRenderer;
		private SpriteRenderer labelRenderer;
		private CircleCollider2D iconCollider;
        private GameObject questHolderHolder;
        private Text questText;
        private Image questImage;

		// Used for setting the status of quests... I'm not sure whether there's a more elegant way to do this? This will work, at any rate.
		private bool quest1_Completed = false;
		private bool quest2_Completed = false;
		private bool quest3_Completed = false;
		private bool quest4_Completed = false;
		private bool quest5_Completed = false;
		private bool quest6_Completed = false;
		private bool quest7_Completed = false;
		private bool quest8_Completed = false;
		private bool quest9_Completed = false;
		private bool quest10_Completed = false;
		private bool quest11_Completed = false;
		private bool quest12_Completed = false;

		// Define a list of quest booleans, for use later on.
		private List<bool> questBooleans = new List<bool> ();

		// Define a list of quest texts, for use later on.
		private List<string> questTexts = new List<string> ();


		void Start ()
		{

			// Add all the quest booleans to a list (for easy manipulation by the RoutineQuestActions() method).
            // NOTE: The 'inner new List' thingy is copied from the internet, and I'm a bit fuzzy about its internal logic. But it works, so, meh. :p
			questBooleans.InsertRange (questBooleans.Count, new List<bool> {
				quest1_Completed,
				quest2_Completed,
				quest3_Completed,
				quest4_Completed,
				quest5_Completed,
				quest6_Completed,
				quest7_Completed,
				quest8_Completed,
				quest9_Completed,
				quest10_Completed,
				quest11_Completed,
				quest12_Completed
			});

			// Add all the quest texts to a list (for easy manipulation by the RoutineQuestActions() method).
			questTexts.InsertRange (questTexts.Count, new List<string> {
				questText_1,
				questText_2,
				questText_3,
				questText_4,
				questText_5,
				questText_6,
				questText_7,
				questText_8,
				questText_9,
				questText_10,
				questText_11,
				questText_12
			});

			// Find the quest popup's image and text components and assign them to variables (for easy manipulation later on).
			// Then disable the QuestHolder object to prevent its Image component from blocking the initial input field
			// for the player's name.
			questImage = GameObject.Find ("QuestHolder").GetComponent<Image> ();
			questText = GameObject.Find ("QuestText").GetComponent<Text> ();
			questHolderHolder = GameObject.Find ("QuestHolder");
			questHolderHolder.SetActive (false);

			// Spawn the quests that exist at game start (or their icons, more precisely, but logically they're pretty much equivalent).
			SpawnQuestIcon ("Boat Wreck", -2.1f, -2.3f, 0);
			GameObject.Find ("Boat Wreck").tag = "shelter";

			SpawnQuestIcon ("Quest_1", -6.6f, 0.4f, 1);
			SpawnQuestIcon ("Quest_2", 4.4f, 0.4f, 2);
			SpawnQuestIcon ("Quest_3", 8.8f, -3.7f, 3);
			GameObject.Find ("Quest_3").tag = "shelter";
			SpawnQuestIcon ("Quest_4", -1.9f, 3.9f, 4);

            // Testing text icon spawning.
            // NOTE: The coordinates are scaled with the canvas, which means that replacing the existing quest labels with plain text would be too tedious for the meager benefit.
            // Therefore I've opted to keep the labels as Sprites, and use the new SpawnTextIcon() method for spawning place names only.
			SpawnTextIcon ("Half Moon Bay", -182.0f, -457.0f);

		}



        // Methods for accessing the quest booleans from outside the class (in Triggerer.cs).
        // IMPORTANT NOTE: It seems that the booleans have a different identity after being added to the list; manipulating the original values (Quest1_Completed, etc) does nothing unless their use
        // is explicitly specified beforehand. As the list is quite necessary in a few spots, these methods should be used at all times when it comes to altering the values of the quest booleans.

        public bool GetQuestBoolean(int givenNumber)
        {
            return questBooleans[givenNumber];
        }

        public void SetQuestBoolean(int givenNumber, bool newValue)
        {
            questBooleans[givenNumber] = newValue;
        }

        // Toggles the 'active' status of the 'QuestHolder' Game Object (and, by proxy, its Image component, which would otherwise block
        // certain clicks that are needed when dealing with pop-ups.).
        public void ToggleQuestHolderActive()
        {
            questHolderHolder.SetActive(!questHolderHolder.activeSelf);
        }

        // I wanted to try using enum + a custom namespace, instead of writing a dozen different quest methods (one for each new quest).
        // I'm not sure if everything is done in a standard way, but this seems to work, so I'm leaving it as it is for now.
        public enum QuestEnum
		{
			Quest_1,
			Quest_2,
			Quest_3,
			Quest_4,
			Quest_5,
			Quest_6,
			Quest_7,
			Quest_8,
			Quest_9,
			Quest_10,
			Quest_11,
			Quest_12 }

		public void ChooseQuest (QuestEnum givenQuest)
		{
			RoutineQuestActions (givenQuest);

			switch (givenQuest) {
			case QuestEnum.Quest_1:

				Coat OldPilotJacket = new Coat (30.0f);
				PlayerStats.AddToInv (OldPilotJacket, "Old Pilot Jacket");

				PlayerStats.CarriedAmmo += 5;
				PlayerStats.MaxAmmoCheck ();

				FoodItem SeagullMeat = new FoodItem (10);
				PlayerStats.AddToInv (SeagullMeat, "Seagull Meat");

				break;

			case QuestEnum.Quest_2:

                    // quest 2

				break;

			case QuestEnum.Quest_3:

                    // quest 3

				break;

			case QuestEnum.Quest_4:

                    // quest 4

				break;

			case QuestEnum.Quest_5:

                    // quest 5

				break;

			case QuestEnum.Quest_6:

                    // quest 6

				break;

			case QuestEnum.Quest_7:

                    // quest 7

				break;

			case QuestEnum.Quest_8:

                    // quest 8

				break;

			case QuestEnum.Quest_9:

                    // quest 9

				break;

			case QuestEnum.Quest_10:

                    // quest 10

				break;

			case QuestEnum.Quest_11:

                    // quest 11

				break;

			case QuestEnum.Quest_12:

                    // quest 12

				break;
			}
		}

		// Certain things should be done whenever a quest is triggered. Therefore, to save on typing, it makes sense to have them as their own method and call it within each 'actual' quest case.
		private void RoutineQuestActions (QuestEnum givenQuest)
		{       

			// Sets the 'quest_Completed' status flag of the quest in question to 'true', preventing the quest from triggering ever again.
			questBooleans [(int)givenQuest] = true;

			// Stops the player from moving on the first click after the quest popup comes up.
			PlayerMove.AllowMove = false;

			// Activates the QuestHolder GameObject (and, by proxy, its script, image and text components).
			ToggleQuestHolderActive ();

			// Switches the quest's background sprite to the appropriate one
			questImage.sprite = questBackgroundSprites [(int)givenQuest];

			// Switches the quest's text to the appropriate one
			questText.text = questTexts [(int)givenQuest];
        
			// Switches the quest's map icon to the appropriate one.
			ChangeQuestIcon (GameObject.Find (givenQuest.ToString ()));

		}
			
		// Creates a new quest icon + quest label with the given name in spot (x,y). The sprites are chosen from lists that can be made manually in Unity (just drag & drop the sprites).
		// NOTE #1: It would've been simpler to have the labels as texts, but for the life of me I couldn't get this to work. And when I finally did, a new complication arose (shifted 
        // coordinates due to scaling), so, I've opted to keep the labels as Sprites for now.

		// NOTE #2: It is in all likelihood a VERY bad practice to associate two Arrays with each other in the way I've done it here. Right now, the int 'iconAndLabel_ID' is the
		// ONLY thing that's linking together the chosen quest icons and quest labels. If either Array of Sprites gets 'out of sync', it changes potentially ALL of
		// the associated labels/icons to the wrong ones! I tried using a Dictionary to associate the two different Arrays with each other, but the syntax proved
		// too hard for me, since the Arrays' contents are defined in the editor and so they can only be referred to in a general way (which I couldn't handle, at present).
		// So... This is a make-shift solution, very non-ideal, but it works (unless someone else goes ahead and decides to break it...). :O
		public void SpawnQuestIcon (string iconName, float x, float y, int iconAndLabel_ID)
		{
			GameObject questIcon = new GameObject (iconName);
			questIcon.transform.position = new Vector3 (x, y, 0.0f);
			iconRenderer = questIcon.AddComponent<SpriteRenderer> ();
			iconRenderer.sortingOrder = 3;
			iconRenderer.sprite = questIconSprites [iconAndLabel_ID];
			iconCollider = questIcon.AddComponent<CircleCollider2D> ();
			iconCollider.radius = 0.18f;

			GameObject questLabel = new GameObject (iconName + "_label");
			questLabel.transform.position = new Vector3 (x, y + 0.38f, 0.0f);
			labelRenderer = questLabel.AddComponent<SpriteRenderer> ();
			labelRenderer.sortingOrder = 3;
			labelRenderer.sprite = questLabelSprites [iconAndLabel_ID];

		}

		// Changes the quest icon to one that indicates the quest has been completed. The collider is destroyed in order to make the quest non-interactable.
		// Originally, this method *destroyed* the used quest icon, but it's better to have them stay on the map, to track the player's progress.
		// If the icon is tagged as a shelter, then the method changes its icon to the 'shelter' icon, but leaves the collision intact.
		public void ChangeQuestIcon (GameObject givenIcon)
		{
			if (givenIcon.tag != "shelter") {

				Destroy (givenIcon.GetComponent<CircleCollider2D> ());

				PlayerMove.CollidedFlag = false;
				PlayerMove.CollidedTag = "";
				PlayerMove.CollidedName = "";
				print ("No longer collided!"); // debug

				iconRenderer = givenIcon.GetComponent<SpriteRenderer> ();
				iconRenderer.sprite = changedQISprites [0];
			} else if (givenIcon.tag == "shelter") {
				iconRenderer = givenIcon.GetComponent<SpriteRenderer> ();
				iconRenderer.sprite = changedQISprites [1];
			}
		}

		// Spawns text icons on the map... This had to be done with a Text prefab, because Unity doesn't allow
		// for direct modification of the text component of a newly created Text object ('protection level' error).
        // ... Or maybe it does, and I simply missed something trivial. Anyway, prefabs are a superior way of dealing
        // with numerous GameObjects, and should be used on all occasions like this one.
		public void SpawnTextIcon (string givenText, float x, float y)
		{
			Transform icon = Instantiate (TextIcon, new Vector3 (x, y, 0.0f), Quaternion.identity);
			icon.transform.SetParent (GameObject.Find ("MapIconCanvas").transform, false);
			icon.name = givenText + "_icon";
			Text textIconText = icon.GetComponent<Text> ();
			textIconText.text = givenText;
		}

		void Update ()
		{
			// I see no other way to check for a mouse-click than to put it in 'update'... I couldn't get this to work by clicking on the image object that's attached to QuestHolder;
			// the click always falls through and hits 'Island' instead. This will work to close the quest window, but it's a clumsy waste of resources. As a bonus, this works 
			// regardless of the clicked location. There is a class called 'EventTrigger' that could be useful in this situation, but it seems fairly complex and time is of the essence.
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetMouseButtonDown (0)) {
				questText.text = ""; // unnecessary, I guess, but meh, might as well keep it
				ToggleQuestHolderActive ();
				PlayerMove.AllowMove = true;
			}
		}

        // All the quest texts are stored at the bottom of the file, to reduce needless scrolling.
		// There must be a better way to store long strings (database?), but I'd rather spend the time on something else. Ugly as it is, this seems to work for our purposes.
		private string questText_1 = @"[ PLANE CRASH ]

Lying high over a vast plain of shimmering ice, the orange postal plane is visible from miles away. The crash occurred only a few years ago. The pilot's corpse lies frozen in the cockpit, a serene expression on what used to be his face. 

Having removed his jacket, I pry open the small mail bag. His fingers make a funny sound as they snap in two. In total I gather the following: 

 - 1 x Old Pilot Jacket (30)
 - 5 x Rifle Ammo
 - 1 x Seagull Meat (canned)

Alas, the plane is too cold to be used as a shelter, nor is there any fire-wood available.

The pilot's map does have one new location marked on it, located far to the Southwest.";

		private string questText_2 = @"[ SEALING CAMP ]

At last, the old sealing camp appears on the horizon, Northeast from Half Moon Bay. As I recall, there have been many companies operating here over the years, leaving behind a lot of their old equipment.

Hoping for a lucky break, it turns out I hit one, as I was able to salvage a small bounty of items: 

 - 1 x Seal-Skin Coat
 - 10 x Rifle Ammo
 - 3 x Seal Meat(canned)]

The seal-skin coat in particular is a great find, putting me that much closer to salvation.

I'm surprised there's no parts for the radio.I wonder if someone else has salvaged them before me -- and if so, where did they head from here?

While the buildings are in a fair enough condition, and there's fire-wood available, the terrain is too rough around this place to make it into a worthy camp site. Bummer.";

		private string questText_3 = @"[ SHIPWRECK ] 

Tossed far inland by a giant wave, the wreck of the New Erebus is a true landmark of the Eastern fjords. Cpt. Roberts went in search of the Northwest Passage after Franklin, in 1865. While the name of the ship may have been an omen, they were ill-equipped for the journey and were half dead when they landed.

Given their dire circumstances, and the age of the wreck, I wasn't expecting too many finds here. When it came to goods, I was right indeed, as there was nothing usable.


To my delight, however, the captain's cabin is intact and may be used as a shelter, and the entire rest of the ship may be burned for warmth!

Even more miraculously, the log-book of the ship was located in a locked box and had been spared from the elements. Quite a treasure-trove by itself, as I was able to mark another two dots onto my map.";

		private string questText_4 = @"[ OLD RUNWAY CAMPSITE ]

In the days when gold-mining was attempted on the island, one of the northern lakes was used as a make-shift runway. Although it was buried under snow-banks a decade ago, the camp beside it still remains, in a surprisingly good condition.

It takes me a while to clear away the snow in front of the entrance, but the main building yields the following:

 - 1 x RadioPart (Antenna)
 - 5 x Rifle Ammo
 - 2 x Seagull Meat (canned)

Having found the antenna is a cause for great joy, of course. But I'm wary of excessive grins, as there's no guarantee that I'll find any further parts.

This site would be useful as a shelter but for the lack of suitable fire-wood. The gas stoves they originally used have long since been removed by parties unknown. Better luck next time, I hope.

A map in the control office puts two new dots on my map -- the very mines which were using the airfield.";

		private string questText_5 = @" [ SCIENTIFIC EXPEDITION ]

It all makes perfect sense now. The pilot must've air-dropped some mail to the scientific expedition that was camped here several years ago.To study the island's birdlife, as I recall. Too scrawny to waste a bullet on, great flocks of puffins congregate annually on the cliffs around Devil's Anvil.

At first I thought the scientists had taken all their gear back home with them, but underneath a collapsed tent I stumble upon a fantastic discovery: 

 - 1 x RadioPart (Batteries)

Probably some extra batteries for someone's personal radio, left behind in a hurry. Eureka!

Ravaged by the elements, the camp is too damaged to serve as a shelter.

As I prepare to leave, I find a map with C. W.Emerson's old polar camp marked onto it! He was a pioneer in the footsteps of Nansen and Amundsen, but met with tragedy on these blighted plains.";

		private string questText_6 = @"[ OLD COAL MINE ]
            
A long, but mercifully flat trek away from the runway site lies the site of an old coal mine. Upon first glance, it seems an ideal shelter, and that conviction is cemented as I locate a huge oven, custom-made to burn the very coal that's taken from the ground.

The foreman's quarters afford the following loot:

 - 1 x Coal Miner's Jacket
 - 1 x Seal Meat (canned)

The miner's jacket is an improvement over my original anorak, but not by much. The real treasure of this place lies in its use as a shelter, although I do get to etch one new mark onto my map.";

		private string questText_7 = @" [ OLD GOLD MINE ]

Finally I made it around Clear Lake to the old gold mining site. Even though it should be frozen solid, I'm wary of taking any risks with the lake. Getting wet at these temperatures would mean near instant death. 

The mine is not much to write home about. It operated only briefly before closing due to dwindling gold prices.

Nevertheless, I manage the following salvages:

 - 5 x Rifle Ammo
 - 2 x Seal Meat (canned)

The mine contains too little firewood to be used as a shelter. Bummer.

The foreman's old map yields one new notch on my own.";

		private string questText_8 = @" [ SUPPLY CACHE ]

Cpt. Roberts' supply cache took some effort to locate, but I finally found it, tucked under a rocky outcropping of Point Blake. Roberts was facing a mutiny and stored these supplies in case he would be thrown out of his main camp. His intuition proved correct; only his crew ended up killing him instead of mere eviction, leaving the supply cache untouched for the many decades to follow.

Breaking the frost-damaged lock with a single blow, I obtain the following: 

 - 15 x Rifle Ammo
 - 1 x Seaman's Greatcoat

The cache cannot be used a shelter, nor are there any new clues for new locations. Nevermind -- its contents are reward enough by themselves.";

		private string questText_9 = @" [ MUTINEERS' CAMP ]

The mutineers of the New Erebus made their way here after an agonizing march through the wilderness. They made the trip to evade the authorities, but also on account of the copious herds of seals and walruses that dwell on these shores.

The buildings are old and weathered; almost nothing of note remains. Or so I thought, until I stumbled upon this find in a modern shipping container:

 - 1 x Radio Part (Wiring)

The container must've dropped from a passing cargo ship somehow. This is a tremendous stroke of luck, as the wires I found are enough to fix any model of modern radio.

The camp is too old and looted clean to be of use as a shelter -- to my chagrin, as the area would be ideal otherwise.";

		private string questText_10 = @"[ OLD POLAR CAMP ]

Snow-blind and half-dead from the cold, I stumble into the old camp of Cpt. Emerson & Co. Theirs is a sad story. Caught up in a freak blizzard during summer, they only made it back to camp to find the entrance buried under an avalanche. Already weak from the frost, the men died in a heap, trying to dig their way back in with their bare hands.

As I rummage through the bodies, it occurs to me that decency is a luxury affordable to angels alone. Regardless of any ill feelings, my search yields the following:

[REWARD]

Even better, I realize that this camp may be used as a shelter for future excursions, as there's still plenty of fire-wood available.

Oddly enough, no new locations of interest can be found on any of Emerson's old maps.";

		private string questText_11 = @"[ URANIUM MINE ]

It's clear to me upon my arrival that this mining operation must've presented a major challenge. Whoever operated it had to brave 10-15 meter waves leaping across the sheer cliff faces surrounding the place in three directions. The salty water has corroded almost everything, yet a few trinkets linger in the stricken ruins:

 - 1 x RadioPart (Receiver)
 - 10 x Rifle Ammo

// +++ Different descriptions based on whether Sealing Camp has been visited or not";

		private string questText_12 = @"[ NUCLEAR TEST SITE ]

Finally! I never expected to be delighted at the site of radiation signs. While the blasts occurred a long time ago, and my situation leaves me no alternative, I'm very quick in my search of the small bunker.

I knew I wouldn't be disappointed, but this time the catch is truly remarkable:

 - 1 x Special Forces Uniform 
 - 20 x Rifle Ammo
 - 1 x Radio Part (Transmitter)
 - 8 x Seagull Meat (canned)

Atop the mummified remains of Col. Caldwell, I find his old diary and read the final entry:
'Having gathered enough supplies, I'll be heading out in the next few days. As soon as the fever subsides...'

Shivering, I salute what remains of the brave Colonel.";

	}

}



