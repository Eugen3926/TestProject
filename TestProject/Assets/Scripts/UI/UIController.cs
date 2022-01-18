using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusBoard;
    string txt = "";
    List<string> str; 
    // Start is called before the first frame update
    void Start()
    {
        str = new List<string>();
        Factory.onChangedStatus += UINotification;
    }

    private void UINotification(Factory.factoryType name, int notifType)
    {

        switch (notifType) {
            case 1:
                str.Add(name + " was stopped. Not enough resources");                
                break;
            case 2:
                str.Add(name + " was stopped. The warehouse is full");                
                break;

        }        
        int lenght = str.Count;
        statusBoard.text = "";
        for (int i= (lenght-1); i > lenght-4; i--) {
            if (i >= 0) {
                statusBoard.text += str[i] + "\n";
            }            
        }      
        
    }    
}
