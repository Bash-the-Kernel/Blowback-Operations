using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_control : MonoBehaviour
{
    public bool is_alive = true;
    public string state;

    public GameObject bullet;
    public GameObject muzzle_flash;

    public GameObject L_foot;
    public GameObject R_foot;
    public GameObject L_foot_red;
    public GameObject R_foot_red;

    public GameObject Score_sheet;

    public AudioSource walk;
    public float rotSpeed;
    public float moveSpeed;

    public GameObject player;

    public float time = 0f;

    public Rigidbody rb;
    public bool is_turning = false;
    public bool is_turning_left = false;

    public float hold_time;

    public Renderer enemy_render;

    public bool is_making_footprints = false;

    Quaternion target_rot = Quaternion.Euler(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Score_sheet = GameObject.Find("paper_ui");
        rb = GetComponent<Rigidbody>();
        enemy_render = GetComponent<Renderer>();
        player = GameObject.Find("player");
        StartCoroutine(make_foot_steps());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        death_check();
        action_control();
    }
    void foot_sound_controller()
    {
        if (can_they_hear_me(player))
        {
            if(can_See(player) && enemy_render.isVisible)
            {
                walk.Pause();
            }
            else
            {
                walk.UnPause();
            }
           
        }
        else
        {
            walk.Pause();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player_control>().is_alive = false;
        }
    }
        void Update()
    {
        foot_sound_controller();
        if (can_they_see_me(player))
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            is_making_footprints = false;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            if (can_they_hear_me(player))
            {
                    is_making_footprints = true;
            }
            else
            {
                is_making_footprints = false;
            }
        }
    }

    IEnumerator make_foot_steps()
    {
        while (true)
        {
            if(state == "attacking" && is_making_footprints)
            {
                Vector3 LeftFootPos = transform.Find("L_foot_pos").transform.position;
                Instantiate(L_foot_red, LeftFootPos, transform.rotation);
                yield return new WaitForSeconds(.2f);
                Vector3 RightFootPos = transform.Find("R_foot_pos").transform.position;
                Instantiate(R_foot_red, RightFootPos, transform.rotation);
            }
            else if(is_making_footprints)
            {
                Vector3 LeftFootPos = transform.Find("L_foot_pos").transform.position;
                Instantiate(L_foot, LeftFootPos, transform.rotation);
                yield return new WaitForSeconds(.2f);
                Vector3 RightFootPos = transform.Find("R_foot_pos").transform.position;
                Instantiate(R_foot, RightFootPos, transform.rotation);
            }
            yield return new WaitForSeconds(.2f);
        }

    }

    bool can_they_hear_me(GameObject target)
    {
        Vector3 direction = new Vector3(transform.position.x - target.transform.position.x, 0, transform.position.z - target.transform.position.z);

        int layerMask = 1 << 8;

        Vector3 startpos = target.transform.position;
        startpos.y = 2;
        RaycastHit hit;
        if (Physics.Raycast(startpos, direction, out hit, Mathf.Infinity, ~layerMask))
        {
            if (hit.distance < 60 && hit.collider.tag == gameObject.tag)
            {
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
    bool can_they_see_me(GameObject target)
    {
        Vector3 direction = new Vector3(transform.position.x - target.transform.position.x, 0, transform.position.z - target.transform.position.z);

        int layerMask = 1 << 8;

        Vector3 startpos = target.transform.position;
        startpos.y = 2;
        RaycastHit hit;
        if (Physics.Raycast(startpos, direction, out hit, Mathf.Infinity, ~layerMask))
        {
            if (Vector3.Angle(direction, target.transform.right) < 80 && hit.collider.tag == gameObject.tag)
            {
                Debug.DrawRay(startpos, direction * hit.distance, Color.magenta);
                return true;
            }
            else
            {
                Debug.DrawRay(startpos, direction * hit.distance, Color.cyan);
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void death_check()
    {
        if (!is_alive)
        {
            Score_sheet.GetComponent<death_counter>().deaths++;
            Destroy(gameObject);
        }
    }

    void action_control()
    {
        if (can_See(player))
        {
            state = "attacking";
            StartCoroutine(RotateTowards(0.2f, player.transform.position));
            if (enemy_render.isVisible)
            {

                if (Time.time - time >= 0.3f)
                {
                    StartCoroutine(shoot(0.1f));
                    time = Time.time;
                }
            }
            else
            {
                StartCoroutine(MoveForward(0.2f));
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
            target_rot = Quaternion.Euler(90f, 0f, -(transform.eulerAngles.y + degrees));
        }
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
        GameObject bullet_inst = Instantiate(bullet, bullet_pos, bullet_rot);
        bullet_inst.GetComponent<bullet_controller_>().owner(transform);
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
                if (is_right_closer())
                {
                    StartCoroutine(Turn(0.1f, 270f, 30f, is_turning));
                }
                else
                {
                    StartCoroutine(Turn(0.1f, 90f, 30f, is_turning));
                }
            }
            else
            {
                StartCoroutine(Turn(0.1f, 0f, 30f, is_turning));
            }

            StartCoroutine(Stop(0f));
            is_turning = true;
        }
        else if(!is_turning)
        {
            StartCoroutine(MoveForward(0f));
        }
        if (Time.time > hold_time + wait_time)
        {
            is_turning = false;
            //is_turning_left = false;
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
