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
        //print("bruh wtf");
        rb.AddForce(transform.right * bullet_speed);
        Physics.IgnoreCollision(col, transform.parent.GetChild(0).GetComponent<Collider>(), true);


    }

    IEnumerator destroy_after(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    

    void Start()
    {
        destroy_after(10.0f);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
