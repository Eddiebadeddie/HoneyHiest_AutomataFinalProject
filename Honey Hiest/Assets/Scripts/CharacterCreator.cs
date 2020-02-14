using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    List<string> Descriptors = new List<string>();
    List<string> Bears = new List<string>();
    List<string> Rolls = new List<string>();
    Dictionary<string, string> Skills= new Dictionary<string, string>();

    Dice DescriptorDice = new Dice();
    Dice BearsDice = new Dice();
    Dice RollsDice = new Dice();

    string description;
    string bear;
    string roll;

    TextMeshProUGUI descriptorText;
    TextMeshProUGUI bearText;
    TextMeshProUGUI rollText;
    TextMeshProUGUI skillText;

    // Start is called before the first frame update
    void Start()
    {
        Descriptors.Add("Rookie");
        Descriptors.Add("Washed Up");
        Descriptors.Add("Retired");
        Descriptors.Add("Unhinged");
        Descriptors.Add("Slick");
        Descriptors.Add("Incompetent");

        Bears.Add("Grizzly Bear");
        Bears.Add("Polar Bear");
        Bears.Add("Panda Bear");
        Bears.Add("Black Bear");
        Bears.Add("Sun Bear");
        Bears.Add("Honey Badger");

        Skills.Add(Bears[0], "Terrify");
        Skills.Add(Bears[1], "Swim");
        Skills.Add(Bears[2], "Eat Bamboo");
        Skills.Add(Bears[3], "Climb");
        Skills.Add(Bears[4], "Sense Honey");
        Skills.Add(Bears[5], "Carnage");

        GameManager.gm.SetSkills(Skills);

        Rolls.Add("Muscle");
        Rolls.Add("Brains");
        Rolls.Add("Driver");
        Rolls.Add("Hacker");
        Rolls.Add("Thief");
        Rolls.Add("Face");

        descriptorText = GameObject.FindWithTag("DescriptionText").GetComponent<TextMeshProUGUI>();
        bearText = GameObject.FindWithTag("BearText").GetComponent<TextMeshProUGUI>();
        rollText = GameObject.FindWithTag("RollText").GetComponent<TextMeshProUGUI>();
        skillText = GameObject.FindWithTag("SkillText").GetComponent<TextMeshProUGUI>();

        RollCharacter();
    }

    public void RollCharacter(){
        description = Descriptors[DescriptorDice.RollSTD() - 1];
        bear = Bears[BearsDice.RollSTD() - 1];
        roll = Rolls[RollsDice.RollSTD() - 1];

        Debug.Log(description + " " + bear + " as the " + roll);

        descriptorText.text = description;
        bearText.text = bear;
        rollText.text = roll;
        skillText.text = Skills[bear];
    }

    public void LoadStart(){
        Player p = new Player(description, bear, Skills[bear], roll);
        GameManager.gm.SetPlayerData(p);
        GameManager.gm.LoadLevel("2_DFA");
    }
}