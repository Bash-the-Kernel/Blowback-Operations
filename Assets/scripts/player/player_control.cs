using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    private Vector3 moveDirection;
    public Rigidbody rb;
    public float moveSpeed;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        ProcessInputs();
    }

    void FixedUpdate()
    {
        faceMouse();
        Move();
        rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
    }
    private void OnCollisionExit(Collision collision)
    {
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = new Vector3(mousePosition.x - transform.position.x, mousePosition.z - transform.position.z,0);
        transform.right = direction;
        transform.rotation = Quaternion.Euler(90, 0, transform.rotation.eulerAngles.z);
    }


    void ProcessInputs()
    {

        //takes input, essentially getaxisraw takes WASD and the arrow keys as "directions"
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        bool shooting = Input.GetMouseButtonDown(0);

        moveDirection = new Vector3(moveX, 0, moveY).normalized;

        if (shooting)
        {
            shoot();
            //bruh
        }

    }

    void shoot()
    {
        Quaternion bullet_rot = transform.rotation;
        Vector3 bullet_pos = GameObject.Find("firepoint").transform.position;
        GameObject bullet_parent = GameObject.Find("player_parent");

        bullet_pos.y = 1;
        //print(transform.rotation.eulerAngles.x);
        bullet_rot.eulerAngles.Set(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Instantiate(bullet, bullet_pos, bullet_rot, bullet_parent.transform);
    }

    void Move()
    {
        //animate movement and then move
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * moveSpeed);

    }

}