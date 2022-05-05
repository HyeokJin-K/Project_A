using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterObjectPool : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> monsterObjectPool;

    [SerializeField]
    protected int initMonsterPoolAmount = 20;

    protected void AddMonsterObjectPool(GameObject monsterPrefab)
    {        
        for(int i = 0; i < initMonsterPoolAmount; i++)
        {
            monsterObjectPool.Add(Instantiate(monsterPrefab, this.transform));
            monsterObjectPool[i].SetActive(false);
        }
    }

}
