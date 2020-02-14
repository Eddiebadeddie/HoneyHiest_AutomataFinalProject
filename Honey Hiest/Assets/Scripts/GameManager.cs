using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    //TODO: Keep track of this, I don't think its really needed
    Dictionary<string, string> Skills;

    Player player = null;

    public static List<char> inventory;

    public bool debug;

    #region Game Manager Utitlity
    void Awake(){
        if(gm == null){
            gm = this;
        }
        else{
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if(debug)
            player = new Player("Unhinged", "Honey Badger", "Carnage", "Hacker");

        inventory = new List<char>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            QuitGame();
        }
    }

    public void LoadLevel(int num){
        SceneManager.LoadScene(num);
    }

    public void LoadLevel(string name){
        SceneManager.LoadScene(name);
    }

    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }
    #endregion
    
    #region Player Management
    public void SetSkills(Dictionary<string, string> skills){
        Skills = skills;
    }

    public void SetPlayerData(Player p){
        player = p;
    }

    public Player bear{
        get{return player;}
    }
    #endregion

    public static void AddToInventory(char s){
        if(s == 'H' || s == 'C')
            inventory.Add(s);
        else
            return;
    }

    public static void RemoveFromInventory(char s){
        if(inventory.Contains(s)){
            inventory.Remove(s);
        }
        else
            return;
    }
}