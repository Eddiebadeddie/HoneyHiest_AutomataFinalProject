using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    readonly string CLASS = "<b><color=blue>STAT DISPLAY::</color></b> ";
    public static StatDisplay stats = null;
    List<GameObject> stats_icons = new List<GameObject>();

    int BEAR_START = 1;
    int CRIMINAL_START = 7;

    void Start(){
        if(stats == null)
            stats = this;
        else
        {
            Destroy(gameObject);
        }

        Transform[] temp = GetComponentsInChildren<Transform>();
        
        for(int i = 0; i < temp.Length; ++i){
            stats_icons.Add(temp[i].gameObject);
        }
        
        UpdateUI();
    }

    void OnEnable(){
        GameManager.gm.bear.UI += UpdateUI;
    }

    void OnDisable(){
         GameManager.gm.bear.UI -= UpdateUI;
    }

    void UpdateUI(){
        //Updates Bear Icon display
        for(int b = BEAR_START; b < BEAR_START + 6; ++b){

            if(b < BEAR_START + GameManager.gm.bear.bear){
                stats_icons[b].SetActive(true);
            }
            else{
                stats_icons[b].SetActive(false);
            }
        }

        //Updates Criminal Icon display
        for(int b = CRIMINAL_START; b < CRIMINAL_START + 6; ++b){
            if(b < CRIMINAL_START + GameManager.gm.bear.criminal){
                stats_icons[b].SetActive(true);
            }
            else{
                stats_icons[b].SetActive(false);
            }
        }
    }

    void Out(string message){
        print(CLASS + message);
    }
}
