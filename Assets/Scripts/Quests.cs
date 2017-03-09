using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for determining quest logic.
/// </summary>

namespace QuestNamespace
{

	public class Quests : MonoBehaviour
	{

		/* 
         * Stores all the info relating to quests (pop-up events which occur when hitting various GameObjects). 
         * Attached to: GameObject 'QuestHolder'.
         * Author: Ville Lohkovuori
         */

		// Needed for reference between MonoBehaviours.
		public PlayerStats PlayerStats;
		public PlayerMove PlayerMove;

		// For storing the map icons & labels that are used for the various quests, and their changed versions.
		public Sprite[] questIconSprites;
		public Sprite[] changedQISprites;
		public Sprite[] questLabelSprites;

		// Stores the quests' background images (currently the same one is used for all quests, but it's good to have the list functionality, just in case more are added).
		public Sprite[] questBackgroundSprites;

		// Needed when spawning quest icons and text icons on the map from prefabs.
		public Transform TextIcon;
		public Transform QuestIcon;

		// Needed for internal reference.
		private SpriteRenderer iconRenderer;
		private SpriteRenderer labelRenderer;
		private CircleCollider2D iconCollider;
		private GameObject questHolderHolder;
		private Text questText;
		private Image questImage;

		// Used for setting the completion status of quests...
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

        /// <summary>
        /// Add all the quest booleans and -texts to lists. Assign values to some reference variables. Spawn the initial quests and place names on the map.
        /// </summary>
		void Start ()
		{

			// Add all the quest booleans to a list (for easy manipulation later on).
			// NOTE: The 'inner new List' thingy is copied from the internet, and I'm a bit fuzzy about its internal logic. But it seems to work, so...
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

			// Add all the quest texts to a list (for easy manipulation later on).
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
				questText_12,
				questText_13,
				questText_14
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

			// Spawn text icons on the map (for place names).
			// NOTE: The coordinates are scaled with the canvas, which means that replacing the existing quest labels with plain text would be too tedious for the meager benefit.
			// Therefore I've opted to keep the labels as Sprites, and use the new SpawnTextIcon() method for spawning place names only.
			// NOTE #2: Technically, place names aren't really 'quests', but it makes sense to spawn them here as well.
			SpawnTextIcon ("Half Moon Bay", -182.0f, -457.0f);
			SpawnTextIcon ("Devil's Anvil", -1200.0f, -535.0f);
			SpawnTextIcon ("Clear Lake", -370.0f, 420.0f);
			SpawnTextIcon ("Ore Lake", -990.0f, 320.0f);
			SpawnTextIcon ("Point Hope", -350.0f, -820.0f);
			SpawnTextIcon ("Point Blake", 50.0f, -800.0f);
			SpawnTextIcon ("Western Cliffs", -1230.0f, 0.0f);
			SpawnTextIcon ("Walrus Bay", 510.0f, 100.0f);
			SpawnTextIcon ("Mount Blake", 950.0f, 340.0f);
			SpawnTextIcon ("Eastern Fjords", 950.0f, -530.0f);
			SpawnTextIcon ("Mount Smoke", 520.0f, 370.0f);

		}


