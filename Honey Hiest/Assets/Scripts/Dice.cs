using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    int rolled_num;
    int STD = 6;
    int HATS = 8;

    public Dice(){}
    
    public int RollSTD(){
        rolled_num = Random.Range(1, STD + 1);
        return rolled_num;
    }

    public int RollHATS(){
        rolled_num = Random.Range(1, HATS + 1);
        return rolled_num;
    }

    public int GetRolled(){
        return rolled_num;
    }

    public int RollAdvantage(){
        int num1 = RollSTD();
        int num2 = RollSTD();

        if (num1 <= num2)
            return num1;
        else
            return num2;
    }

    public int RollDisadvantage(){
        int num1 = RollSTD();
        int num2 = RollSTD();

        if (num1 < num2)
            return num2;
        else
            return num1;
    }

    public string Hats(){
        int num = RollHATS();

        switch(num){
            case 1:
                return "Trilby";
            case 2:
                return "Top";
            case 3:
                return "Bowler";
            case 4:
                return "Flat-Cap";
            case 5:
                return "Cowboy";
            case 6:
                return "Fez";
            case 7:
                return "Crown";
            case 8:
                break;
        }

        return Hats();
    }
}
