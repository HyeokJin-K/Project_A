using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class NormalMonsterManager : ObjectPool
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

    List<NormalMonsterAnimation> monsterAnimationList = new List<NormalMonsterAnimation>();

    Camera mainCamera;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        mainCamera = Camera.main;

        #endregion

        AddObjectPool(monsterPrefab);

        foreach (var monster in objectList)
        {
            monsterMoveInterfaces.Add(monster.GetComponent<IMoveable>());

            monsterAnimationList.Add(monster.GetComponent<NormalMonsterAnimation>());
        }        

        StartCoroutine(AutoSpawn());        
    }

    private void Update()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeInHierarchy && monsterAnimationList[i].CheckPosInCamera(mainCamera))
            {   
                monsterAnimationList[i].UpdateSprite();
            }            
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeInHierarchy)
            {
                monsterMoveInterfaces[i].Move();
            }
        }
    }

    #endregion

    public void Spawn()     //  ���� ���� ����
    {
        bool isSpawned = false;

        for (int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeInHierarchy)
            {
                objectList[i].SetActive(true);

                objectList[i].transform.position = SetRandomPosOutCamera();

                isSpawned = true;

                break;
            }
        }

        if (!isSpawned)
        {
            objectList.Add(Instantiate(monsterPrefab, SetRandomPosOutCamera(), Quaternion.identity, this.transform));

            monsterMoveInterfaces.Add(objectList[objectList.Count - 1].GetComponent<IMoveable>());

            monsterAnimationList.Add(objectList[objectList.Count - 1].GetComponent<NormalMonsterAnimation>());

            isSpawned = true;
        }

        #region Local Method

        Vector3 SetRandomPosOutCamera() //  ī�޶� ����Ʈ �ۿ� �ش��ϴ� ���� ��ǥ ����
        {
            int spawnPointType = Random.Range(0, 4); // 0:RightSide, 1:LeftSide, 2:TopSide, 3:BottomSide

            Vector3 randomPos = new Vector3();

            switch (spawnPointType)
            {
                case 0:

                    randomPos = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(-0.1f, 1.1f), mainCamera.nearClipPlane));

                    break;

                case 1:

                    randomPos = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(-0.1f, 1.1f), mainCamera.nearClipPlane));

                    break;

                case 2:

                    randomPos = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), 1.1f, mainCamera.nearClipPlane));

                    break;

                case 3:

                    randomPos = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), -0.1f, mainCamera.nearClipPlane));

                    break;
            }

            return randomPos;
        }

        #endregion
    }

    IEnumerator AutoSpawn()     //  �ڵ� ����
    {
        while (true)
        {
            if (isAutoSpawn)
            {
                int randomSpawnAmount = Random.Range(0, 4);

                for(int i = 0; i < randomSpawnAmount; i++)
                {
                    Spawn();
                }

                yield return new WaitForSeconds(autoSpawnDelay);
            }

            yield return null;
        }
    }    
}
