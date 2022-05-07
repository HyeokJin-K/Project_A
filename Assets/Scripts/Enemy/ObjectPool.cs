using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> objectPool;

    [SerializeField]
    protected int initPoolAmount = 20;

    protected void AddObjectPool(GameObject prefab)
    {        
        for(int i = 0; i < initPoolAmount; i++)
        {
            objectPool.Add(Instantiate(prefab, this.transform));
            objectPool[i].SetActive(false);
        }
    }

}
