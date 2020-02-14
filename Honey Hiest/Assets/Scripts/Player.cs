using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string Descriptor;
    string Bear;
    string Skill;
    string Roll;
    public string Hat;

    int Bear_stat;
    int Criminal_stat;


    public delegate void UpdateStats();
    public event UpdateStats UI;
    public event UpdateStats Lose;

    public Player(string d, string b, string s, string r){
        Descriptor = d;
        Bear = b;
        Skill = s;
        Roll = r;

        Bear_stat = 3;
        Criminal_stat = 3;
    }

    #region Setters and Getters
    public int bear{
        get{return Bear_stat;}
    }

    public int criminal{
        get{return Criminal_stat;}
    }


    public void SetPlayer(Player p){
        Descriptor = p.GetDescription();
        Bear = p.GetBear();
        Skill = p.GetSkill();
        Roll = p.GetRoll();
    }

    public string GetDescription(){
        return Descriptor;
    }

    public string GetBear(){
        return Bear;
    }

    public string GetSkill(){
        return Skill;
    }

    public string GetRoll(){
        return Roll;
    }
    #endregion

    public void MoveToBear(){
        ++Bear_stat;
        --Criminal_stat;

        UI();
        if(Bear_stat == 6){
            Lose();
        }
    }

    public void MoveToCriminal(){
        ++Criminal_stat;
        --Bear_stat;

        UI();
        if(Criminal_stat == 6){
            Lose();
        }
    }
}