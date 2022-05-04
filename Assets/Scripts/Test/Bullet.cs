using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.up * Time.deltaTime;
        Delete();
    }

    void Delete()
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y));        
        if(pos.x > -0.1f && pos.x < 1.1f && pos.y > -0.1f && pos.y < 1.1f)
        {

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
