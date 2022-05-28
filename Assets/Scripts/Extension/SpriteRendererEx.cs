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
    public static void DoDisable(this SpriteRenderer spriteRenderer, SpriteDisableMode mode)
    {
        Color spriteOriginColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a);

        if(StaticCoroutine.instance == null)
        {
            return;
        }

        if (mode == SpriteDisableMode.Normal)
        {
            Normal(spriteRenderer);
        }
        else
        {
            StaticCoroutine.instance?.StartStaticCoroutine(Lerp(spriteRenderer));
        }

        #region Local Method

        void Normal(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.gameObject.SetActive(false);
        }

        IEnumerator Lerp(SpriteRenderer spriteRenderer)
        {
            Color color1 = spriteRenderer.color;

            float lerpT = 0f;

            Color color2 = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

            while (lerpT <= 1f)
            {
                lerpT += Time.deltaTime;

                spriteRenderer.color = Color.Lerp(color1, color2, lerpT);

                yield return null;
            }

            spriteRenderer.color = spriteOriginColor;
        }

        #endregion
    }
}
