using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager InventoryScreen = null;
    public GameObject InventoryUI;
    public GameObject UIParent;
    public GameObject HoneyButton;
    public GameObject CrimeButton;

    void Awake(){
        if(InventoryScreen == null){
            InventoryScreen = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InventoryUI.SetActive(false);
    }

    public void Activate(){
        InventoryUI.SetActive(true);
        FillInventory();
    }

    public void Deactivate(){
        foreach(Transform t in UIParent.transform){
            Destroy(t.gameObject);
        }
        InventoryUI.SetActive(false);
    }

    void FillInventory(){
        foreach(char c in GameManager.inventory){
            if(c=='H'){
                Instantiate(HoneyButton, UIParent.transform);
            }
            else if(c == 'C'){
                Instantiate(CrimeButton, UIParent.transform);
            }
        }
    }
}
