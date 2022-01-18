using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab1 { get; }
    public T prefab2 { get; }
    public T prefab3 { get; }

    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> pool;

    public PoolMono(T prefab1, T prefab2, T prefab3, int count, Transform container) {
        this.prefab1 = prefab1;
        this.prefab2 = prefab2;
        this.prefab3 = prefab3;
        this.container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i<count; i++) {
            this.CreateObject(this.prefab1);
            this.CreateObject(this.prefab2);
            this.CreateObject(this.prefab3);
        }

    }

    private T CreateObject(T resPrefab, bool isActiveByDefault = false) {
        var createdObject = UnityEngine.Object.Instantiate(resPrefab, this.container);
        createdObject.name = resPrefab.name;
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element, T resPrefab) {
        
        foreach (var mono in pool) {            
            if (!mono.gameObject.activeInHierarchy && mono.gameObject.name == resPrefab.name) {                
                element = mono;
                element.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement(T resPrefab) {
        if (this.HasFreeElement(out var element, resPrefab)){
            return element;
        }

        if (this.autoExpand) {
            return this.CreateObject(resPrefab, true);
        }

        throw new Exception($"There is no free elements in pool of type {typeof(T)}" );
    }
}
