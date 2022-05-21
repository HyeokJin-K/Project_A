using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test22 : MonoBehaviour
{
    #region Public Field

    public List<GameObject> list = new List<GameObject>();

    #endregion    

    #region Private Field

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle
    void Start()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (list.Contains(collision.gameObject))
        {
            return;
        }

        list.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (list.Contains(collision.gameObject))
        {
            list.Remove(collision.gameObject);
        }
    }

    #endregion
}
