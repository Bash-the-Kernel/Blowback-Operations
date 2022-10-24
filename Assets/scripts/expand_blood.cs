using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expand_blood : MonoBehaviour
{
    public float size = 1;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(expand());
    }

    IEnumerator expand()
    {
        float max_size = Random.Range(5.0f, 18.0f);
        while(size < max_size)
        {
            size+= max_size/700f;
            transform.localScale = new Vector3 (size, size, 0.1f);
            yield return null;
        }
    }
}
