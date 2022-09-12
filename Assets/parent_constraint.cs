using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent_constraint : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        parent_constraint parent_const = transform.GetComponent<parent_constraint>();
    }
}
