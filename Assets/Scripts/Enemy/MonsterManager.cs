using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MonsterManager : ObjectPool
{
    #region Public Field
    public GameObject monsterPrefab;    

    [Tooltip("�ڵ� ���� ���")]
    public bool isAutoSpawn = true;
    [Tooltip("�ڵ� ���� ������(��) ����")]
    public float autoSpawnDelay = 1.0f;
    #endregion

    #region Private Field    
    List<IMoveable> monsterMoveInterfaces = new List<IMoveable>();
    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle
    private void Awake()
    {
        AddObjectPool(monsterPrefab);
        foreach(var monster in objectPool)
        {
            monsterMoveInterfaces.Add(monster.GetComponent<IMoveable>());
        }
        StartCoroutine(AutoSpawn());
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].activeInHierarchy)
            {
                monsterMoveInterfaces[i].Move();
            }
        }
    }
    #endregion

    public void Spawn()     //  ���� ���� ����
    {
        bool isSpawned = false;
        while (!isSpawned)
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    objectPool[i].SetActive(true);
                    objectPool[i].transform.position = SetRandomPosOutCamera(); 
                    isSpawned = true;
                    break;
                }
            }

            if (!isSpawned)
            {
                objectPool.Add(Instantiate(monsterPrefab, SetRandomPosOutCamera(), Quaternion.identity, this.transform));
                monsterMoveInterfaces.Add(objectPool[objectPool.Count - 1].GetComponent<IMoveable>());
                isSpawned = true;
            }
        }
    }
    IEnumerator AutoSpawn()     //  �ڵ� ����
    {
        while (true)
        {
            if (isAutoSpawn)
            {
                Spawn();
                yield return new WaitForSeconds(autoSpawnDelay);
            }
            yield return null;
        }
    }
    Vector3 SetRandomPosOutCamera() //  ī�޶� ����Ʈ �ۿ� �ش��ϴ� ���� ��ǥ ����
    {
        int spawnPointType = Random.Range(0, 4); // 0:RightSide, 1:LeftSide, 2:TopSide, 3:BottomSide

        Vector3 randomPos = new Vector3();

        switch (spawnPointType)
        {
            case 0:
                randomPos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(-0.1f, 1.1f), Camera.main.nearClipPlane));
                break;
            case 1:
                randomPos = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(-0.1f, 1.1f), Camera.main.nearClipPlane));
                break;
            case 2:
                randomPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), 1.1f, Camera.main.nearClipPlane));
                break;
            case 3:
                randomPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), -0.1f, Camera.main.nearClipPlane));
                break;
        }

        return randomPos;
    }
}