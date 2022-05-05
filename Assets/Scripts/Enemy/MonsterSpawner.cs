using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MonsterSpawner : MonsterObjectPool
{
    public GameObject monsterPrefab;

    [Tooltip("자동 스폰 기능")]
    public bool isAutoSpawn = true;
    [Tooltip("자동 스폰 딜레이(초) 설정")]
    public float autoSpawnDelay = 1.0f;

    private void Awake()
    {
        AddMonsterObjectPool(monsterPrefab);
        StartCoroutine(AutoSpawn(autoSpawnDelay));
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }        
    }
    
    public void Spawn()
    {
        bool isSpawned = false;
        while (!isSpawned)
        {
            for (int i = 0; i < monsterObjectPool.Count; i++)
            {
                if (!monsterObjectPool[i].activeInHierarchy)
                {
                    monsterObjectPool[i].SetActive(true);
                    monsterObjectPool[i].transform.position = SetRandomPosOutCamera();
                    isSpawned = true;
                    break;
                }
            }

            if (!isSpawned)
            {
                monsterObjectPool.Add(Instantiate(monsterPrefab, SetRandomPosOutCamera(), Quaternion.identity, this.transform));
                isSpawned = true;
            }
        }
    }

    IEnumerator AutoSpawn(float delayTime)
    {
        while (true)
        {
            if (isAutoSpawn)
            {
                Spawn();
                yield return new WaitForSeconds(delayTime);
            }
            yield return null;
        }
    }
    Vector3 SetRandomPosOutCamera()
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
