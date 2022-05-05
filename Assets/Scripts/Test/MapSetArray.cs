using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSetArray : MonoBehaviour
{
    public Transform playerTransform;    
    public GameObject[] mapObjectArray; // [1]�� [4]�߾� [3]�� [5]�� [7]��


    Vector2 centerPos;
    float tileHalfSize;    

    private void Start()
    {
        tileHalfSize = Mathf.Abs(mapObjectArray[4].GetComponent<SpriteRenderer>().bounds.min.x);
        centerPos = mapObjectArray[4].transform.position;        
    }
    void Update()
    {
       MoveSetTile();
    }

    public void MoveSetTile()
    {
        //                              mapObjectArray,  CopyMapArray
        //  [0][1][2]    [2][0][1]      ���� 2,5,8�� 0,3,6�ڸ��� �ű��
        //  [3][4][5] -> [5][3][4]      ���� 0,3,6�� 1,4,7�ڸ��� �ű��
        //  [6][7][8]    [8][6][7]      ���� 1,4,7�� 2,5,8�ڸ��� �ű��
        if (playerTransform.position.x < mapObjectArray[4].transform.position.x - tileHalfSize)
        {
            GameObject[] copyMapArray = new GameObject[9];
            System.Array.Copy(mapObjectArray, copyMapArray, 9);

            for (int i = 0; i < 9; i++)
            {
                if(i % 3 == 2)
                {
                    copyMapArray[i - 2] = mapObjectArray[i];
                    mapObjectArray[i].transform.position = new Vector2(mapObjectArray[i].transform.position.x - tileHalfSize * 6f,
                                                                            mapObjectArray[i].transform.position.y);
                }
                else
                {
                    copyMapArray[i + 1] = mapObjectArray[i];
                }
            }
            mapObjectArray = copyMapArray;
        }                         
        //  [0][1][2]    [1][2][0]      
        //  [3][4][5] -> [3][4][3]      
        //  [6][7][8]    [7][8][6]     
        else if (playerTransform.position.x > mapObjectArray[4].transform.position.x + tileHalfSize)
        {
            GameObject[] copyMapArray = new GameObject[9];
            System.Array.Copy(mapObjectArray, copyMapArray, 9);

            for(int i = 0; i < 9; i++)
            {
                if(i % 3 == 0)
                {
                    copyMapArray[i + 2] = mapObjectArray[i];
                    mapObjectArray[i].transform.position = new Vector2(mapObjectArray[i].transform.position.x + tileHalfSize * 6f,
                                                        mapObjectArray[i].transform.position.y);
                }
                else
                {
                    copyMapArray[i - 1] = mapObjectArray[i];
                }                
            }
            mapObjectArray = copyMapArray;
        }
        //  [0][1][2]    [6][7][8]      
        //  [3][4][5] -> [0][1][2]      
        //  [6][7][8]    [3][4][5] 
        else if (playerTransform.position.y > mapObjectArray[4].transform.position.y + tileHalfSize)
        {
            GameObject[] copyMapArray = new GameObject[9];
            System.Array.Copy(mapObjectArray, copyMapArray, 9);

            for(int i = 0; i < 9; i++)
            {
                if(i > 5)
                {
                    copyMapArray[i - 6] = mapObjectArray[i];
                    mapObjectArray[i].transform.position = new Vector2(mapObjectArray[i].transform.position.x,
                                                        mapObjectArray[i].transform.position.y + tileHalfSize * 6f);
                }
                else
                {
                    copyMapArray[i + 3] = mapObjectArray[i];
                }
            }
            mapObjectArray = copyMapArray;
        }
        //  [0][1][2]    [3][4][5]      
        //  [3][4][5] -> [6][7][8]      
        //  [6][7][8]    [0][1][2] 
        else if (playerTransform.position.y < mapObjectArray[4].transform.position.y - tileHalfSize)
        {
            GameObject[] copyMapArray = new GameObject[9];
            System.Array.Copy(mapObjectArray, copyMapArray, 9);

            for (int i = 0; i < 9; i++)
            {
                if (i < 3)
                {
                    copyMapArray[i + 6] = mapObjectArray[i];
                    mapObjectArray[i].transform.position = new Vector2(mapObjectArray[i].transform.position.x,
                                                        mapObjectArray[i].transform.position.y - tileHalfSize * 6f);
                }
                else
                {
                    copyMapArray[i - 3] = mapObjectArray[i];
                }
            }
            mapObjectArray = copyMapArray;
        }

    }
}
