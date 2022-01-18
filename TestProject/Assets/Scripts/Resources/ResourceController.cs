using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController
{
    private int line = 4; 
    private int column = 9;
    
    private Vector3 firstPoint;

    public Vector3 ResourcePosition(Transform inventory, Transform res) {
        int stackHeight = 15; // Inventory stack height
        int colNum = (inventory.childCount - 1) / stackHeight;
        float xPos = 0f;
        float yPos = (inventory.childCount - 1) * res.localScale.y - res.localScale.y * (stackHeight) * colNum;
        float zPos = colNum * (-res.localScale.z) * 1f;

        return new Vector3(xPos, yPos, zPos);
    }

    public Vector3 ResourcePositionWH(Transform inventory, Transform res, int childCount)
    {
        float distance = 1.2f; // Distance between objects
        firstPoint = new Vector3(-0.367f, 0.64f, 0.45f);
        int colNum = (childCount - 1) / line;
        int l = line;
        if (inventory.parent.GetComponent<Factory>() != null && inventory.parent.GetComponent<Factory>().nameFactory == Factory.factoryType.Factory3)
        {
            colNum = (childCount - 1) / (line / 2);
            l = line /2 ;            
        }
        int lineNum = colNum / column;        
        
        float xPos = firstPoint.x + res.localScale.x * distance * (childCount - 1) - res.localScale.x * distance * l * colNum;
        float yPos = firstPoint.y + res.localScale.y * distance * 1.2f * ((childCount - 1) / (l * column));
        float zPos = firstPoint.z - res.localScale.z * distance * 1.2f * colNum + res.localScale.z * distance * 1.2f * column * lineNum;      

        return new Vector3(xPos, yPos, zPos);       
    }    
}
