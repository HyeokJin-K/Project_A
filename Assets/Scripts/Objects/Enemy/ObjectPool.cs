using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    #region Protected Field

    [SerializeField]
    protected List<GameObject> objectList;    

    [SerializeField]
    protected int initPoolAmount = 20;

    #endregion

    //------------------------------------------------------------------------------------------------

    protected void AddObjectPool(GameObject prefab)     //  오브젝트 풀 리스트에 오브젝트 추가
    {               
        for(int i = 0; i < initPoolAmount; i++)
        {
            objectList.Add(Instantiate(prefab, this.transform));

            objectList[i].SetActive(false);            
        }
    }

    protected void AddObjectPoolSetParent(GameObject prefab, Transform parent)      //  트랜스폼 인자의 자식으로 오브젝트 추가
    {
        for (int i = 0; i < initPoolAmount; i++)
        {
            objectList.Add(Instantiate(prefab, parent));

            objectList[i].SetActive(false);
        }
    }
}
