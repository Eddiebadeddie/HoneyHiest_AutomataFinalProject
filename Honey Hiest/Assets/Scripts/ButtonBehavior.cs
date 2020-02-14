using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public void Over(){
        SFX_Audio.sfx.PlayOver();
    }

    public void Click(){
        SFX_Audio.sfx.PlayClick();
    }

    public virtual void MoveStat(){}

    public void LoadLevel(string name){
        GameManager.gm.LoadLevel(name);
    }

    public void LoadLevel(int num){
        GameManager.gm.LoadLevel(num);
    }

    public void Quit(){
        GameManager.gm.QuitGame();
    }
}
