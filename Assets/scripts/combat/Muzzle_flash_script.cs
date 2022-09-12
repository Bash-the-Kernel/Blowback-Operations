using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle_flash_script : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 0.05f);
    }
}
