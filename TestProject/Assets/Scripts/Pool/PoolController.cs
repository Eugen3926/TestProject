using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private int poolCount = 3;
    [SerializeField] private bool autoExpand = false;

    [SerializeField] private Resource resourcePrefub1;
    [SerializeField] private Resource resourcePrefub2;
    [SerializeField] private Resource resourcePrefub3;

    private PoolMono<Resource> pool;
    

    // Start is called before the first frame update
    private void Start()
    {
        this.pool = new PoolMono<Resource>(this.resourcePrefub1, this.resourcePrefub2, this.resourcePrefub3, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
        Factory.onResCreated += CreateResource;
    }

    private void CreateResource(Resource res, Transform warehouse)
    {
        var cube = this.pool.GetFreeElement(res);        
        cube.transform.SetParent(warehouse);
        cube.transform.localPosition = new Vector3(0f, 0f, 0f);
        cube.transform.GetComponent<Resource>().enabled = true;        
    }    
}