        /* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

        /* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   QUEST SPAWN METHODS   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


        /// <summary>
        /// Creates a new quest icon + quest label with the given name in spot (x,y). The sprites are chosen from lists that can be made manually in Unity (just drag & drop the sprites).
        /// </summary>
        // NOTE: It is in all likelihood a VERY bad practice to associate two Arrays with each other in the way I've done it here. Right now, the int 'iconAndLabel_ID' is the
        // ONLY thing that's linking together the chosen quest icons and quest labels. If either Array of Sprites gets 'out of sync', it changes potentially ALL of
        // the associated labels/icons to the wrong ones! I tried using a Dictionary to associate the two different Arrays with each other, but the syntax proved
        // too hard for me atm.
        public void SpawnQuestIcon (string iconName, float x, float y, int iconAndLabel_ID)
		{

			Transform icon = Instantiate (QuestIcon, new Vector3 (x, y, 0.0f), Quaternion.identity);
			icon.name = iconName;
			iconRenderer = icon.GetComponent<SpriteRenderer> ();
			iconRenderer.sprite = questIconSprites [iconAndLabel_ID];

			GameObject questLabel = new GameObject (iconName + "_label");
			questLabel.transform.position = new Vector3 (x, y + 0.42f, 0.0f);
			labelRenderer = questLabel.AddComponent<SpriteRenderer> ();
			labelRenderer.sortingOrder = 3;
			labelRenderer.sprite = questLabelSprites [iconAndLabel_ID];

		}

        /// <summary>
        /// Changes the quest icon to one that indicates that the quest has been completed. The collider is destroyed in order to make the quest non-interactable.
        /// If the icon is tagged as a shelter, then the method changes its icon to the 'shelter' icon, but leaves the collider intact.
        /// </summary>
        // Originally, this method *destroyed* the used quest icon, but it's better to have them stay on the map, to track the player's progress.
        public void ChangeQuestIcon (GameObject givenIcon)
		{
			if (givenIcon.tag != "shelter") {
				Destroy (givenIcon.GetComponent<CircleCollider2D> ());

				PlayerMove.CollidedFlag = false;
				PlayerMove.CollidedName = "";
				print ("No longer collided!"); // debug

				iconRenderer = givenIcon.GetComponent<SpriteRenderer> ();
				iconRenderer.sprite = changedQISprites [0];

				Invoke ("TagNuller", 0.1f);
			} else if (givenIcon.tag == "shelter") {
				iconRenderer = givenIcon.GetComponent<SpriteRenderer> ();
				iconRenderer.sprite = changedQISprites [1];
			}
		}

        /// <summary>
        ///  Spawns text icons (for place names) on the map, in the given coordinates. 
        /// </summary>
        // This had to be done with a Text prefab, because Unity doesn't allow
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

        /* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

        /* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   HELPER METHODS   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


        /// <summary>
        /// Method for getting a quest boolean from outside the class (in Triggerer.cs).
        /// </summary>
        // IMPORTANT NOTE: It seems that the booleans have a different identity after being added to the list; manipulating the original values (Quest1_Completed, etc) does nothing unless their use
        // is explicitly specified beforehand. As the list is quite necessary in a few spots, and using the original values could lead to serious conflicts, these methods should be used
        // at *ALL* times when it comes to altering the values of the quest booleans.
        public bool GetQuestBoolean (int givenNumber)
		{
			return questBooleans [givenNumber];
		}

        /// <summary>
        /// Method for setting a quest boolean from outside the class (in Triggerer.cs).
        /// </summary>
        public void SetQuestBoolean (int givenNumber, bool newValue)
		{
			questBooleans [givenNumber] = newValue;
		}

        /// <summary>
        /// Toggles the 'active' status of the 'QuestHolder' Game Object (and, by proxy, its Image component, which would otherwise block
        /// certain clicks that are needed when dealing with pop-ups.).
        /// </summary>
        public void ToggleQuestHolderActive ()
		{
			questHolderHolder.SetActive (!questHolderHolder.activeSelf);
		}

        /// <summary>
        /// Called in ChangeQuestIcon (). Needed to prevent an issue with the quest sound effect not playing; the tag that triggers the effect was nulled instantly upon collision.
		/// By using the Invoke () method with a delay of 0.1 seconds, the effect has time to trigger before the tag is nulled.
        /// </summary>
		private void TagNuller ()
		{
			PlayerMove.CollidedTag = null;
		}


		/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

		/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   QUEST REWARD LOGIC   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */


		// I wanted to try using enum + a custom namespace, instead of writing a dozen different quest methods (one for each new quest).
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
			Quest_12
		}

