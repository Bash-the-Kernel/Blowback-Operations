using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller_ : MonoBehaviour
{
    public Rigidbody rb;
    public Collider col;
    public float bullet_speed;
    // Start is called before the first frame update
    private void Awake()
    {
        print("bruh wtf");
        rb.AddForce(transform.right * bullet_speed);
        Physics.IgnoreCollision(col, transform.parent.GetChild(0).GetComponent<Collider>(), true);


    }
    

    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
