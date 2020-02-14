using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeButton : ButtonBehavior
{
    bool over;
    bool display;
    float Timer;

    public GameObject hintText;

    void Start(){
        over = false;
        display = false;
        Timer = 0f;
        ExitDisplay();
    }

    void Update(){
        if(over){
            Timer += Time.deltaTime;
            if(Timer >= 2f && !display){
                Display();
            }
        }
        else if(!over && Timer > 0f){
            ExitDisplay();
        }
    }


    public override void MoveStat(){
        GameManager.gm.bear.MoveToCriminal();
        Destroy(gameObject);
    }

    public void MouseOver(){
        over = true;
    }

    public void MouseExit(){
        over = false;
    }

    public void Display(){
        display = true;
        hintText.SetActive(true);
    }

    public void ExitDisplay(){
        display = false;
        hintText.SetActive(false);
        Timer = 0f;
    }
}
