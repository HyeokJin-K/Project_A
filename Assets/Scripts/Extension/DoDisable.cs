using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDisable : MonoBehaviour
{
    #region Public Field
    
    public DisableMode mode;

    public float waitTime;

    #endregion    

    #region Private Field

    public enum DisableMode
    {
        Normal,
        Lerp
    }

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void OnEnable()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);

        StartCoroutine(Disable());
    }

    #endregion

    IEnumerator Disable()
    {
        float t = waitTime;

        switch (mode)
        {
            case DisableMode.Normal:

                while (t >= 0f)
                {
                    t -= Time.deltaTime;

                    yield return null;
                }

                gameObject.SetActive(false);

                break;

            case DisableMode.Lerp:

                SpriteRenderer sprite = GetComponent<SpriteRenderer>();

                float lerpT = 0f;

                Color color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);

                while (t >= 0f)
                {
                    t -= Time.deltaTime;

                    yield return null;
                }

                while(lerpT <= 1f)
                {
                    lerpT += Time.deltaTime * 0.5f;

                    sprite.color = Color.Lerp(sprite.color, color, lerpT);

                    print(lerpT);

                    yield return null;
                }
                
                gameObject.SetActive(false);

                break;
        }        
    }
}