        /// <summary>
        /// Method for granting the player quest rewards (called in Triggerer.cs).
        /// </summary>
        public void ChooseQuest (QuestEnum givenQuest)
		{
			// Certain things should be done whenever a quest is triggered; I've put them here on top of the actual quest rewards.

			// Sets the 'quest_Completed' status flag of the quest in question to 'true', preventing the quest from triggering ever again.
			questBooleans [(int)givenQuest] = true;

			// Stops the player from moving on the first click after the quest popup comes up.
			PlayerMove.AllowMove = false;

			// Activates the QuestHolder GameObject (and, by proxy, its script, image and text components).
			// (It's activated only when a quest triggers, because otherwise the image would block the game view.)
			ToggleQuestHolderActive ();

			// Switches the quest's background sprite to the appropriate one.
			questImage.sprite = questBackgroundSprites [(int)givenQuest];

			// Switches the quest's text to the appropriate one.
			questText.text = questTexts [(int)givenQuest];

			// Switches the quest's map icon to the appropriate one.
			ChangeQuestIcon (GameObject.Find (givenQuest.ToString ()));


			// Reward logic, based on the enum defined above.
			switch (givenQuest) {

			// Plane crash
			case QuestEnum.Quest_1:

				Coat OldPilotJacket = new Coat (30.0f);
				PlayerStats.AddToInv (OldPilotJacket, "Old Pilot Jacket");

				PlayerStats.CarriedAmmo += 5;
				PlayerStats.MaxAmmoCheck ();

				FoodItem SeagullMeat = new FoodItem (10);
				PlayerStats.AddToInv (SeagullMeat, "Seagull Meat");

				break;

			// Sealing Camp
			case QuestEnum.Quest_2:

				Coat SealSkinCoat = new Coat (40.0f);
				PlayerStats.AddToInv (SealSkinCoat, "Seal-Skin Coat");

				PlayerStats.CarriedAmmo += 10;
				PlayerStats.MaxAmmoCheck ();

				FoodItem SealMeat = new FoodItem (30);
				PlayerStats.AddToInv (SealMeat, "Seal Meat");
				FoodItem SealMeat2 = new FoodItem (30);
				PlayerStats.AddToInv (SealMeat2, "Seal Meat");

				break;

			// 'New Erebus' shipwreck (s.)
			case QuestEnum.Quest_3:

                    // no reward, other than the new shelter + new map locations

				break;

			// Old Runway Campsite
			case QuestEnum.Quest_4:

				PlayerStats.RadioPartCount += 1;

				PlayerStats.CarriedAmmo += 5;
				PlayerStats.MaxAmmoCheck ();

				FoodItem SeagullMeat3 = new FoodItem (10);
				PlayerStats.AddToInv (SeagullMeat3, "Seagull Meat");
				FoodItem SeagullMeat4 = new FoodItem (10);
				PlayerStats.AddToInv (SeagullMeat4, "Seagull Meat");

				break;

			// Scientific Expedition
			case QuestEnum.Quest_5:

				PlayerStats.RadioPartCount += 1;

				break;

			// Coal Mine (s.)
			case QuestEnum.Quest_6:

				Coat CoalMinersJacket = new Coat (35.0f);
				PlayerStats.AddToInv (CoalMinersJacket, "Coal Miner's Jacket");

				FoodItem SealMeat3 = new FoodItem (30);
				PlayerStats.AddToInv (SealMeat3, "Seal Meat");

				break;

			// Gold Mine
			case QuestEnum.Quest_7:

				PlayerStats.CarriedAmmo += 5;
				PlayerStats.MaxAmmoCheck ();

				FoodItem PolarFoxMeat = new FoodItem (20);
				PlayerStats.AddToInv (PolarFoxMeat, "Arctic Fox Meat");

				break;

			// Supply Cache
			case QuestEnum.Quest_8:

                    // If Uranium Mine (quest 11) has already been activated, change the quest text accordingly.
				if (GetQuestBoolean (10) == true) {
					questText.text = questTexts [12];
				}

				PlayerStats.CarriedAmmo += 15;
				PlayerStats.MaxAmmoCheck ();

				Coat SeamansGreatCoat = new Coat (50.0f);
				PlayerStats.AddToInv (SeamansGreatCoat, "Seaman's Greatcoat");

				break;

			// Mutineers' Camp
			case QuestEnum.Quest_9:

				PlayerStats.RadioPartCount += 1;

				break;

			// Polar Camp
			case QuestEnum.Quest_10:

				PlayerStats.CarriedAmmo += 5;
				PlayerStats.MaxAmmoCheck ();

				FoodItem PolarFoxMeat2 = new FoodItem (20);
				PlayerStats.AddToInv (PolarFoxMeat2, "Arctic Fox Meat");
				FoodItem PolarFoxMeat3 = new FoodItem (20);
				PlayerStats.AddToInv (PolarFoxMeat3, "Arctic Fox Meat");
				FoodItem PolarFoxMeat4 = new FoodItem (20);
				PlayerStats.AddToInv (PolarFoxMeat4, "Arctic Fox Meat");

				Coat SturdyOldGreatCoat = new Coat (60.0f);
				PlayerStats.AddToInv (SturdyOldGreatCoat, "Sturdy Old Greatcoat");

				break;

			// Uranium Mine
			case QuestEnum.Quest_11:

                    // If Supply Cache (quest 8) has already been activated, change the quest text accordingly.
				if (GetQuestBoolean (7) == true) {
					questText.text = questTexts [13];
				}

				PlayerStats.RadioPartCount += 1;

				PlayerStats.CarriedAmmo += 10;
				PlayerStats.MaxAmmoCheck ();

				break;

			// Nuclear Test Site
			case QuestEnum.Quest_12:

				PlayerStats.RadioPartCount += 1;

				PlayerStats.CarriedAmmo += 20;
				PlayerStats.MaxAmmoCheck ();

				Coat SpecialForcesUniform = new Coat (70.0f);
				PlayerStats.AddToInv (SpecialForcesUniform, "Special Forces Uniform");

				for (int i = 0; i < 6; i++) {
					PlayerStats.AddToInv (new FoodItem (10), "Seagull Meat");
				}

				break;
			}
		}

