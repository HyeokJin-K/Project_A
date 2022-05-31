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

    protected void AddObjectPool(GameObject prefab)     //  ������Ʈ Ǯ ����Ʈ�� ������Ʈ �߰�
    {               
        for(int i = 0; i < initPoolAmount; i++)
        {
            objectList.Add(Instantiate(prefab, this.transform));

            objectList[i].SetActive(false);            
        }
    }

    protected void AddObjectPoolSetParent(GameObject prefab, Transform parent)      //  Ʈ������ ������ �ڽ����� ������Ʈ �߰�
    {
        for (int i = 0; i < initPoolAmount; i++)
        {
            objectList.Add(Instantiate(prefab, parent));

            objectList[i].SetActive(false);
        }
    }
}
