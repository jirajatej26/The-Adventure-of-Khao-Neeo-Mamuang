using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bee : MonoBehaviour {

    public float move_speed = 20f;
    public float max_speed = 0f;
    public bool grounded = false;
    public bool facing_right;
    public Animator anim;
    public float cooldown = 2f;
    public bool can_attack = true;
    public bool can_move = true;
    public float hp = 2f;
    public float alert_distance = 7f;
    public float stop_distance = 1f;
    public bool alerted = false;
    public Rigidbody2D sting;
    public float sting_speed = 7f;
    public Rigidbody2D coin;

    private Rigidbody2D myRigid;
    private float distance_a;
    private float distance_b;
    private Vector3 current_position;
    private Vector3 scale;
    private float speed = 0.0001f;
    private Collider2D bee_col;
    private Rigidbody2D plasma_sting;
    private Vector3 sting_scale;
    private Vector3 player_1;
    private Vector3 player_2;
    private Rigidbody2D spawned_coin;
    private AudioSource audi_sting;
    private AudioSource audi_die;

    // Use this for initialization
    void Start()
    {

        myRigid = GetComponent<Rigidbody2D>();
        bee_col = GetComponent<Collider2D>();
        scale = transform.localScale;
        audi_sting = GameObject.Find("audi_sting").GetComponent<AudioSource>();
        audi_die = GameObject.Find("audi_mondie").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        player_1 = GameObject.Find("Player_1").transform.position;
        player_2 = GameObject.Find("Player_2").transform.position;

        Vector3 easeVelocity = myRigid.velocity;
        easeVelocity.x = myRigid.velocity.x * 0.75f;
        easeVelocity.y = myRigid.velocity.y;
        easeVelocity.z = 0.0f;

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

        if (hp <= 0)
        {
            StartCoroutine(Dying());
        }

        if (grounded && can_move)
        {

            myRigid.velocity = easeVelocity;

            if (alerted)
            {
                if (distance_a < stop_distance || distance_b < stop_distance)
                {
                    if (can_attack)
                    {
                        StartCoroutine(Attacking());
                    }
                }

                else if (distance_a <= distance_b)
                {

                    anim.SetBool("alert", true);
                    transform.position = Vector3.MoveTowards(transform.position, player_1, speed);

                    if (can_attack)
                    {
                        StartCoroutine(Attacking());
                    }

                    if (transform.position.x < current_position.x)
                    {
                        transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                        myRigid.AddForce(Vector2.left * move_speed);
                        facing_right = false;
                    }
                    else if (transform.position.x > current_position.x)
                    {
                        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                        myRigid.AddForce(Vector2.right * move_speed);
                        facing_right = true;
                    }

                }

                else if (distance_b < distance_a)
                {

                    anim.SetBool("alert", true);
                    transform.position = Vector3.MoveTowards(transform.position, player_2, speed);

                    if (can_attack)
                    {
                        StartCoroutine(Attacking());
                    }

                    if (transform.position.x < current_position.x)
                    {
                        transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                        myRigid.AddForce(Vector2.left * move_speed);
                        facing_right = false;
                    }
                    else if (transform.position.x > current_position.x)
                    {
                        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                        myRigid.AddForce(Vector2.right * move_speed);
                        facing_right = true;
                    }

                }
            }
            else if (distance_a <= alert_distance || distance_b <= alert_distance)
            {
                alerted = true;
            }

            else if (distance_a > alert_distance * 2 && distance_b > alert_distance * 2)
            {
                alerted = false;
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
            StartCoroutine(Dying());
        }

        if (col.gameObject.name == "Sword_Check")
        {
            hp = hp - Player_Attack_1.sword_damage;
            StartCoroutine(Dying());
        }

        if (col.gameObject.tag == "Bullet")
        {
            hp = hp - Player_Attack_2.bullet_damage;
            StartCoroutine(Dying());
        }
    }

    IEnumerator Attacking()
    {
        audi_sting.volume = Random.Range(0.8f, 1);
        audi_sting.Play();
        can_attack = false;
        anim.SetBool("attack", true);
        plasma_sting = Instantiate(sting, new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.rotation);
        if (facing_right)
        {
            sting_scale = plasma_sting.transform.localScale;
            plasma_sting.transform.localScale = new Vector3(-sting_scale.x, sting_scale.y, sting_scale.z);
            plasma_sting.velocity = transform.TransformDirection(new Vector2(sting_speed, 0));
        }
        else
        {
            plasma_sting.velocity = transform.TransformDirection(new Vector2(-sting_speed, 0));
        }
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(cooldown);
        can_attack = true;
    }

    IEnumerator Dying()
    {
        audi_die.Play();
        can_move = false;
        myRigid.velocity = new Vector2(0, 0);
        myRigid.gravityScale = 0f;
        bee_col.enabled = false;
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.5f);
        spawned_coin = Instantiate(coin, transform.position, coin.transform.rotation);
        spawned_coin.velocity = new Vector2(0, 2f);
        Destroy(gameObject);
    }
}
