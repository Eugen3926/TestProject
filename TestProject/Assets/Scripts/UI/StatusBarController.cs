using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] private TextMeshPro statusBoard;
    [SerializeField] private Transform wh;

    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
        Resource.onChangedState += UpdateInfo;
    }

    private void UpdateInfo()
    {
        int capacity = wh.GetComponent<Warehouse>().capacity;
        int occupied = wh.childCount;
        statusBoard.text = occupied + "/" + capacity;
    }    
}
