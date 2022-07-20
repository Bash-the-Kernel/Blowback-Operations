using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_slerp_ANGERY : MonoBehaviour
{
    public int eye = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Turn(0.1f, 90f, 1f));

    }

    IEnumerator Turn(float time, float degrees, float turnspeed)
    {
        yield return new WaitForSeconds(time);
        Quaternion target_rot = Quaternion.Euler(0f, 0f, transform.rotation.z + degrees);
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, turnspeed * Time.deltaTime);

    }
}
