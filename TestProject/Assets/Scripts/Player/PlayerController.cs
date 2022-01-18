using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform inventory;


    [SerializeField] private float _moveSpeed;
    [SerializeField] private int inventoryCapacity;

    
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical !=0) {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.Play("Run");
        } else _animator.Play("Idle");
    }    

    private void OnCollisionStay(Collision collision)
    {
        ResExchenged(collision);        
    }

    private void ResExchenged(Collision collision) {
        switch (collision.transform.name) {
            case "FinishedGoodsWarehouse":
                PickUpResources(collision.transform, inventoryCapacity);                
                break;
            case "ResourcesWarehouse":
                PutResources(collision.transform, inventoryCapacity);                
                break;
        }
    }

    private void PutResources(Transform wh, int capacity)
    {
        var factoryType = wh.parent.GetComponent<Factory>().nameFactory;

        if (inventory.childCount > 0 && wh.childCount < wh.GetComponent<Warehouse>().capacity)
        {            
            switch (factoryType)
            {
                case Factory.factoryType.Factory2:
                    GiveAwayResources(inventory, wh, "Resource 1");
                    break;
                case Factory.factoryType.Factory3:
                    GiveAwayResources(inventory, wh.parent.GetChild(9).transform, "Resource 1");
                    GiveAwayResources(inventory, wh.parent.GetChild(10).transform, "Resource 2");
                    break;
            }
        }        
    }

    private void PickUpResources(Transform wh, int capacity)
    {        
        if (inventory.childCount < capacity) {            
            GetResources(wh, inventory);
        }
    }

    private void GetResources(Transform wh, Transform inv)
    {
        if (wh.childCount>0) {
            int lastObj = wh.childCount - 1;
            for (int i= lastObj; i > -1; i--) {
                Transform res = wh.GetChild(i).transform;
                if (res.localPosition.z < 0.62f)
                {
                    res.gameObject.SetActive(false);
                    res.SetParent(inv);                                        
                    res.localScale = new Vector3(1f, 1f, 1f);                    
                    res.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    res.gameObject.SetActive(true);
                    res.GetComponent<Resource>().enabled = true;
                    i = 0;
                }
            }                      
        }
    }

    private void GiveAwayResources(Transform inv, Transform wh, string resType)
    {
        if (inv.childCount > 0)
        {
            int lastObj = inv.childCount - 1;
            for (int i = lastObj; i > -1; i--) {
                Transform res = inv.GetChild(i).transform;
                if (res.name == resType)
                {
                    inv.GetChild(lastObj).transform.localPosition = res.localPosition;
                    res.SetParent(wh);
                    res.gameObject.SetActive(false);
                    res.localScale = new Vector3(0.2f, 0.44f, 0.071f);
                    res.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    res.gameObject.SetActive(true);
                    res.GetComponent<Resource>().enabled = true;
                    i = 0;
                }
            } 
        }
    }
}
