using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footPrintControl : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(fadeOut());
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
    }


    IEnumerator fadeOut()
    {
        Renderer r = GetComponent<Renderer>();
        Color c = r.material.color;
        for (float alpha = 1.0f; alpha > 0f; alpha -= Time.deltaTime * 5)
        {
            c.a = alpha;
            r.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
