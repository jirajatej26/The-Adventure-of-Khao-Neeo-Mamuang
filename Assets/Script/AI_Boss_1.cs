using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AI_Boss_1 : MonoBehaviour {

    public float move_speed = 30f;
    public float max_speed = 1f;
    public bool grounded;
    public Animator anim;
    public bool can_move = true;
    public float hp = 100f;
    public float alert_distance = 15f;
    public float stop_distance = 1.2f;
    public Collider2D smash;
    public float charge_timer = 10f;
    public float charge_power = 1000f;
    public bool facing_right;
    public bool alert = false;
    public Image hp_foreground;
    public GameObject boss_col;
    public Rigidbody2D door;

    public static bool charged = false;

    private Vector3 player_1;
    private Vector3 player_2;
    private float distance_a;
    private float distance_b;
    private Rigidbody2D myRigid;
    private float speed = 0.0001f;
    private Vector3 current_position;
    private Vector3 scale;
    private float check_time = 100000f;
    private bool check = false;
    private float damage = 0;
    private AudioSource audi_punch;

    // Use this for initialization
    void Start()
    {

        myRigid = gameObject.GetComponent<Rigidbody2D>();   
        scale = transform.localScale;
        smash.enabled = false;
        audi_punch = GameObject.Find("audi_boss_punch").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
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

        if (damage > 0)
        {
            hp = hp - 0.25f;
            damage = damage - 0.25f;
        }

        hp_foreground.fillAmount = hp / 100f;

        if (myRigid.velocity.x > max_speed)
        {
            myRigid.velocity = new Vector2(max_speed, myRigid.velocity.y);
        }
        else if (myRigid.velocity.x < -max_speed)
        {
            myRigid.velocity = new Vector2(-max_speed, myRigid.velocity.y);
        }

        if (hp - damage <= 0)
        {
            can_move = false;
            myRigid.velocity = new Vector2(0, 0);
            boss_col.SetActive(false);
            anim.SetBool("alert", false);
            StartCoroutine(Dying());
        }

        if (alert && !check)
        {
            check_time = Time.time + charge_timer;
            check = true;
        }

        else if (Time.time > check_time && can_move)
        {
            StartCoroutine(Charging());
            check_time = Time.time + charge_timer;
        }

        if (grounded && can_move)
        {

            myRigid.velocity = easeVelocity;

            if (distance_a <= alert_distance && distance_a <= distance_b)
            {

                transform.position = Vector3.MoveTowards(transform.position, player_1, speed);
                anim.SetBool("alert", true);
                alert = true;

                if (transform.position.x < current_position.x)
                {
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    if (distance_a < stop_distance)
                    {
                        StartCoroutine(Attacking());
                    }
                    else
                    {
                        myRigid.AddForce(Vector2.left * move_speed);
                        facing_right = false;
                    }
                }
                else if (transform.position.x > current_position.x)
                {
                    transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    if (distance_a < stop_distance)
                    {
                        StartCoroutine(Attacking());
                    }
                    else
                    {
                        myRigid.AddForce(Vector2.right * move_speed);
                        facing_right = true;
                    }
                }


            }

            else if (distance_b <= alert_distance && distance_b < distance_a)
            {

                transform.position = Vector3.MoveTowards(transform.position, player_2, speed);
                anim.SetBool("alert", true);
                alert = true;

                if (transform.position.x < current_position.x)
                {
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    if (distance_b < stop_distance)
                    {
                        StartCoroutine(Attacking());
                    }
                    else
                    {
                        myRigid.AddForce(Vector2.left * move_speed);
                        facing_right = false;
                    }
                }
                else if (transform.position.x > current_position.x)
                {
                    transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    if (distance_b < stop_distance)
                    {
                        StartCoroutine(Attacking());
                    }
                    else
                    {
                        myRigid.AddForce(Vector2.right * move_speed);
                        facing_right = true;
                    }
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
            damage = damage + (Player_Attack_1.punch_damage);
            StartCoroutine(Hurting());
        }

        if (col.gameObject.name == "Sword_Check")
        {
            damage = damage + (Player_Attack_1.sword_damage);
            StartCoroutine(Hurting());
        }

        if (col.gameObject.tag == "Bullet")
        {
            damage = damage + (Player_Attack_2.bullet_damage);
            StartCoroutine(Hurting());
        }
    }

    IEnumerator Attacking()
    {
        can_move = false;
        myRigid.velocity = new Vector2(0, 0);
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        smash.enabled = true;
        audi_punch.Play();
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("attack", false);
        smash.enabled = false;
        yield return new WaitForSeconds(0.2f);
        can_move = true;
    }

    IEnumerator Hurting()
    {
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("hurt", false);
    }

    IEnumerator Charging()
    {
        can_move = false;
        myRigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(2f);
        max_speed = charge_power;
        charged = true;
        if (facing_right)
        {
            myRigid.AddForce(Vector2.right * charge_power);
        }
        else
        {
            myRigid.AddForce(Vector2.left * charge_power);
        }
        yield return new WaitForSeconds(2f);
        charged = false;
        can_move = true;
        max_speed = 1f;
    }

    IEnumerator Dying()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("CS_B1to2-2");
    }

}
