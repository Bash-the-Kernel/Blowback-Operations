using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_control : MonoBehaviour
{
    public bool is_alive = true;
    string state;
    public GameObject bullet;
    public GameObject muzzle_flash;
    public float rotSpeed;
    public float moveSpeed;
    public GameObject player;
    public float time = 0f;
    public Rigidbody rb;
    public bool is_turning = false;
    public bool is_turning_left = false;
    public float hold_time;
    Quaternion target_rot = Quaternion.Euler(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        death_check();
        action_control();
    }

    void death_check()
    {
        if (!is_alive)
        {
            Destroy(gameObject);
        }
    }

    void action_control()
    {
        if (can_See(player))
        {
            state = "attacking";
            StartCoroutine(RotateTowards(0.2f, player.transform.position));
            if (Time.time - time >= 0.3f)
            {
                StartCoroutine(shoot(0.1f));
                time = Time.time;
            }
        }
        else
        {
            Roaming_mode();
        }
    }

    void MoveToPos(Vector3 position)
    {

    }

    bool can_See(GameObject target)
    {
        Vector3 direction = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);

        //now make a rotation value that can be attributed to the enemy such that it is facing the player
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion target_rot = Quaternion.AngleAxis(angle, Vector3.forward);

        //these are masks that will be used such that the ray that will be cast only hits the player and not other things like walls and other enemies
        int layerMask = 1 << 8;/*
        int mask = 1 << LayerMask.NameToLayer("player");
        mask |= 1 << LayerMask.NameToLayer("Default");*/

        Vector3 startpos = transform.position;
        startpos.y = 1;
        RaycastHit hit;
        if (Physics.Raycast(startpos, direction, out hit, Mathf.Infinity, ~layerMask))
        {

            Debug.DrawRay(startpos, direction * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            //print(Vector3.Angle(direction, transform.right));
            if (Vector3.Angle(direction, transform.right) < 80 && hit.collider.tag == target.tag)
            {
                //print("KIZZMADICK");
                //if the player is within the enemies field of vision which is 80 degrees
                //then do the running towards the player coroutine
                Debug.DrawRay(transform.position, direction * hit.distance, Color.white);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    Vector3 leading_shot_pos(Vector3 target, Rigidbody target_rb)
    {
        Vector3 position;
        float time_est = (Vector3.Distance(target, transform.position) * player.GetComponent<player_control>().moveSpeed * 1.28f / bullet.GetComponent<bullet_controller_>().bullet_speed);
        //print(time_est);
        if (target_rb.velocity.magnitude > 0)
        {
            position = target + target_rb.velocity * time_est;

        }
        else
        {
            position = target;
        }
        //print(target.x + " " + target.y + " " + target.z);
        //print(" ");
        //print(position.x + " " + position.y + " " + position.z);
        return position;


    }

    IEnumerator MoveForward(float time)
    {
        yield return new WaitForSeconds(time);
        //transform.position += transform.right * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector3(transform.right.x * moveSpeed, 0, transform.right.z * moveSpeed);
    }
    IEnumerator Stop(float time)
    {
        yield return new WaitForSeconds(time);
        //transform.position += transform.right * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator Turn(float time, float degrees, float turnspeed, bool is_turning)
    {

        yield return new WaitForSeconds(time);
        if (!is_turning)
        {
            target_rot = Quaternion.Euler(90f, 0f, transform.eulerAngles.y + degrees);
        }
        //print(target_rot.eulerAngles.y + " KUNT");
        //print(target_rot.eulerAngles.y + " YOU");
        //print(transform.rotation.eulerAngles.y + "fugg (y)");
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, turnspeed * Time.deltaTime);

    }

    IEnumerator shoot(float time)
    {
        //print("LIGMABALLS");
        yield return new WaitForSeconds(time);
        Quaternion bullet_rot = transform.rotation;
        Vector3 bullet_pos = transform.Find("enemy_firepoint").transform.position;
        Vector3 flash_pos = transform.Find("enemy_flashpoint").transform.position;
        GameObject bullet_parent = GameObject.Find("enemy_parent");

        bullet_pos.y = 3;
        //print(transform.rotation.eulerAngles.x);
        //bullet_rot.eulerAngles.Set(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Instantiate(bullet, bullet_pos, bullet_rot, transform);
        Instantiate(muzzle_flash, flash_pos, bullet_rot, transform);

    }

    IEnumerator RotateTowards(float time, Vector3 position)
    {
        
        if (position == player.transform.position)
        {
            yield return new WaitForSeconds(time);
            Vector3 lead_pos = leading_shot_pos(player.transform.position, player.GetComponent<player_control>().rb);
            //print(lead_pos.x + " " + player.transform.position.x);
            Vector3 direction = new Vector3(lead_pos.x - transform.position.x, 0,lead_pos.z - transform.position.z);
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            Quaternion target_rot = Quaternion.Euler(90f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, rotSpeed * Time.deltaTime);
        }
        else
        {
            yield return new WaitForSeconds(time);
            Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            Quaternion target_rot = Quaternion.Euler(90f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, rotSpeed * Time.deltaTime);

        }

    }

    void Roaming_mode()
    {
        //StartCoroutine(Turn(0.1f, 180f, 0.5f));
        
        float wait_time = 1.5f;
        state = "roaming";
        if (is_object_close(10f) || is_turning)
        {
            if (!is_turning)
            {
                hold_time = Time.time;
            }
            StartCoroutine(Stop(0f));
            if ((is_right_closer() && is_turning == false) || is_turning_left)
            {
                is_turning_left = true;
                StartCoroutine(Turn(0.1f, -90f, 10f, is_turning));
            }
            else
            {
                is_turning_left = false;
                StartCoroutine(Turn(0.1f, 90f, 10f, is_turning));
            }
            is_turning = true;
        }
        else if(!is_turning)
        {
            StartCoroutine(MoveForward(0f));
        }
        if (Time.time > hold_time + wait_time)
        {
            is_turning = false;
            is_turning_left = false;
        }
    }
    bool is_object_close(float dist)
    {
        int layerMask = 1 << 7;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity, ~layerMask))
        {
            //print(hit.distance);
            if (hit.distance < dist) return true;
            else return false;
        }
        return false;
            
    }
    bool is_right_closer()
    {
        int layerMask = 1 << 7;
        RaycastHit hit;
        RaycastHit hit_2;
        Vector3 right = Quaternion.Euler(0, 90, 0) * transform.right;
        Vector3 left = Quaternion.Euler(0, -90, 0) * transform.right;



        if (Physics.Raycast(transform.position, right, out hit, Mathf.Infinity, ~layerMask))
        {
            if (Physics.Raycast(transform.position, left, out hit_2, Mathf.Infinity, ~layerMask))
            {
                Debug.DrawRay(transform.position, right * hit.distance, Color.red);
                Debug.DrawRay(transform.position, left * hit_2.distance, Color.blue);
                if (hit.distance < hit_2.distance) return true;
                else return false;
            }
            return false;
        }
        return false;
    }


}
