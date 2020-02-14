using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DFA : MonoBehaviour
{
    TextMeshProUGUI[] content;
    Scrollbar scroll;

    int LOCATION_INDEX = 0;
    int DESCRIPTION_INDEX = 1;

    bool updated;

    #region Bear skills, used for if statements
    //Skills directly related to bear
    readonly string DETECT_HONEY = "Sun Bear";
    readonly string CLIMB = "Black Bear";
    readonly string TERRIFY = "Grizzly Bear";
    readonly string SWIM = "Polar Bear";
    readonly string EAT_BAMBOO = "Panda Bear";
    readonly string CARNAGE = "Honey Badger";

    //Skills related to the rolls
    readonly string MUSCLE = "Muscle";
    readonly string BRAINS = "Brains";
    readonly string DRIVER = "Driver";
    readonly string HACKER = "Hacker";
    readonly string THIEF = "Thief";
    readonly string FACE = "Face";
    #endregion

    #region Location Text Variables
    readonly string TRAP = "NEEDS WORK";
    readonly string LOSE_TEXT = "YOU LOST!!!";
    readonly string LOCATION_1 = "Brambleberry Forest";
    readonly string LOCATION_2 = "Barefoot Lake";
    readonly string LOCATION_3 = "Road to Town";
    readonly string LOCATION_4 = "Lilliput Gates";
    readonly string LOCATION_5 = "Lilliput Town Center";
    readonly string LOCATION_6 = "B. Arthur's Books";
    readonly string LOCATION_7 = "Krack-Inn";
    readonly string LOCATION_8 = "The Vault";
    readonly string LOCATION_9 = "YOU WIN";
    #endregion

    #region State Text Variables
    readonly string CONSTRUCT_TEXT = "Pardon the dust, we are making this!\n\n"+
        "Press any key to go back to the main menu";

    readonly string START_TEXT ="It’s October, and the summer air has crisped to that quintessential Autumn breeze. The light shines through the forest green, with pockets of amber and crimson through the leaves. Slivers of light illuminate the patches of soft vivid green moss on the ground: and the light winds create a kaleidoscopic vision of greens, reds, and gold. The sweet smell of grass, vines, and humus is present, and so is the sound of all the creatures in the forest. The air is alive with chitters, songs, and chirps."+
            "\n\n Any key to continue";
    
    readonly string START1_TEXT = "As you inspect the clearing of this forest, you hear the sound of rustling coming from the other side of a bush. A brown bear walks into view. This bear ambling through the forest seems to have seen better days, and a few angry village folk. It has matted hair, with stick and leaves stuck to it's legs and its head. One of it's eyes is noticeably bigger than the other, giving this bear a deranged look.\n\n"+
            "T - Talk to the bear\n"+
            "F - Follow the bear\n"+
            "O - Go the other way";

    readonly string TALK_OZMODEUS_TEXT = "The bear begins to speak. \n\n\""+
            "Ozzy smelt all the honey down at the village. Ozzy tried to get in, but evil men with big guns smash Ozzy nose, now Ozzy can't find honey!\"\n\n"+
            "The bear sniffs around again\n\n"+
            "\"So much honey down in village. Need to get in! And take it from evil  men. Will you help Ozzy? Ozzy going to find fish now, Ozzy needs num nums. If only Ozzy not look like Ozzy...more like hooman--\"\n\n"+
            "And with that, the bear returns to what he was doing.\n\n"+
            "F - Follow Ozzy\n"+
            "O - Head the other way";

    readonly string FOLLOW_TEXT_HO = "The forest opens to a clearing. The sound of crashing water fills the area and so does the smell of its mist. The ground transitions from grass to rocks as it reaches the lake. Barefoot lake is a pretty sizeable lake, which feeds water to Lilliput, the fishing village down towards the bottom of the mountain. It serves as the last lake before reaching the village, and then the ocean. The space is domineered by a high cliff that features a waterfall.\n\n"+
            "The bear can be seen searching the pond for fish. You can hear the sound of a beehive close by.\n\n"+
            "T - Talk to the bear\n"+
            "S - Search for the beehive\n" + 
            "H - Head to Town";

    readonly string FOLLOW_TEXT_O ="The forest opens to a clearing. The sound of crashing water fills the area and so does the smell of its mist. The ground transitions from grass to rocks as it reaches the lake. Barefoot lake is a pretty sizeable lake, which feeds water to Lilliput, the fishing village down towards the bottom of the mountain. It serves as the last lake before reaching the village, and then the ocean. The space is domineered by a high cliff that features a waterfall.\n\n"+
            "The bear can be seen searching the pond for fish.\n\n"+
            "T - Talk to the bear\n" +
            "H - Head to Town";


    readonly string FOLLOW_TEXT_H = "The forest opens to a clearing. The sound of crashing water fills the area and so does the smell of its mist. The ground transitions from grass to rocks as it reaches the lake. Barefoot lake is a pretty sizeable lake, which feeds water to Lilliput, the fishing village down towards the bottom of the mountain. It serves as the last lake before reaching the village, and then the ocean. The space is domineered by a high cliff that features a waterfall.\n\n"+
            "The bear can be seen searching the pond for fish. You can hear the sound of a beehive close by.\n\n"+
            "S - Search for the beehive\n"+
            "H - Head to Town";

    readonly string FOLLOW_TEXT = "The forest opens to a clearing. The sound of crashing water fills the area and so does the smell of its mist. The ground transitions from grass to rocks as it reaches the lake. Barefoot lake is a pretty sizeable lake, which feeds water to Lilliput, the fishing village down towards the bottom of the mountain. It serves as the last lake before reaching the village, and then the ocean. The space is domineered by a high cliff that features a waterfall.\n\n"+
            "The bear can be seen searching the pond for fish. You can hear the sound of a beehive close by.\n\n"+
            "H - Head to Town";
    readonly string LAKE_HONEY_PASS_TEXT = "You find the beehive live and teeming with activity hanging precariously off a branch high above your head. It is possible to climb the tree that is holding the branch.\n\n"+
        "C - Climb the tree\n" + 
        "R - Return";

    readonly string LAKE_HONEY_HONEY_P = "You easily climb up the tree, and make it to the branch holding the beehive. You balance and the branch bows under your weight, but you make it to the be hive. Easily enough, you grab the beehive and crack it open. You find a great serving of honey. \n\n"+
        "HONEY HAS BEEN ADDED TO YOUR INVENTORY!!\n\n"+
        "Press [I] whenever you want to inspect your inventory.\n\n"+
        "R - Return";

    readonly string LAKE_HONEY_HONEY_F = "You begin to climb the tree precariously. You slip a little, but you make it to the branch holding the beehive. The branch sags a little under your weight. As you move forward, the branch begins to sag more and more. You get to the beehive, and reach down for it. Suddenly, the branch snaps, and you fall to the ground. You're okay, but the beehive is smashed to a goop. There are angry bees stinging you\n\n"+
        "YOUR FRUSTRATION MAKES YOU MORE LIKE A BEAR\n\n"+
        "R - Return";

    readonly string LAKE_HONEY_FAIL_TEXT = "You search, but you can't seem to find the beehive.\n\n" +
    "R - Return.";

    readonly string OTHERWAY_TEXT = "The trail to Lilliput is a picturesque dirt road with massive trees along the way. Lining the dirt path is a bunch of bushes and wildflowers in full bloom. The air is buzzing with activity: dragonflies, butterflies, and bees buzz about.  As the sun rises, it breaks through the trees and fully illuminates the road, casting shadows through the leaves every so often. There is a natural path that runs parallel to the road, it’s a bit more obscured.\n\n"+
        "The sound of a cart being pushed down the road is heard by the bears. Upon coming into view, it is a small hand cart topped with a mountain of hats in different styles and colors. There is also the sound of someone humming to themselves.\n\n"+
            "H - Hide\n" +
            "C - Continue down the path";

    readonly string OTHERWAY_H_TEXT = "A bit cautious of the approaching creature, you hide in the overgrowth with a visual on the road. You see a man with small tufts of white hair on the sides of his head, and a great bald spot on the top that visciously reflects the sun. His eyes appear to be three sizes two big due to the glasses he wears. He is pushing a very crude looking cart towards town. The cart is overfilled with a mountain of what looks like home made hats.\n\n"+
        "There is a snap of some twigs as you adjust your weight in the bushes. There is a moment where the man looks in your direction, but he hits a bump in the road and he gives a little shout as the cart flips over.\n\n"+
        "H - Help the man\n"+
        "A - Attack the man";

    readonly string OTHERWAY_C_TEXT = "You pay no mind to the approaching person. As they get closer, it sounds as if they do not intend on stopping. There is a small cry out, and a CRASH behind you.\n\n"+
        "As you turn around, you see a man with small tufts of white hair on the sides of his head, and a great bald spot on the top that visciously reflects the sun. His eyes appear to be three sizes two big due to the glasses he wears. The cart he was pushing is sprawled on the side of the road and there are hats everywhere.\n\n"+
        "H - Help the man\n" +
        "A - Attack the man";

    string ATTACK_MATTY_P;

    readonly string ATTACK_MATTY_F = "The man is crawling around on the ground with his hands outstretched, looking for his glasses. His distraction is your opprotunity. You slowly creep on him, and trip over the cart, gaining his attention. He turns towards you and says\n\n"+
		"\"OH! Hello there young whipper snapper, could you help me pick my cart back up?\n\n"+
		"You begin to try and speak to this old man, but nothing but bear noises come out. So you then try to give an approving tone in your growling.\n\n"+
		"\"You should probably get that checked out,that sounds like an awful cold your getting.\"\n\n"+
		"H - Help";

    string HELP_MATTY_TEXT;

    readonly string LILLIPUT_GATES_TEXT = "The road leads up to a massive stone wall with an archway, manned by two guards wearing black and yellow uniforms and currently carrying rifles. On the wall there is a big sign that shows the symbol of a bear, with a bright red circle and a slash, indicating that bears will not be permitted in the city.\n\n"+
		"F - Find a Disguise\n"+
		"A - Approach Guards";

    readonly string LILLIPUT_GATES_D = "The road leads up to a massive stone wall with an archway, manned by two guards wearing black and yellow uniforms and currently carrying rifles. On the wall there is a big sign that shows the symbol of a bear, with a bright red circle and a slash, indicating that bears will not be permitted in the city.\n\n"+
		"A - Approach Guards";

    readonly string FIND_DISGUISE_TEXT_P = "Looking around, you find a pair of pants in the bushes and an extremely wrinkled shirt near by on the ground. The pants are stiff and caked in dirt, and the shirt is torn in some places with pooling red stains around. You put on the shirt and the pants. The pants are a little too small for you and as you walk, you can hear the fibers straining to remain intact. The shirt, on the other hand, is rather big on you and hangs to the middle of your massive bear thighs.\n\n"+
		"A - Approach Guards";

    readonly string FIND_DISGUISE_TEXT_F = "The area around seems to be clear of anything that can be used as a disguise. Maybe another area will have something suitable.\n\n"+
		"S - Search around \n"+
		"A - Approach the Guards";

    readonly string APPROACH_GUARDS_TEXT = "You lumber up to the archway. As you approach, the guards become weary of your presence. One of the guards begins to yell.\n\n"+
		"\"OYE! SHOO BEAR!\" The guard is waving his hand around as the other guard readys the rifle and aims at you. You walk closer as a small bang echoes through the clearing. You look down and see red feathers connected to a shiny tube sticking out of your chest. You then look straight ahead and see the blue skies, and feel a muffled thud. The world goes black.\n\n"+
		"You wake up some time later, further away from the town on the dirt road. You get up, shake off the sleep, and head back to the gates of Lilliput\n\n" +
		"C - Lilliput Gates";

    readonly string APPROACH_GUARDS_D = "You lumber to the archway. As you approach, the guards become aware of your presence. You see them take a slight step back as you approach.\n\n"+
		"\"Good...day.\" One of the guards tell you, hesitantly.\n\n"+
		"You nod back, tip your hat, and continue through the archway into Lilliput.\n\n"+
		"YOU SUCCESSFULLY ENTERED LILLIPUT, YOU GAIN A POINT FOR CRIMINAL, AND A CRIMINAL FLASH BACK (PRESS \"I\" TO VIEW)\n\n"+
		"C - Continue";

    readonly string TOWN_CENTER_TEXT_E = "Lilliput is a town that is centered around a main road that goes straight down towards the center. The road is lined with houses and shops that boast different types of honeys. There are signs that declare the presence of Honey Con, the local honey convention, taking place in the main plaza of town. On the way to the center of town, the scent of honey becomes more noticable. It is clear to see that the plaza is where the convention is, due to the number of people, the pop up stalls for sampling honey and the grand stage and podium that block the view of the docks behind them.\n\n"+
		"E - Explore\n"+
		"C - Continue";

    readonly string TOWN_CENTER_TEXT = "Lilliput is a town that is centered around a main road that goes straight down towards the center. The road is lined with houses and shops that boast different types of honeys. There are signs that declare the presence of Honey Con, the local honey convention, taking place in the main plaza of town. On the way to the center of town, the scent of honey becomes more noticable. It is clear to see that the plaza is where the convention is, due to the number of people, the pop up stalls for sampling honey and the grand stage and podium that block the view of the docks behind them.\n\n"+
		"C - Continue";

    readonly string LILLIPUT_TENTS_TEXT = "Walking around the central park area, you find that the many tents are sampling various types of honeys. You go around to each of the tents and get a sample from each. Altogether, you have quite a bit of honey. There is also a tent that has voting booths.\n\n" +
		"YOU NOW HAVE A BUNCH OF HONEY IN YOUR INVENTORY (PRESS \"I\" TO VIEW)\n\n"+
		"C - Continue";

    readonly string TOWN_CENTER_TEXT_1 = "As you are taking in the scenery, the crowd begins to roar in applause as a dumpy man in a blue suit parades across the stage. He is followed by a tall and muscular man with angular features. The dumpy man takes the stage and begins to speak.\n\n"+
		"\"Welcome to the annual Honey Con of 2019!\"\n\n"+
		"The crowd erupts in applause again.\n\n"+
		"\"Thank you, thank you. It was all my idea. I created this convention, and in my 73 years alive, I have never had a better idea. The convention began back in 1902 when the bees in this area..\" he drones on.\n\n"+
		"C - Continue";
        
    readonly string TOWN_CENTER_TEXT_2 = "As the dumpy man was talking and through the ooh and ahhs of the audience, the tall man next to the dumpy man was looking around the audience, as if looking for something. His eyes meet yours, and there seems to be a moment of understanding. He smiles at you, in a way that makes you feel uneasy in your disguise, and he continues to survey the audience.\n\n"+
		"\"...and the winner will get this years grand prize, a massive vat of honey!\" The dumpy man continues and pats a massive black cauldron on stage. When he pulls his hand away, a dribble of honey extends from the cauldron to his hand. He swats at it, and tastes the honey and gives a displeased face, which quickly gives way to a happier face.\n\n"+
		"\"It's a bit bitter at first, but then its sweet.\" He states. \"Now my assistant, Rafaj,\" the tall man nods,\"will be signing contenstants up for this years Miss Honey Beauty Paegant.\"\n\n"+
		"C - Continue";
        
    readonly string TOWN_CENTER_TEXT_3 = "To the sound of the audience's appluase, the dumpy man leaves the stage. You see a few guards wheel the cauldron of honey off of the stage and towards a building directly behind the park. They take it to a place where the wall splits. The green light blinks as the doors open, and once the honey is wheeled inside, the door closes. The green light now is red.\n\n"+
		"\"That's a nifty bank feature.\" a voice says from next to you. You turn to look and find two guards speaking to each other.\n\n"+
		"\"Yeah, this year they went all out on security. We have those security cameras around town, and the vault. Nobody is going to be able to steal the honey.\"\n\n"+
		"\"Is it Mike watching the cameras? Over at B. Arthur's Books?\"\n\n"+
		"\"I think so. Hey, you getting hungry? I'm starving!\"\n\n"+
		"\"Ehh, I could eat.\" The guards then walk off.\n\n"+
		"C - Continue";

    readonly string TOWN_CENTER_TEXT_4 = "You stand in the center of the park with the knowledge of a massive vat of honey being stored in the vault, a keycard that opens the door to the vault, and the location of the security system. The plan begins to form in your head.\n\n"+
		"B - Head to B. Arthur's Book Store";

    readonly string BOOKS_TEXT = "This book shop is made of alabaster limestone with some stone work to make it stand out a little bit. It is slightly recessed, with a big window up front. The window has a painting of a bee with frizzy gray hair and an unamused expression. Next to this picture is the name of the shop. The inside can be seen in between the spaces of the letters.\n\n"+
		"The interior of the shop is dim with some recessed lights that point downward to cast light on chairs for readers.At the counter, Sits a young man reading a thick book with glossy pages. He has bushy red hair, freckles that populate his cheeks and the bridge of his nose , and square glasses that threaten to fall off of his face.\n\n"+
		"T - Talk to the young man\n"+
		"E - Explore\n"+
		"R - Return to town center";

    readonly string BOOKS_TEXT_E = "This book shop is made of alabaster limestone with some stone work to make it stand out a little bit. It is slightly recessed, with a big window up front. The window has a painting of a bee with frizzy gray hair and an unamused expression. Next to this picture is the name of the shop. The inside can be seen in between the spaces of the letters.\n\n"+
		"The interior of the shop is dim with some recessed lights that point downward to cast light on chairs for readers.At the counter, Sits a young man reading a thick book with glossy pages. He has bushy red hair, freckles that populate his cheeks and the bridge of his nose , and square glasses that threaten to fall off of his face.\n\n"+
		"T - Talk to the young man\n"+
		"R - Return to town center";

    readonly string BOOK_EXPLORE_TEXT = "You search around the book shop and find an empty table with a book open and a steaming cup of tea. Upon sniffing the tea, you can smell the massive amount of honey in the tea. You take the cup and close the book.\n\n"+
		"T - Take the tea cup\n"+
		"R- Return to entrance";

    readonly string TAKE_T_CUP_P = "You look around and the coast seems clear. You slink around the table, and swipe the cup. It is warm in your hands, and you can smell the sweet sweet honey that is caked to the bottom of the cup.\n\n"+
		"YOU NOW HAVE A BUNCH OF HONEY IN YOUR INVENTORY (PRESS \"I\" TO VIEW)\n\n"+
		"YOU HAVE SUCCESFULLY STOLEN AN ITEM, MOVING A POINT TO CRIMINAL\n\n"+
		"YOU NOW HAVE A CRIMINAL FLASHBACK IN YOUR INVENTORY (PRESS \"I\" TO VIEW)\n\n"+
		"R - Return";
        
    readonly string TAKE_T_CUP_F = "You look around and the coast seems clear. You sling around the table and swipe the cup. In an instant, you see the cup leave the table, float for a second before it falls to the ground. A loud shattering is heard through the books shop, and a couple of the patrons look in your direction.\n\n"+
		"YOUR FAILURE FRUSTRATES YOU, MOVING A POINT TO BEAR\n\n"+
		"R - Return";

    readonly string BOOKS_TALK_TEXT = "\"Hello sir..and or madam!\" The man behind the counter says to you. He speaks with a severe lisp and showers you with nearly every word that comes out of his mouth. \"My name is Carl. How can I help you?\"\n\n"+
		"You look down at the book he is currently reading. It has big glossy white pages, with pictures of a very peculiar bug. You point to the image.\n\n"+
		"\"Oh! I'm reading about the Wynona Water Beatle! It's facinating! Did you know that...\" he drones on about the bug.\n\n"+
		"As he speaks, you notice a guard walk from the front of the store, towards the back. Knock a couple of times on the door, and another guard emerges from the room. It appears they just changed shifts.\n\n"+
		"\"... and that's why they are my favorite insectoid among the 	Lethocerus!\"\n\n"+
		"You smile at him.\n\n"+
		"R - Take a step back\n"+
		"B - Head towards the back";

    readonly string SECURITY_ROOM_TEXT = "You head to the back of the shop and go to the door that you saw the guard go into. You knock a couple of times, the same way you saw the guard knock. In a second, the other guard sticks his head out.\n\n"+
		"Without hesitation, you push him back into the small space and slash at him. The guard takes a fatal blow to the chest and red paints the walls behind you.\n\n"+
		"After the battle, you take a look at the room. It is small with a couple of monitors on the wall. They show the festivities in Lilliput, as well as the enterance of a building that looks like a ship. You see the guard who opened the vault with the key card head into that building.\n\n"+
		"H - Hack\n"+
		"F - Follow Key Card";

    readonly string HACK_P = "You sit at the controls of the security system and begin typing commands on the computer. After a few minutes, you look up at the computer screen and see a guard walk across one of the monitors. After a minute, the same guard walks across the monitor.\n\n"+
		"YOU SUCCESSFULLY HACKED THE SECURITY SYSTEM, MOVE A POINT OVER TO CRIMINAL.\n\n"+
		"YOU NOW HAVE A NEW CRIMINAL FLASH BACK IN YOUR INVENTORY (PRESS \"I\" TO VIEW)\n\n"+
		"F - Follow Key Card";

    readonly string HACK_F = "You sit at the controls of the security system and begin to pound the keyboard. Keys begin to fly off of the keyboard. You look up and see nothing has changed. You enter a crazed state and slash the monitors. Sparks fly over the entire room as deeply scarred monintors decorate the wall.\n\n"+
		"YOUR FRUSTRATION HAS MADE YOU MORE LIKE A BEAR, MOVING A POINT TO BEAR\n\n"+
		"F -Follow Key Card";

    readonly string KRACK_INN_TEXT = "The exterior of the inn is shaped and styled as the front of a ship that has been wrecked and was sitting at the bottom of the sea for some time. Tentacles wrap around it, coming from different cracks in the façade. The exterior is made from distressed and blackened wood, with portholes placed along an even interval. The front of the ship façade is broken away, which makes way for the door in an alcove. The second floor has a private balcony attached to the suite at the front and serves as an aesthetic deck.\n\n"+
		"The interior is dimly lit and also made from wood. The downstairs is a tavern with a bar along the back wall. There is a cut out window for orders to come through and a swinging door to get to the kitchen. The sounds of chopping, pans moving about, and conversation pour through as the chefs prepare for later. There are tables around the tavern with a few guards drinking about. Some are clustered in groups of 3 or 4, others are alone.\n\n"+
		"You see the Guard with the keycard sitting alone, with his head on the table.\n\n"+
		"A - Approach Guard";

    readonly string KRACK_INN_TEXT_1 = "You approach the guard. He seems to be sleeping. You can see the key card dangling out of his pocket.\n\n"+
		"T - Take the card.";

    readonly string KRACK_INN_TEXT_2 = "You reach for the card, delicately grabbing the plastic. With great caution, you begin to pull the card out of the pocket. It comes out, to your great happiness. You turn and begin to walk away, when there comes a great tug on the card, followed by a loud \"OOF\" and the sound of someone hitting the ground.\n\n"+
		"Upon further inspection, the card is attached to a string, that is then attached to the guard. He is beginning to stir off of the ground. You let go of the card as it zips back to his pocket.\n\n"+
		"\"Hehehe...I guess I drank a little too *hiccup* much\" The guard says, as he rights himself and sits down at the table.\n\n"+
		"A - Approach the guard";

    readonly string KRACK_INN_TEXT_3 = "You approach the guard. He regards you through half open eyes, and a swaying posture. He seems to be dancing to unheard music.\n\n"+
		"\"Hey! wusgoiiin onn??\" He asks you. \"I'm doin fiiine. Ivegot the best jaahb in the woorld\" He laughs and hiccups. \"You see,\" he leans in dangerously close. You can smell the honey whiskey on his breath. \"I haave a kard dat lessme open the vahlt\" He hiccups.\n\n"+
		"\"Yeaaah, you can thinkof me as a pree improtant duude...you look kinda cuute.\" He laughs and then sighs, and puts his face into his hands, embarassed. \"Wuz I beeeing to for-\" he trails off and begins snoring.\n\n"+
		"T - Take the card";

    readonly string KRACK_INN_TEXT_4 = "You have acquired the keycard to the vault. Now, it is time to steal your vat of honey.\n\n"+
		"V - Head to the Vault";

    readonly string VAULT_TEXT = "The trapezoidal building is made mostly of ornate white stone with golden detailing around the edges of the massive pillars that hold up the alcove. The Vault is the official bank of Lilliput and features large shiny windows that allow for natural sunlight to illuminate every corner of the building.\n\n"+
		"E - Enter the Vault\n";

    readonly string VAULT_TEXT_T = "The trapezoidal building is made mostly of ornate white stone with golden detailing around the edges of the massive pillars that hold up the alcove. The Vault is the official bank of Lilliput and features large shiny windows that allow for natural sunlight to illuminate every corner of the building.\n\n"+
		"As you make your way to the entrance of the bank, you hear a soft click. You look over and see a guard staring at you at the end of a rifle. You hear a click\n\n"+
		"D - Dodge";

    readonly string VAULT_GUARD_P = "You see the red feathers on the end of the dart coming towards you. You swiftly duck out of the way and gather yourself, as the guard looks stunned that you were able to dodge the dart. He fumbles with the rifle.\n\n"+
	"You run towards him and swipe your paw, connecting with his face. Your claws sinking into his flesh as it comes undone, like three zippers, across his face. He falls to the floor as red pools under his body. You proceed to enter the Bank.\n\n"+
	"C - Continue";

    readonly string VAULT_GUARD_F = "You heard the click, and look puzzled as you feel a small twinge of pain on your leg. You look down, and see a plume of red feathers sticking out of the pants you are wearing.\n\n"+
	"You begin to walk towards the guard, but your legs feel heavy. Everything begins to blur, and you suddenly become dizzy and extremely tired. You land on the ground, and a cloud of dust obstructs your vision. You see the guard beginning to walk over to you.\n\n" + 
	"C - Continue";

    readonly string VAULT_TEXT_1 = "You enter the bank. The interior of the building is just as immaculate as the exterior. The walls a stark white, with dark wooden detailing around the entire room. To the back of the bank, off to the right is the teller area. It is all empty due to this being used as the vault. To the right there is a seating area. And in one of the seats, there is a guard, wearing opaque black sun glasses, and a crimson scarf.\n\n"+
	"If he sees you, he has made no indication of noticing your presence.\n\n"+
	"A - Approach Guard";

    readonly string VAULT_TEXT_2 = "You slowly approach the guard, who is simply staring straight ahead. You wave your paws around his face. While waving your paws, you slightly touch his face, and his head falls back, revealing four clawlike gashes through his throat. The crimson scarf is colored that way due to his blood.\n\n"+
	"You look over to the bank's vault and find that the vault door is ajar.\n\n"+
	"\"It took you a while to get here.\" You hear a voice say from down the hall. In a moment, you feel a small pinch on your leg, and see a red plume of feathers sticking out of your jeans. You fall to the ground and see Rafaj, the convention coordinator come from around the corner.\n\n"+
	"Your vision gets hazy and darkness envelopes you.\n\n"+
	"C - Continue";

    readonly string VAULT_TEXT_3 = "You come to. The bright light from above an affront to your senses. You look around the vault. The entirety of the vault is an atrium, with a pedistal in the middle that holds the cauldron of honey. In front of the honey, you see Rafaj walking towards the honey, only something is off.\n\n"+
	"It is then that you notice Rafaj is wearing shoes and gloves that appear to be bear paws. His feet are creating impressions in the ground that remarkably resemble bear paws. He is scratching the ground in locations to create the illusion of struggle.\n\n"+
	"You get up and make your way to Rafaj.\n\n"+
	"C - Continue";

    readonly string WIN_TEXT = "You find a ship that is ready to sail. You load up the honey and untie the boat from the dock and push off. The sails billow in the wind, as you drive the boat through the bay. Looking back at the dock, you see guards and fishermen jumping up and down, shouting for you to come back. You continue out of the bay, with the wind in your fur, and a cauldron full of honey.\n\n"+
    "CONGRATULATIONS! YOU WON!!\n\n"+
    "PRESS ANY KEY TO RETURN TO THE MAIN MENU";

    readonly string EASY_ESCAPE_TEXT = "Rafaj is so distracted by trying to plant evidence of a bear attack, he does not hear you come up behind him. In one easy swipe of your paw; he flys to the other side of the vault, red blossoming across his back. He falls to the ground, motionless.\n\n"+
	"You head to the honey, and regard your prize. You know this is the infamous Black Orchid honey. It has a mystical property that transforms anyone who eats some of it into a goth.\n\n"+
	"You grab the handle of the cart the honey is on, and guide it to the opposite end of the vault, and swipe the key card you stole from the guard. The massive doors open revealing the convention has ended, and the park area is nearly deserted. You push the cart over to the docks and board a ship.\n\n"+
	"C - Continue";

    readonly string HARD_ESCAPE_TEXT = "You sneak up behind the man, but your shadow covers Rafaj, alerting him of your presence. He turns around as you swipe your paw. He is hit, and flies to the other end of the vault, but not before he is able to let out a loud yell.\n\n"+
	"You hear the sound of guards rushing up to the vault. Three guards show up with dart guns at the ready and shoot.\n\n"+
	"D - Dodge";

    readonly string HARD_ESCAPE_P = "You dodge all three darts with great agility.\n\n"+
	"\"QUICK! RELOAD BEFORE IT GETS AWAY!\" You hear one of them yell. You turn and grab the handle of the cart the honey is on and push it towards the big doors on the opposite end of the vault.\n\n"+
	"You hear three darts wizz by your head. You swipe the card into the card reader, and the door begins to open.\n\n"+
	"You then hear the guards catching up to you. You begin to squeeze the cart through the doors. You hear the smacks of the darts on the metal doors and you dart towards the ships.\n\n"+
	"C - Continue";

    readonly string HARD_ESCAPE_F = "You feel the three darts sink into your skin, and suddenly feel very dizzy. Your grip on the cart weekens as you try to run with it out of the vault. You stumble and fall forwards. The cart tips, and the cauldron of honey falls to the ground. The viscous yellow nectar spills onto the ground.\n\n"+
    "You hear the footfall of the guards as they approach you. \"We got it!\" the guards cry in happiness as your vision fades to black.\n\n"+
    "C - Continue";
    #endregion

    //These control the text that is to be displayed
    #region Event Booleans
    bool talked_to_Ozzy = false;
    bool lake_honey = false;
    bool lake_honey_passed = false;
    bool lake_got_honey = false;
    bool attack_matty_passed = false;
    bool disguise = false;
    bool town_center_explored = false;
    bool books_explored = false;
    bool t_cup_taken = false;
    bool hacked_security = false;
    bool keycard = false;
    bool forgot = false;
    bool vault_guard_pass = false;
    bool attack_rafaj = false;
    bool hard_escape_dodge = false;
    #endregion

    Player p;

    void Start(){
        content = transform.parent.GetComponentsInChildren<TextMeshProUGUI>();
        updated = false;

        scroll = GameObject.FindGameObjectWithTag("Scroll").GetComponent<Scrollbar>();

        p = GameManager.gm.bear;

        p.Lose += LoseGame;
    }

    enum States{
        start,
        start1,
        talk_Ozmodeus,
        follow,
        follow1,
        otherway,
        lake_honey_pass,
        lake_honey_honey,
        lake_honey_fail,
        otherway_h,
        otherway_c,
        attack_matty,
        help_matty,
        lilliput_gates,
        find_disguise,
        approach_guards,
        lilliput_town_center,
        lilliput_tents,
        lilliput_town_center_1,
        lilliput_town_center_2,
        lilliput_town_center_3,
        lilliput_town_center_4,
        books,
        books_e,
        books_e_t,
        books_talk,
        security_room,
        security_room_1,
        krack_inn,
        krack_inn_1,
        krack_inn_2,
        krack_inn_3,
        krack_inn_4,
        vault,
        vault_guard,
        vault_1,
        vault_2,
        vault_3,
        easy_escape,
        hard_escape,
        hard_escape_1,
        win,
        CONSTRUCTION,
        lose,
    }
    States state = States.start;

    bool inventory_visible = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            //"I" toggles the inventory screen
            inventory_visible = !inventory_visible;
            if(inventory_visible){
                InventoryManager.InventoryScreen.Activate();
                return;
            }
            else{
                InventoryManager.InventoryScreen.Deactivate();
            }
        }

        //Prevents progression if the inventory is open
        if(inventory_visible)
            return;

        switch(state){
            case States.lose:
                LOSE();
                break;
            case States.CONSTRUCTION:
                CONSTRUCT();
                break;
            case States.start:
                START();
                break;
            case States.start1:
                START1();
                break;
            case States.talk_Ozmodeus:
                TALK_OZMODEUS();
                break;
            case States.follow:
                FOLLOW();
                break;
            case States.lake_honey_pass:
                LAKE_HONEY_PASS();
                break;
            case States.lake_honey_honey:
                LAKE_HONEY_HONEY();
                break;
            case States.lake_honey_fail:
                LAKE_HONEY_FAIL();
                break;
            case States.otherway:
                OTHERWAY();
                break;
            case States.otherway_h:
                OTHERWAY_H();
                break;
            case States.otherway_c:
                OTHERWAY_C();
                break;
            case States.attack_matty:
                ATTACK_MATTY();
                break;
            case States.help_matty:
                HELP_MATTY();
                break;
            case States.lilliput_gates:
                LILLIPUT_GATES();
                break;
            case States.find_disguise:
                FIND_DISGUISE();
                break;
            case States.approach_guards:
                APPROACH_GUARDS();
                break;
            case States.lilliput_town_center:
                LILLIPUT_TOWN_CENTER();
                break;
            case States.lilliput_tents:
                LILLIPUT_TENTS();
                break;
            case States.lilliput_town_center_1:
                LILLIPUT_TOWN_CENTER_1();
                break;
            case States.lilliput_town_center_2:
                LILLIPUT_TOWN_CENTER_2();
                break;
            case States.lilliput_town_center_3:
                LILLIPUT_TOWN_CENTER_3();
                break;
            case States.lilliput_town_center_4:
                LILLIPUT_TOWN_CENTER_4();
                break;
            case States.books:
                BOOKS();
                break;
            case States.books_e:
                BOOKS_E();
                break;
            case States.books_e_t:
                BOOKS_E_T();
                break;
            case States.books_talk:
                BOOKS_TALK();
                break;
            case States.security_room:
                SECURITY_ROOM();
                break;
            case States.security_room_1:
                SECURITY_ROOM_1();
                break;
            case States.krack_inn:
                KRACK_INN();
                break;
            case States.krack_inn_1:
                KRACK_INN_1();
                break;
            case States.krack_inn_2:
                KRACK_INN_2();
                break;
            case States.krack_inn_3:
                KRACK_INN_3();
                break;
            case States.krack_inn_4:
                KRACK_INN_4();
                break;
            case States.vault:
                VAULT();
                break;
            case States.vault_guard:
                VAULT_GUARD();
                break;
            case States.vault_1:
                VAULT_1();
                break;
            case States.vault_2:
                VAULT_2();
                break;
            case States.vault_3:
                VAULT_3();
                break;
            case States.easy_escape:
                EASY_ESCAPE();
                break;
            case States.hard_escape:
                HARD_ESCAPE();
                break;
            case States.hard_escape_1:
                HARD_ESCAPE_1();
                break;
            case States.win:
                WIN();
                break;
        }
    }

