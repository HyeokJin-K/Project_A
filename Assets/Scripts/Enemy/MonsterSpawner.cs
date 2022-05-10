using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MonsterSpawner : ObjectPool
{
    public GameObject monsterPrefab;

    [Tooltip("자동 스폰 기능")]
    public bool isAutoSpawn = true;
    [Tooltip("자동 스폰 딜레이(초) 설정")]
    public float autoSpawnDelay = 1.0f;

    private void Awake()
    {
        AddObjectPool(monsterPrefab);
        StartCoroutine(AutoSpawn());
    }    
    
    public void Spawn()     //  몬스터 수동 스폰
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
                isSpawned = true;
            }
        }
    }
    IEnumerator AutoSpawn()     //  자동 스폰
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
    Vector3 SetRandomPosOutCamera() //  카메라 뷰포트 밖에 해당하는 랜덤 좌표 지정
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
