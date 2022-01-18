using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private Vector3 endPoint;
    private float elapsedTime;
    private float desiredDuration = 10f;    

    ResourceController rc;
    private Transform inventary;
    private int childCount;
    private Transform pool;

    public static event onResourceEvent onChangedState; // Event for Resource creation     
    public delegate void onResourceEvent();

    private void Start()
    {
        pool = GameObject.Find("ResourcesPool").transform;
        rc = new ResourceController();
    }

    private void OnEnable()
    {
        inventary = this.transform.parent;
        childCount = inventary.childCount;
        setSpecifications(inventary, childCount);
        
    }    

    private void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        transform.localPosition = Vector3.Lerp(transform.localPosition, endPoint, Mathf.SmoothStep(0, 1, percentageComplete));

        if (transform.localPosition == endPoint) {
            if (this.transform.parent.name == "Kill")
            {
                this.gameObject.transform.SetParent(pool);
                this.gameObject.SetActive(false);
            }

            if (this.transform.parent.tag == "Warehouse" || this.transform.parent.name == "Inventory") {
                onChangedState?.Invoke();
            }

            transform.GetComponent<Resource>().enabled = false;
        }
    }

    private void setSpecifications(Transform inventary, int childCount)
    {
        switch (inventary.name)
        {
            case "ResourcesWarehouse":
                moveConfig(rc.ResourcePositionWH(inventary, this.transform, childCount), 10f);
                break;
            case "Resources1Warehouse":
                moveConfig(rc.ResourcePositionWH(inventary, this.transform, childCount), 10f);
                break;
            case "Resources2Warehouse":
                moveConfig(rc.ResourcePositionWH(inventary, this.transform, childCount), 10f);
                break;
            case "Kill":
                moveConfig(new Vector3(0f, 0f, 0f), 15f);
                break;
            case "Spawn":
                moveConfig(new Vector3(0f, 0f, -16f), 20f);
                break;
            case "FinishedGoodsWarehouse":
                moveConfig(new Vector3(-0.1f, 3.72f, 0f), 10f);
                break;
            case "Inventory":
                moveConfig(rc.ResourcePosition(inventary, this.transform), 10f);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inventary.name != "Inventory") {
            this.transform.SetParent(transform.parent.transform.parent.GetComponent<Factory>().finishedGoodsWarehouse.transform);
            inventary = this.transform.parent;
            moveConfig(rc.ResourcePositionWH(inventary, this.transform, inventary.childCount), 15f);
        }
                
    }

    private void moveConfig(Vector3 end, float desired){
        endPoint = end;
        desiredDuration = desired;
    }
    

}