		/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

		/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   UPDATE () + QUEST TEXTS   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  */

        /// <summary>
        /// Logic for closing the quest pop-up upon mouse-click / tap.
        /// </summary>
		void Update ()
		{
			// Closes the quest popup upon mouse-click / tap. (There must be a better way to do this, but this will do for now).
			// The 'AllowMove' flag is used to stop the click from moving the Player on the map.
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetMouseButtonDown (0)) {
				questText.text = ""; // unnecessary, I guess, but meh, might as well keep it
				ToggleQuestHolderActive ();
				PlayerMove.AllowMove = true;
			}
		}

		// All the quest texts are stored here at the bottom of the file, to reduce needless scrolling.
		// There must be a better way to store long strings (database?), but I'd rather spend the time on something else. Ugly as this is, it seems to work for our purposes.
		private string questText_1 = @"[ PLANE CRASH ]

Lying high over a vast plain of shimmering ice, the orange postal plane is visible from miles away. The crash occurred only a few years ago. The pilot's corpse lies frozen in the cockpit, a serene expression on what remains of his face. 

Having removed his jacket, I pry open the small mail bag. His fingers make a funny sound as they snap in two. 

In total I gather the following: 

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
 - 2 x Seal Meat (canned)

I'm surprised by the lack of any radio parts. I wonder if someone else has salvaged them before me -- and if so, where did they head from here?

While the buildings are in a fair enough condition, and there's fire-wood available, the terrain is too rough around this place to make it into a worthy camp site. Bummer.";

		private string questText_3 = @"[ SHIPWRECK ] 

Tossed far inland by a giant wave, the wreck of the New Erebus is a true landmark of the Eastern fjords. Cpt. Roberts & Co. went in search of the Northwest Passage after Franklin, in 1865. While the name of the ship may have been an omen, they were ill-equipped for the journey and were half dead when they landed.

Given the crew's dire circumstances, and the age of the wreck, I wasn't expecting too many finds here. When it comes to goods, I was right indeed, as there was nothing usable.


To my delight, however, the captain's cabin is intact and may be used as a shelter, and the entire rest of the ship may be burned for warmth!

Even more miraculously, the log-book of the ship was stowed away in a locked metal box and had been spared from the elements. Quite a treasure-trove by itself, as I was able to mark another two dots onto my map.";

		private string questText_4 = @"[ OLD RUNWAY CAMPSITE ]

