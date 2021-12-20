using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_control : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        action_control();
    }

    void action_control()
    {

    }

    void MoveToPos(Vector3 position)
    {

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }


    void shoot()
    {
        Quaternion bullet_rot = transform.rotation;
        Vector3 bullet_pos = GameObject.Find("firepoint").transform.position;
        GameObject bullet_parent = GameObject.Find("enemy_parent");

        bullet_pos.y = 1;
        print(transform.rotation.eulerAngles.x);
        bullet_rot.eulerAngles.Set(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Instantiate(bullet, bullet_pos, bullet_rot, bullet_parent.transform);
    }

    void RotateTowards(Vector3 position)
    {

    }


}
