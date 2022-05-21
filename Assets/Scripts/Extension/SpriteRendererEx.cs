using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteDisableMode
{
    Normal,
    Lerp
}

public static class SpriteRendererEx
{
    public static void DoDisable(this SpriteRenderer spriteRenderer, float delay, SpriteDisableMode mode)
    {
        if (mode == SpriteDisableMode.Normal)
        {
            StaticCoroutine.instance?.StartStaticCoroutine(Normal(spriteRenderer, delay));
        }
        else
        {
            StaticCoroutine.instance?.StartStaticCoroutine(Lerp(spriteRenderer, delay));
        }

        IEnumerator Normal(SpriteRenderer spriteRenderer, float delay)
        {
            yield return new WaitForSeconds(delay);

            spriteRenderer.gameObject.SetActive(false);
        }

        IEnumerator Lerp(SpriteRenderer spriteRenderer, float delay)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

            Color color1 = spriteRenderer.color;

            float lerpT = 0f;

            float t = delay;

            Color color2 = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

            yield return new WaitForSeconds(delay);

            while (lerpT <= 1f)
            {
                lerpT += Time.deltaTime;

                spriteRenderer.color = Color.Lerp(color1, color2, lerpT);

                yield return null;
            }

            spriteRenderer.gameObject.SetActive(false);
        }
    }
}
