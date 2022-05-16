using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisableMode
{
    Normal,
    Lerp
}

public static class DoSpriteDisable
{    
    public static IEnumerator DoDisable(this SpriteRenderer spriteRenderer, float delay, DisableMode mode)
    {
        IEnumerator ienum;

        if(mode == DisableMode.Normal)
        {
            ienum = Normal(spriteRenderer, delay);
        }
        else
        {            
            ienum = Lerp(spriteRenderer, delay);
        }

        return ienum;
    }

    public static IEnumerator Normal(SpriteRenderer spriteRenderer, float delay)
    {
        float t = delay;

        while (t >= 0f)
        {
            t -= Time.deltaTime;

            yield return null;
        }
        
        spriteRenderer.gameObject.SetActive(false);
    }

    public static IEnumerator Lerp(SpriteRenderer spriteRenderer, float delay)
    {   
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        Color color1 = spriteRenderer.color;

        float lerpT = 0f;

        float t = delay;

        Color color2 = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        while (t >= 0f)
        {
            t -= Time.deltaTime;

            yield return null;
        }

        while (lerpT <= 1f)
        {
            lerpT += Time.deltaTime;

            spriteRenderer.color = Color.Lerp(color1, color2, lerpT);

            Debug.Log(lerpT);

            yield return null;
        }

        spriteRenderer.gameObject.SetActive(false);
    }
}
