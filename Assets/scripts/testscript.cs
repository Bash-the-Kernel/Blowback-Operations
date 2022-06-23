using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;

    public float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        Vector3 moveDirection = new Vector3(moveX, 0, moveY).normalized;
        rb.MovePosition(rb.position + moveSpeed * moveDirection * Time.deltaTime);
        //rb.MovePosition(new Vector3(100, 100, 100));
    }
}
