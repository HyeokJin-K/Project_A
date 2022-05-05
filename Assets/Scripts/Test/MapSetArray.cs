using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSetArray : MonoBehaviour
{
    public Transform playerTransform;    
    public GameObject[] mapObjectArray; // [1]북 [4]중앙 [3]서 [5]동 [7]북


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
        //  [0][1][2]    [2][0][1]      기존 2,5,8은 0,3,6자리로 옮긴다
        //  [3][4][5] -> [5][3][4]      기존 0,3,6은 1,4,7자리로 옮긴다
        //  [6][7][8]    [8][6][7]      기존 1,4,7은 2,5,8자리로 옮긴다
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