In the days when gold-mining was attempted on the island, one of the northern lakes was used as a make-shift runway. Although buried under snow-banks a decade ago, the camp beside it still remains, in a surprisingly good condition.

It takes me a while to clear away the snow in front of the entrance, but the main building yields the following:

 - 1 x Radio Part (Antenna)
 - 5 x Rifle Ammo
 - 2 x Seagull Meat (canned)

Finding the antenna would've made me smile, were my lips less numb from the cold.

This site would be useful as a shelter but for the lack of suitable fire-wood. The gas stoves they originally used have long since been removed by parties unknown.

A map in the control office puts two new dots on my own -- the very mines which were using the airfield.";

		private string questText_5 = @"[ SCIENTIFIC EXPEDITION ]

It all makes perfect sense now. The pilot must've air-dropped some mail to the scientific expedition that was camped here several years ago. To study the island's birdlife, as I recall. Too scrawny to waste a bullet on, great flocks of rainbow puffins congregate annually on the cliffs around Devil's Anvil.

At first I thought the scientists had departed with all their gear, but underneath a collapsed tent I stumble upon a fantastic find: 

 - 1 x RadioPart (Batteries)

Probably some extra batteries for someone's personal radio, left behind in a hurry. They glitter like little nuggets of gold in my frostbit hands.

As if my luck could never end, upon preparing to leave, I find a map with C.W. Emerson's old polar camp marked onto it! He was a pioneer in the footsteps of Nansen and Amundsen, but met with tragedy on these blighted plains.";

		private string questText_6 = @"[ OLD COAL MINE ]
            
A long, but mercifully flat trek away from the runway lies the site of an old coal mine. Upon first glance, it appears an ideal shelter, and that conviction is cemented as I locate a huge oven, custom-built to burn the very coal that's taken from the ground.

The foreman's quarters afford the following loot:

 - 1 x Coal Miner's Jacket
 - 1 x Seal Meat (canned)

While the miner's jacket is a sturdy piece, it is by no means ideal. The real treasure of this place lies in its use as a shelter, although I do get to etch one new mark onto my map.";

		private string questText_7 = @"[ OLD GOLD MINE ]

Finally I made it around Clear Lake to the old gold mining site. Even though it should be frozen solid, I'm wary of taking any risks with the lake. Moisture in these temperatures spells a very swift, almost instant death. 

The mine is not much to write home about. It operated only briefly before closing due to dwindling gold prices.

Nevertheless, I manage the following salvages:

 - 5 x Rifle Ammo
 - 2 x Arctic Fox Meat (canned)

The mine contains too little firewood to be used as a shelter. Bummer.

The foreman's old map yields one new notch on my own.";

		private string questText_8 = @"[ SUPPLY CACHE ]

Cpt. Roberts' supply cache took some effort to locate, but I finally found it, tucked under a rocky outcropping at Point Blake. 

Roberts was facing a mutiny and stored these supplies in case he'd be thrown out of his main camp. His intuition proved correct; however, he failed to foresee that his crew would end up killing him instead of mere eviction, leaving the cache untouched for all this time.

Breaking the frost-damaged lock with a single blow, I obtain the following: 

 - 15 x Rifle Ammo
 - 1 x Seaman's Greatcoat

How curious..! There's a hand-written note here, left by a US military Colonel named Caldwell: 

'Only took what I needed. Stranded on island for 2 months now. Not at liberty to disclose shelter location. Good luck to whoever finds this, and godspeed.'";

		private string questText_9 = @"[ MUTINEERS' CAMP ]

The mutineers of the New Erebus made their way here after an agonizing march through the wilderness. They made the trip to evade the authorities, but also on account of the copious herds of seals and walruses that dwell on these shores.

The buildings are old and weathered; almost nothing of note remains. Or so I thought, until I stumbled upon this find in a broken-down, modern shipping container:

 - 1 x Radio Part (Wiring)

The container must've dropped from a passing cargo ship! This is a tremendous stroke of luck, as the wires are enough to fix any model of modern radio.

The camp is too old and looted clean to be of use as a shelter -- to my chagrin, as the area would be ideal otherwise.";

		private string questText_10 = @"[ OLD POLAR CAMP ]

