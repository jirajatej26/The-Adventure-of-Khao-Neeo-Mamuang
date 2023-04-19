using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dieshit_2 : MonoBehaviour {

    public float move_speed = 50f;
    public float max_speed = 5f;
    public bool grounded;
    public bool facing_right;
    public bool can_move = true;
    public bool hurting = false;
    public float hp = 1f;
    public GameObject die_col;

    private float distance_a;
    private float distance_b;
    private Rigidbody2D myRigid;
    private float speed = 0.0001f;
    private Vector3 current_position;
    private Vector3 scale;
    private Animator anim;
    private Vector3 player_1;
    private Vector3 player_2;
    private AudioSource audi_die;

    // Use this for initialization
    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        audi_die = GameObject.Find("audi_mondie").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        player_1 = GameObject.Find("Player_1").transform.position;
        player_2 = GameObject.Find("Player_2").transform.position;

        distance_a = Vector3.Distance(transform.position, player_1);
        distance_b = Vector3.Distance(transform.position, player_2);

        current_position = transform.position;

        if (myRigid.velocity.x > max_speed)
        {
            myRigid.velocity = new Vector2(max_speed, myRigid.velocity.y);
        }
        else if (myRigid.velocity.x < -max_speed)
        {
            myRigid.velocity = new Vector2(-max_speed, myRigid.velocity.y);
        }

        if (grounded && can_move && !hurting)
        {

            if (distance_a <= distance_b)
            {

                transform.position = Vector3.MoveTowards(transform.position, player_1, speed);

                if (transform.position.x < current_position.x)
                {
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    myRigid.AddForce(Vector2.left * move_speed);
                }
                else if (transform.position.x > current_position.x)
                {
                    transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    myRigid.AddForce(Vector2.right * move_speed);
                }

            }

            else if (distance_b < distance_a)
            {

                transform.position = Vector3.MoveTowards(transform.position, player_2, speed);

                if (transform.position.x < current_position.x)
                {
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    myRigid.AddForce(Vector2.left * move_speed);
                }
                else if (transform.position.x > current_position.x)
                {
                    transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    myRigid.AddForce(Vector2.right * move_speed);
                }
            }


        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Punch_Check")
        {
            hp = hp - Player_Attack_1.punch_damage;
            StartCoroutine(Hurting());
        }

        if (col.gameObject.name == "Sword_Check")
        {
            hp = hp - Player_Attack_1.sword_damage;
            StartCoroutine(Hurting());
        }

        if (col.gameObject.tag == "Bullet")
        {
            hp = hp - Player_Attack_2.bullet_damage;
            StartCoroutine(Hurting());
        }
    }

    IEnumerator Hurting()
    {
        audi_die.Play();
        hurting = true;
        die_col.SetActive(false);
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
