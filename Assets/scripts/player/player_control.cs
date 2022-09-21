using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_control : MonoBehaviour
{
    private Vector3 moveDirection;
    public Rigidbody rb;
    public GameObject doorway;
    public GameObject wall;
    public GameObject roof_remover;
    public List<GameObject> roof_removers;
    public float moveSpeed;
    public GameObject bullet;
    public GameObject muzzle_flash;
    public AudioSource walk;
    public bool is_alive = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in roof_removers)
        {
            Destroy(go);
        }
        see_under_rooves();
        ProcessInputs();
        death_check();
        faceMouse();
    }

    void FixedUpdate()
    {
        Move();
        rb.isKinematic = false;
    }

    void see_under_rooves()
    {
        for(int i = -60;i < 60; i++)
        {
            int layerMask = 1 << 6;
            Vector3 direction =  Quaternion.Euler(0, i, 0) * transform.right;
            RaycastHit hit;
            float distance = 0;
            if (Physics.Raycast(transform.position, direction, out hit, 100, ~layerMask))
            {
                if(hit.collider.tag == doorway.tag)
                {
                    RaycastHit hit_2;
                    layerMask = 1 << 7;

                    if (Physics.Raycast(hit.transform.position, direction, out hit_2, 100, ~layerMask))
                    {
                        if(hit_2.collider.tag == wall.tag)
                        {
                            distance = hit_2.distance + hit.distance;

                        }
                    }
                    //Debug.DrawRay(transform.position, direction, Color.green);
                    Vector3 position = transform.position + direction * distance/2;
                    Quaternion rotation = transform.rotation;
                    rotation.z += i;
                    position.y = 11f;
                    GameObject r_r_slide = Instantiate(roof_remover, position, transform.rotation);
                    r_r_slide.transform.localScale = new Vector3(distance,2,1);
                    r_r_slide.transform.Rotate(0,0,-i);
                    roof_removers.Add(r_r_slide);
                    if (hit.collider.tag == wall.tag)
                    {
                        //print("yes");
                    }
                    if (hit.collider.tag != wall.tag)
                    {
                        //print("no");
                    }
                }
                else
                {
                    //Debug.DrawRay(transform.position, direction, Color.yellow);
                }
            }
        }
    }

    void death_check()
    {
        if (!is_alive)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
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
        Debug.DrawRay(transform.position, transform.right, Color.green);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = new Vector3(mousePosition.x - transform.position.x, 0, mousePosition.z - transform.position.z);
        transform.right = direction;
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
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
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
    void shoot()
    {
        Quaternion bullet_rot = transform.rotation;
        Vector3 bullet_pos = GameObject.Find("firepoint").transform.position;
        Vector3 flash_pos = GameObject.Find("flashpoint").transform.position;
        GameObject bullet_parent = GameObject.Find("player_parent");

        bullet_pos.y = 3;
        //print(transform.rotation.eulerAngles.x);
        bullet_rot.eulerAngles.Set(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        GameObject bullet_inst = Instantiate(bullet, bullet_pos, bullet_rot);
        bullet_inst.GetComponent<bullet_controller_>().owner(transform);
        Instantiate(muzzle_flash, flash_pos, bullet_rot, transform);

    }

    void Move()
    {
        //animate movement and then move
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * moveSpeed);
        
        if(moveDirection.x != 0 || moveDirection.z != 0)
        {
            walk.UnPause();
        }
        else
        {
            walk.Pause();
        }
    }

}