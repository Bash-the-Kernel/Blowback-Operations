using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask_obj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }

}
