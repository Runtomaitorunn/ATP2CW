using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public SpriteRenderer childRenderer; 


    void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < 3f)
        {
            float t = timer / 3f;
            t = Mathf.Clamp01(t);

                Color color = childRenderer.color;
                color.a = Mathf.Lerp(0f, 1f, t);
                childRenderer.color = color;


            timer += Time.deltaTime;
            yield return null;
        }
    }
    public void DestroyMonk()
    {
        Destroy(gameObject);
    }
}
