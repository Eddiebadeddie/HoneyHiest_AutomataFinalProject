using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollUI : MonoBehaviour
{
    public static RollUI ui = null;

    TextMeshProUGUI text;

    Dice d = new Dice();

    void Awake(){
        if(ui == null){
            ui = this;
        }
        else
            Destroy(gameObject);

        text = GetComponent<TextMeshProUGUI>();
        text.text= "Roll";
    }

    public int RollSTD(){
        int num = d.RollSTD();
        text.text = "Roll: " + num;
        return num;
    }

    public int RollAdvantage(){
        int num = d.RollAdvantage();
        text.text = "Roll: " + num;
        return num;
    }

    public int RollDisadvantage(){
        int num = d.RollDisadvantage();
        text.text = "Roll: " + num;
        return num;
    }
}
