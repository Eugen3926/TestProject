using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public enum factoryType
    {
        Factory1,
        Factory2,
        Factory3
    };

    [SerializeField] public factoryType nameFactory;    
    [SerializeField] private float timeToCreate = 2f;
    [SerializeField] private Transform resourcesWarehouse;
    [SerializeField] public Transform finishedGoodsWarehouse;

    [SerializeField] private Transform spawn;
    [SerializeField] private Resource resourcePrefub1;
    [SerializeField] private Resource resourcePrefub2;
    [SerializeField] private Resource resourcePrefub3;

    public static event onChangedEvent onChangedStatus;
    public static event onFactoryEvent onResCreated; // Event for Resource creation     
    public delegate void onFactoryEvent(Resource resource, Transform Warehouse);
    public delegate void onChangedEvent(factoryType name, int notifType);

    // Start is called before the first frame update
    void Start()
    {   
        this.StartCoroutine("ResourceCreation");
        
    }

    private IEnumerator ResourceCreation()
    {       
        while (true)
        {
            yield return new WaitForSecondsRealtime(this.timeToCreate);
            if ((finishedGoodsWarehouse.childCount + spawn.childCount) < finishedGoodsWarehouse.transform.GetComponent<Warehouse>().capacity)
            {
                resType(nameFactory);
            }
            else { onChangedStatus?.Invoke(nameFactory, 2); }                                 
        }
    }

    private void resType(factoryType name) {
        switch (name)
        {            
            case factoryType.Factory1:
                onResCreated?.Invoke(resourcePrefub1, finishedGoodsWarehouse.parent.transform.GetChild(10).transform);                 
                break;
            case factoryType.Factory2:
                int needRes = 1; // Number of Resources from first factory which need to create new good

                if (resourcesWarehouse.childCount / needRes >= 1)
                {
                    for (int i = 0; i < needRes; i++) {
                        GameObject obj = resourcesWarehouse.GetChild(resourcesWarehouse.childCount-1).gameObject;
                        obj.transform.SetParent(resourcesWarehouse.parent.transform.GetChild(9).transform);
                        obj.transform.GetComponent<Resource>().enabled = true;                        
                    }
                        onResCreated?.Invoke(resourcePrefub2, finishedGoodsWarehouse.parent.transform.GetChild(10).transform);
                }
                else {
                    onChangedStatus?.Invoke(factoryType.Factory2, 1);
                }

                break;
            case factoryType.Factory3:
                int needRes1 = 1; // Number of Resources from first factory which need to create new good
                int needRes2 = 1; // Number of Resources from second factory which need to create new good

                int currentRes1 = 0;
                int currentRes2 = 0;

                Transform wh1 = resourcesWarehouse.parent.GetChild(9).transform;
                Transform wh2 = resourcesWarehouse.parent.GetChild(10).transform;

                if (wh1.childCount >= needRes1 && wh2.childCount >= needRes2) {
                    for (int i = 0; i < needRes1; i++)
                    {
                        GameObject obj = wh1.GetChild(wh1.childCount - 1).gameObject;
                        obj.transform.SetParent(resourcesWarehouse.parent.transform.GetChild(11).transform);
                        obj.transform.GetComponent<Resource>().enabled = true;                        
                    }
                    for (int i = 0; i < needRes2; i++)
                    {
                        GameObject obj = wh2.GetChild(wh2.childCount - 1).gameObject;
                        obj.transform.SetParent(resourcesWarehouse.parent.transform.GetChild(11).transform);
                        obj.transform.GetComponent<Resource>().enabled = true;                        
                    }
                    onResCreated?.Invoke(resourcePrefub3, finishedGoodsWarehouse.parent.transform.GetChild(12).transform);
                }                

                if (currentRes1 / needRes1 < 1 || currentRes2 / needRes2 < 1) onChangedStatus?.Invoke(factoryType.Factory3, 1); 


                break;

        }
    }
}