#region State Functions
    void START(){
        if(!updated){
            UpdateText(LOCATION_1, START_TEXT);
        }

        if(Input.anyKey && !Input.GetMouseButton(0)){
            ChangeState(States.start1);
        }
    }

    void START1(){
        if(!updated){
            UpdateText(LOCATION_1, START1_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.T)){
            ChangeState(States.talk_Ozmodeus);
        }
        else if(Input.GetKeyDown(KeyCode.F)){
            ChangeState(States.follow);
        }
        else if(Input.GetKeyDown(KeyCode.O)){
            ChangeState(States.otherway);
        }
    }

    //Talk to the bear
    void TALK_OZMODEUS(){
        if(!updated){
            UpdateText(TALK_OZMODEUS_TEXT);
            talked_to_Ozzy = true;
        }

        if(Input.GetKeyDown(KeyCode.F)){
            ChangeState(States.follow);
        }
        else if(Input.GetKeyDown(KeyCode.O)){
            ChangeState(States.otherway);
        }
    }

    //Follow bear to Barefoot Lake
    void FOLLOW(){
        if(!updated){
            if(!lake_honey && !talked_to_Ozzy)
                UpdateText(LOCATION_2, FOLLOW_TEXT_HO);
            else if (lake_honey && !talked_to_Ozzy)
                UpdateText(LOCATION_2, FOLLOW_TEXT_O);
            else if(!lake_honey && talked_to_Ozzy)
                UpdateText(LOCATION_2, FOLLOW_TEXT_H);
            else
                UpdateText(LOCATION_2, FOLLOW_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.T)){
            ChangeState(States.talk_Ozmodeus);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            //If the player already passed the roll and left by accident or for some reason, then they do not need to reroll. Simialrly, if they failed, then they will have failed.
            if(lake_honey && !lake_honey_passed){
                ChangeState(States.lake_honey_fail);
                return;
            }
            else if(lake_honey_passed){
                ChangeState(States.lake_honey_pass);
                return;
            }
            
            int num;

            //Since the sun bear has the power to locate honey, they roll with advantage
            if(p.GetBear() == DETECT_HONEY){
                num = RollUI.ui.RollAdvantage();
            }
            else{
                num = RollUI.ui.RollSTD();
            }
            
            //If the number is <= the player's bear stat, then they pass the roll
            if(num <= p.bear){
                lake_honey_passed = true;
                ChangeState(States.lake_honey_pass);
            }
            else{
                lake_honey_passed = false;
                ChangeState(States.lake_honey_fail);
            }
        }
        else if(Input.GetKeyDown(KeyCode.H)){
            ChangeState(States.otherway);
        }
    }

    //Finds the Honey at bearfoot lake
    void LAKE_HONEY_PASS(){
        if(!updated){
            UpdateText(LOCATION_2, LAKE_HONEY_PASS_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            lake_honey = true;

            int num;

            //If player is either the climber or the theif, they will roll with advantage
            if(p.GetBear() == CLIMB || p.GetRoll() == THIEF){
                num = RollUI.ui.RollAdvantage();

                //If the player is both the climbing bear and the thief, the game should choose the best stat to compare the roll to. In other words, this player could roll for either bear or criminal
                if(p.GetBear() == CLIMB && p.GetRoll() == THIEF){
                    //Roll comparison
                    int comp;
                    //By choosing the biggest number, the player has a better chance of success
                    if(p.bear >= p.criminal)
                        comp = p.bear;
                    else
                        comp = p.criminal;

                    if(num <= comp){
                        //Got the honey
                        lake_got_honey = true;
                    }
                    else{
                        //Didn't get the honey
                    }
                }
                else if(p.GetBear() == CLIMB){
                    if(num <= p.bear){
                        //Got the honey
                        lake_got_honey = true;
                    }
                    else{
                        //Didnt get the honey
                    }
                }
                else if (p.GetRoll()==THIEF){
                    if(num <= p.criminal){
                        //Got the honey
                        lake_got_honey = true;
                    }
                    else{
                        //Didnt get the honey
                    }
                }
               ChangeState(States.lake_honey_honey);
               return; 
            }
            else 
                num = RollUI.ui.RollSTD();
            
            if(num<=p.bear){
                lake_got_honey = true;   
            }
            
            ChangeState(States.lake_honey_honey);
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            ChangeState(States.follow);
        }
    }

    void LAKE_HONEY_HONEY(){
        if(!updated){
            if(lake_got_honey){
                UpdateText(LOCATION_2, LAKE_HONEY_HONEY_P);
                GameManager.AddToInventory('H');
            }
            else{
                UpdateText(LOCATION_2, LAKE_HONEY_HONEY_F);
                GameManager.gm.bear.MoveToBear();
            }
            lake_honey = true;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            ChangeState(States.follow);
        }
    }

    void LAKE_HONEY_FAIL(){
        if(!updated){
            UpdateText(LOCATION_2, LAKE_HONEY_FAIL_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.R)){
            ChangeState(States.follow);
        }
    }

    void OTHERWAY(){
        if(!updated){
            UpdateText(LOCATION_3, OTHERWAY_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.H)){
            ChangeState(States.otherway_h);
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.otherway_c);
        }

    }

    void OTHERWAY_H(){
        if(!updated){
            UpdateText(LOCATION_3, OTHERWAY_H_TEXT);
            Dice d = new Dice();
            p.Hat = d.Hats();
            HELP_MATTY_TEXT ="You begin helping the old man by righting the cart. You then, in no time, pick up all of the hats that litter the ground and place them in the cart.\n\n"+
		"\"Thank you young whipper snapper!\" the old man says, with a slight whistle on his s's.\n\n"+
		"\"The name is Matty! I am taking hats down to the Honey Festival in Lilliput!. For helping me out, would you like a hat?\"\n\n"+
		"Matty does not wait for you to answer as he pushes a "+ p.Hat + " into your hands.\n\n"+
		"\"There you go! You look so distinguished! Everyone will think you're part of the main act at the park. Well, enjoy the festival!\" Matty says, as he continues towards the town with the his cart, singing to himself.\n\n"+
		"C - Continue";

            ATTACK_MATTY_P = "The man is crawling around on the ground with his hands outstretched, looking for his glasses. His distraction is your opprotunity. You slowly creep on him and with one swipe of your paw, the man falls still and the side of his wagon is painted red.\n\n"+
		"You pick up a"+ p.Hat +" and place it on your head.\n\n"+
		"YOU SUCCESSFULLY KILLED A HUMAN, YOU GAIN A POINT FOR CRIMINAL, AND A CRIMINAL FLASH BACK (PRESS \"I\" TO VIEW)\n\n"+
		"C - Continue";
        }

        if (Input.GetKeyDown(KeyCode.H)){
            ChangeState(States.help_matty);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            int num;

            if(p.GetBear() == CARNAGE || p.GetRoll() == MUSCLE){
                num = RollUI.ui.RollAdvantage();

                if(p.GetBear() == CARNAGE && p.GetRoll() == MUSCLE){
                    int comp;

                    if(p.bear >= p.criminal){
                        comp = p.bear;
                    }
                    else {
                        comp = p.criminal;
                    }

                    if(num <= comp){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                }
                else if(p.GetBear() == CARNAGE){
                    if(num <= p.bear){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                    
                }
                else if (p.GetRoll() == MUSCLE){
                    if(num <= p.criminal){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                }
                ChangeState(States.attack_matty);
                return;
            }
            else{
                num = RollUI.ui.RollSTD();
            }
            
            if(num <= p.bear){
                attack_matty_passed = true;
            }

            ChangeState(States.attack_matty);
        }
    }

    void OTHERWAY_C(){
        if(!updated){
            UpdateText(LOCATION_3, OTHERWAY_C_TEXT);
            Dice d = new Dice();
            p.Hat = d.Hats();
            HELP_MATTY_TEXT ="You begin helping the old man by righting the cart. You then, in no time, pick up all of the hats that litter the ground and place them in the cart.\n\n"+
		"\"Thank you young whipper snapper!\" the old man says, with a slight whistle on his s's.\n\n"+
		"\"The name is Matty! I am taking hats down to the Honey Festival in Lilliput!. For helping me out, would you like a hat?\"\n\n"+
		"Matty does not wait for you to answer as he pushes a "+ p.Hat+ " into your hands.\n\n"+
		"\"There you go! You look so distinguished! Everyone will think you're part of the main act at the park. Well, enjoy the festival!\" Matty says, as he continues towards the town with the his cart, singing to himself.\n\n"+
		"C - Continue";

        ATTACK_MATTY_P = "The man is crawling around on the ground with his hands outstretched, looking for his glasses. His distraction is your opprotunity. You slowly creep on him and with one swipe of your paw, the man falls still and the side of his wagon is painted red.\n\n"+
		"You pick up a "+ p.Hat +" and place it on your head.\n\n"+
		"YOU SUCCESSFULLY KILLED A HUMAN, YOU GAIN A POINT FOR CRIMINAL, AND A CRIMINAL FLASH BACK (PRESS \"I\" TO VIEW)\n\n"+
		"C - Continue";
        }

        if (Input.GetKeyDown(KeyCode.H)){
            ChangeState(States.help_matty);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            int num;

            if(p.GetBear() == CARNAGE || p.GetRoll() == MUSCLE){
                num = RollUI.ui.RollAdvantage();

                if(p.GetBear() == CARNAGE && p.GetRoll() == MUSCLE){
                    int comp;

                    if(p.bear >= p.criminal){
                        comp = p.bear;
                    }
                    else {
                        comp = p.criminal;
                    }

                    if(num <= comp){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                }
                else if(p.GetBear() == CARNAGE){
                    if(num <= p.bear){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                    
                }
                else if (p.GetRoll() == MUSCLE){
                    if(num <= p.criminal){
                        attack_matty_passed = true;
                    }
                    else{
                        attack_matty_passed = false;
                    }
                }
                ChangeState(States.attack_matty);
                return;
            }
            else{
                num = RollUI.ui.RollSTD();
            }
            
            if(num <= p.bear){
                attack_matty_passed = true;
            }

            ChangeState(States.attack_matty);
        }
    }

    void ATTACK_MATTY(){
        if(!updated){
            if(attack_matty_passed){
                UpdateText(ATTACK_MATTY_P);
                GameManager.AddToInventory('C');
                p.MoveToCriminal();
            }
            else
                UpdateText(ATTACK_MATTY_F);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.lilliput_gates);
        }
        if(Input.GetKeyDown(KeyCode.H)){
            ChangeState(States.help_matty);
        }
    }

    void HELP_MATTY(){
        if(!updated){
            UpdateText(HELP_MATTY_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.lilliput_gates);
        }
    }

    void LILLIPUT_GATES(){
        if(!updated){
            if(!disguise){
                UpdateText(LOCATION_4,LILLIPUT_GATES_TEXT);
            }
            else{
                UpdateText(LOCATION_4, LILLIPUT_GATES_D);
            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            if(!disguise){
                
                int num;

                if(p.GetRoll() == BRAINS){
                    num = RollUI.ui.RollAdvantage();
                }
                else{
                    num = RollUI.ui.RollSTD();
                }
                
                if(num<=p.criminal){
                    disguise = true;
                }

                ChangeState(States.find_disguise);
            }
            else
                return;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            ChangeState(States.approach_guards);
        }
    }

    void FIND_DISGUISE(){
        if(!updated){
            if(disguise){
                UpdateText(FIND_DISGUISE_TEXT_P);
            }
            else{
                UpdateText(FIND_DISGUISE_TEXT_F);
            }
        }

        if(Input.GetKeyDown(KeyCode.S)){
            if(!disguise){
                int num;

                if(p.GetRoll() == BRAINS){
                    num = RollUI.ui.RollAdvantage();
                }
                else{
                    num = RollUI.ui.RollSTD();
                }
                
                if(num<=p.criminal){
                    disguise = true;
                }

                ChangeState(States.find_disguise);
            }
            else{
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            ChangeState(States.approach_guards);
        }
    }

    void APPROACH_GUARDS()
    {
        if (!updated)
        {
            if(!disguise)
            {
                UpdateText(APPROACH_GUARDS_TEXT);
            }else
            {
                UpdateText(APPROACH_GUARDS_D);
                GameManager.AddToInventory('C');
                p.MoveToCriminal();
            }
        }

        if(!disguise)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeState(States.lilliput_gates);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeState(States.lilliput_town_center);
            }
        }
    }

    void LILLIPUT_TOWN_CENTER(){
        if(!updated){
            if(!town_center_explored)
                UpdateText(LOCATION_5, TOWN_CENTER_TEXT_E);
            else
                UpdateText(LOCATION_5, TOWN_CENTER_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            if(!town_center_explored){
                ChangeState(States.lilliput_tents);
            }
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.lilliput_town_center_1);
        }
    }

    void LILLIPUT_TENTS()
    {
        if(!updated)
        {
            UpdateText(LILLIPUT_TENTS_TEXT);
            town_center_explored = true;
            GameManager.AddToInventory('H');
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(States.lilliput_town_center);
        }
    }

    void LILLIPUT_TOWN_CENTER_1()
    {
        if(!updated)
        {
            UpdateText(TOWN_CENTER_TEXT_1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(States.lilliput_town_center_2);
        }
    }

    void LILLIPUT_TOWN_CENTER_2()
    {
        if (!updated)
        {
            UpdateText(TOWN_CENTER_TEXT_2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(States.lilliput_town_center_3);
        }
    }

    void LILLIPUT_TOWN_CENTER_3()
    {
        if (!updated)
        {
            UpdateText(TOWN_CENTER_TEXT_3);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(States.lilliput_town_center_4);
        }
    }

    void LILLIPUT_TOWN_CENTER_4()
    {
        if (!updated)
        {
            UpdateText(LOCATION_5, TOWN_CENTER_TEXT_4);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(States.books);
        }
    }

    void BOOKS()
    {
        if(!updated)
        {
            if(!books_explored)
            {
                UpdateText(LOCATION_6, BOOKS_TEXT);
            }
            else
            {
                UpdateText(LOCATION_6, BOOKS_TEXT_E);
            }
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            ChangeState(States.books_talk);
        }

        if(Input.GetKeyDown(KeyCode.E) && !books_explored)
        {
            ChangeState(States.books_e);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(States.lilliput_town_center_4);
        }
    }

    void BOOKS_TALK()
    {
        if(!updated)
        {
            UpdateText(BOOKS_TALK_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(States.books);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(States.security_room);
        }
    }

    void BOOKS_E(){
        if(!updated){
            UpdateText(BOOK_EXPLORE_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.T)){
            int num;
            bool b = p.GetBear() == DETECT_HONEY;
            bool c = p.GetRoll() == THIEF;

            if(b || c){
                num = RollUI.ui.RollAdvantage();

                if(b && c){
                    int comp;

                    if(p.bear >= p.criminal)
                        comp = p.bear;
                    else
                        comp = p.criminal;

                    if(num <= comp)
                        t_cup_taken = true;
                    else;
                }
                else if(b){
                    if(num <= p.bear){
                        t_cup_taken = true;
                    }
                    else;
                }
                else if(c){
                    if (num <= p.criminal){
                        t_cup_taken = true;
                    }
                    else;
                }
                ChangeState(States.books_e_t);
                return;
            }
            
            num = RollUI.ui.RollSTD();

            if(num <= p.criminal)
                t_cup_taken = true;
            ChangeState(States.books_e_t);
        }
    }

    void BOOKS_E_T()
    {
        if (!updated){
            if (!t_cup_taken){
                UpdateText(TAKE_T_CUP_F);
            }
            else{
                UpdateText(TAKE_T_CUP_P);
                GameManager.AddToInventory('H');
                GameManager.AddToInventory('C');
                p.MoveToCriminal();
            }
            books_explored = true;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(States.books);
        }
    }

    void SECURITY_ROOM(){
        if(!updated){
            UpdateText(SECURITY_ROOM_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.H)){
            int num;

            if(p.GetRoll() == HACKER){
                num = RollUI.ui.RollAdvantage();
            }
            else
                num = RollUI.ui.RollSTD();

            if(num <= p.criminal)
                hacked_security = true;

            
            ChangeState(States.security_room_1);
        }

        if(Input.GetKeyDown(KeyCode.F)){
            ChangeState(States.krack_inn);
        }
    }
    
    void SECURITY_ROOM_1(){
        if(!updated){
            if(hacked_security){
                UpdateText(HACK_P);
                p.MoveToCriminal();
                GameManager.AddToInventory('C');
            }
            else{
                UpdateText(HACK_F);
                p.MoveToBear();
            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            ChangeState(States.krack_inn);
        }
    }

    void KRACK_INN(){
        if(!updated){
            UpdateText(LOCATION_7, KRACK_INN_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.A)){
            ChangeState(States.krack_inn_1);
        }
    }

    void KRACK_INN_1(){
        if(!updated){
            UpdateText(KRACK_INN_TEXT_1);
        }

        if(Input.GetKeyDown(KeyCode.T)){
            ChangeState(States.krack_inn_2);
        }
    }

    void KRACK_INN_2(){
        if(!updated){
            UpdateText(KRACK_INN_TEXT_2);
            if(forgot)
                content[DESCRIPTION_INDEX].text += "\n\n You seem to have forgotten the string.";
        }

        if(Input.GetKeyDown(KeyCode.A)){
            ChangeState(States.krack_inn_3);
        }
    }

    void KRACK_INN_3(){
        if(!updated){
            UpdateText(KRACK_INN_TEXT_3);
        }

        if(Input.GetKeyDown(KeyCode.T)){
            int num;
            if(p.GetRoll() == THIEF){
                num = RollUI.ui.RollAdvantage();
            }
            else
                num = RollUI.ui.RollSTD();
            
            if(num <= p.criminal){
                ChangeState(States.krack_inn_4);
                forgot = false;
            }
            else{
                ChangeState(States.krack_inn_2);
                forgot = true;
            }
        }
    }

    void KRACK_INN_4(){
        if(!updated){
            UpdateText(KRACK_INN_TEXT_4);
        }

        if(Input.GetKeyDown(KeyCode.V)){
            ChangeState(States.vault);
        }
    }

    void VAULT(){
        if(!updated){
            if(hacked_security){
                UpdateText(LOCATION_8, VAULT_TEXT);
            }
            else{
                UpdateText(LOCATION_8, VAULT_TEXT_T);
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            if(hacked_security)
                ChangeState(States.vault_1);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            if(!hacked_security){
                ChangeState(States.vault_guard);
                int num = RollUI.ui.RollSTD();

                if(num <= p.bear){
                    vault_guard_pass = true;
                }
                ChangeState(States.vault_guard);
            }
        }
    }

    void VAULT_GUARD(){
        if(!updated){
            if(vault_guard_pass)
                UpdateText(VAULT_GUARD_P);
            else
                UpdateText(VAULT_GUARD_F);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            if(vault_guard_pass)
                ChangeState(States.vault_1);
            else
                ChangeState(States.vault_3);
        }
    }

    void VAULT_1(){
        if(!updated){
            UpdateText(VAULT_TEXT_1);
        }

        if(Input.GetKeyDown(KeyCode.A)){
            ChangeState(States.vault_2);
        }
    }

    void VAULT_2(){
        if(!updated){
            UpdateText(VAULT_TEXT_2);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.vault_3);
        }
    }

    void VAULT_3(){
        if(!updated){
            UpdateText(VAULT_TEXT_3);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            int num;
            bool b = p.GetBear() == CARNAGE;
            bool c = p.GetRoll() == MUSCLE;
            if(b || c){
                num = RollUI.ui.RollAdvantage();

                if(b && c){
                    int comp = p.bear >= p.criminal ? p.bear : p.criminal;
                    if(num <= comp){
                        attack_rafaj = true;
                    }
                    else;
                }
                else if (b){
                    if (num <= p.bear){
                        attack_rafaj = true;
                    }
                    else;
                }
                else if(c){
                    if(num <= p.criminal){
                        attack_rafaj = true;
                    }
                    else;
                }
                
                if(attack_rafaj){
                    ChangeState(States.easy_escape);
                    return;
                }
                else{
                    ChangeState(States.hard_escape);
                    return;
                }
            }
            else{
                num = RollUI.ui.RollSTD();

                if(num <= p.bear){
                    ChangeState(States.easy_escape);
                }
                else{
                    ChangeState(States.hard_escape);
                }
            }
        }
    }

    void EASY_ESCAPE(){
        if(!updated){
            UpdateText(EASY_ESCAPE_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            ChangeState(States.win);
        }
    }

    void HARD_ESCAPE(){
        if(!updated){
            UpdateText(HARD_ESCAPE_TEXT);
        }

        if(Input.GetKeyDown(KeyCode.D)){
            int num = RollUI.ui.RollSTD();

            if(num <= p.bear){
                hard_escape_dodge = true;
            }
            
            ChangeState(States.hard_escape_1);
        }
    }

    void HARD_ESCAPE_1(){
        if(!updated){
            if(hard_escape_dodge)
                UpdateText(HARD_ESCAPE_P);
            else
                UpdateText(HARD_ESCAPE_F);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            if(hard_escape_dodge)
                ChangeState(States.win);
            else    
                ChangeState(States.lose);
        }
    }

    void WIN(){
        if(!updated){
            UpdateText(LOCATION_9, WIN_TEXT);
        }

        if(Input.anyKeyDown && !Input.GetMouseButton(0)){
            GameManager.gm.LoadLevel("0_Main Menu");
        }
    }

    void CONSTRUCT(){
        if(!updated){
            UpdateText(TRAP, CONSTRUCT_TEXT);
        }

        if(Input.anyKeyDown && !Input.GetMouseButton(0)){
            GameManager.gm.LoadLevel("0_Main Menu");
        }
        
    }

    void LOSE(){
        if(!updated){
            
            content[LOCATION_INDEX].text = LOSE_TEXT;
            if(p.bear == 6){
                content[DESCRIPTION_INDEX].text = "You stop everything you are doing, and roar extremely loudly. You get on all four paws, and run off. The guards in town try to capture you; however, you are too wild to be contained. You flee Lilliput, with the clothes on your back, and no honey.\n\n"+
                "YOU HAVE TURNED INTO A FULL FLEDGED BEAR.\n\n"+
                "PRESS ANY BUTTON TO RETURN TO THE MAIN MENU.";
            }
            else if(p.criminal == 6){
                content[DESCRIPTION_INDEX].text = "You stop everything you are doing, and think. You evaluate your surroundings and see the bigger picture. You don't need the honey here. You can do so much better. You decide to return to the woods and work on your master heist. The one that will begin your plan to dominate the world.\n\n"+
                "YOU HAVE TURNED INTO A FULL FLEDGED CRIMINAL.\n\n"+
                "PRESS ANY BUTTON TO RETURN TO THE MAIN MENU.";
            }
            else{
                content[DESCRIPTION_INDEX].text = "Unfortunately you lost.\n\n Thank you for playing.\n\nPRESS ANY BUTTON TO RETURN TO THE MAIN MENU.";
            }
        }

        if(Input.anyKeyDown && !Input.GetMouseButton(0))
            GameManager.gm.LoadLevel("0_Main Menu");
    }


#endregion

    void LoseGame(){
        ChangeState(States.lose);
    }

    void ChangeState(States s){
        state = s;
        updated = false;
        scroll.value = 1f;
    }

    void UpdateText(string location, string descript){
        content[LOCATION_INDEX].text = location;
        content[DESCRIPTION_INDEX].text = descript;

        updated = true;
    }

    void UpdateText(string descript){
        content[DESCRIPTION_INDEX].text = descript;

        updated = true;
    }

    void OnDisable(){
        p.Lose -= LoseGame;
    }
}
