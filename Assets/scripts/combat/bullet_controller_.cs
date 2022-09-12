using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller_ : MonoBehaviour
{
    public AudioSource bang;
    public Rigidbody rb;
    public Collider col;
    public float bullet_speed;
    // Start is called before the first frame update
    private void Awake()
    {
        //print("bruh wtf");
        rb.AddForce(transform.right * bullet_speed);
        Destroy(gameObject, 5);


    }

    public void owner(Transform owner)
    {
        Physics.IgnoreCollision(col, owner.GetComponent<CapsuleCollider>(), true);
        bang = owner.GetChild(4).GetComponent<AudioSource>();
        bang.Play();
    }

    


    // Update is called once per frame
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player_control>().is_alive = false;
        }
        else if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<enemy_control>().is_alive = false;
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