Snow-blind and half-dead from the cold, I stumble into the old camp of Cpt. Emerson & Co. Theirs is a sad story. Caught up in a freak blizzard during summer, they only made it back to camp to find the entrance buried under an avalanche. Already weak from the frost, the men died in a heap, trying to dig their way back in with their bare hands.

As I rummage through the bodies, it occurs to me that decency is a luxury affordable to angels alone. Regardless of any ill feelings, my search yields the following:

 - 1 x Sturdy Old Greatcoat (Emerson's)
 - 3 x Arctic Fox Meat (canned)
 - 5 x Rifle Ammo

Even better, I realize that this camp may be used as a shelter for future excursions, as there's still plenty of fire-wood available.

Oddly enough, no new locations of interest can be found on any of Emerson's old maps. Perhaps I should search further to the South or West?";

		private string questText_11 = @"[ URANIUM MINE ]

It's clear to me upon arrival that this mining operation must've presented a major challenge. Whoever operated it had to brave 10-15 meter waves leaping across the sheer cliff faces surrounding the place in three directions. The salty water has corroded almost everything, yet a few trinkets linger in the stricken ruins:

 - 1 x Radio Part (Receiver)
 - 10 x Rifle Ammo

A most curious find! An note from a US army Colonel by the name of Caldwell:

'The salvage effort has proved itself useless. Full report next month. Departed NE for X-site at 0600 hours. Leaving behind a radio as a safety precaution.'

Apparently, he and his men have visited the mine to see if there's any leftover uranium there for whatever purposes. Having found none, they departed towards the Northeast. With some more info, I might be able to pin-point the so-called 'X-site's location!";

		private string questText_12 = @"[ NUCLEAR TEST SITE ]

I never expected to be delighted at the sight of radiation signs! While the blasts occurred some time ago, and my situation leaves me no alternative, I'm very quick in my search of the small bunker.

I knew I wouldn't be disappointed, but this time the catch is truly remarkable:

 - 1 x Special Forces Uniform 
 - 20 x Rifle Ammo
 - 1 x Radio Part (Transmitter)
 - 6 x Seagull Meat (canned)

Among the mummified remains of Col. Caldwell, I find his old diary, reading the final entry:

'Having gathered enough supplies, I'll be heading out in the next few days. As soon as the fever subsides ...'

Shivering, I salute what remains of the brave Colonel.";

		// These are alternative texts for quests 8 and 11, based on various conditions.
		private string questText_13 = @"[ SUPPLY CACHE ]

Cpt. Roberts' supply cache took some effort to locate, but I finally found it, tucked under a rocky outcropping at Point Blake. 

Roberts was facing a mutiny and stored these supplies in case he'd be thrown out of his main camp. His intuition proved correct; however, he failed to foresee that his crew would end up killing him instead of mere eviction, leaving the cache untouched for all this time.

Breaking the frost-damaged lock with a single blow, I obtain the following: 

 - 15 x Rifle Ammo
 - 1 x Seaman's Greatcoat

What a marvellous find! A note by the same Col. Caldwell who visited the uranium mine! He must've been stranded on the island after the nuclear tests somehow. With this new knowledge of the reach of his travels, along with the bearing of his departure from the mine, I should be able to pin-point the 'X-site's general location!";

		private string questText_14 = @"[ URANIUM MINE ] 

It's clear to me upon arrival that this mining operation must've presented a major challenge. Whoever operated it had to brave 10-15 meter waves leaping across the sheer cliff faces surrounding the place in three directions. The salty water has corroded almost everything, yet a few trinkets linger in the stricken ruins:

 - 1 x RadioPart (Receiver)
 - 10 x Rifle Ammo

While the new radio part makes me shiver with joy (to bring a change from the cold), the real treasure of this site is another note by Col. Caldwell: 

'The salvage effort has proved itself useless. Full report next month. Departed NE for X-site at 0600 hours. Leaving behind a radio as a safety precaution.'

Assuming his party kept going Northeast, I now have a general location for Col. Caldwell's 'X-site', based on this note and his earlier one!";

	}

}
