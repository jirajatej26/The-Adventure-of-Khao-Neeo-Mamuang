using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AI_Boss_2 : MonoBehaviour {

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
    public GameObject totem_1;
    public GameObject totem_2;
    public bool can_attack = false;
    public GameObject shield;
    public bool totem_ready_1;
    public bool totem_ready_2;
    public Image hp_foreground;
    public float damage = 0;
    public bool die_check = false;

    public static bool charged = false;

    private float distance_a;
    private float distance_b;
    private Rigidbody2D myRigid;
    private float speed = 0.0001f;
    private Vector3 current_position;
    private Vector3 scale;
    private float check_time = 100000f;
    private bool check = false;
    private bool move_up = false;
    private Vector3 player_1;
    private Vector3 player_2;

    // Use this for initialization
    void Start()
    {

        myRigid = gameObject.GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        smash.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            hp = 0;
        }

        player_1 = GameObject.Find("Player_1").transform.position;
        player_2 = GameObject.Find("Player_2").transform.position;

        totem_ready_1 = Totem_1.totem_ready_1;
        totem_ready_2 = Totem_2.totem_ready_2;

        Vector3 easeVelocity = myRigid.velocity;
        easeVelocity.x = myRigid.velocity.x * 0.75f;
        easeVelocity.y = myRigid.velocity.y;
        easeVelocity.z = 0.0f;

        distance_a = Vector3.Distance(transform.position, player_1);
        distance_b = Vector3.Distance(transform.position, player_2);

        current_position = transform.position;

        if (damage > 0)
        {
            hp = hp - 0.5f;
            damage = damage - 0.5f;
        }

        hp_foreground.fillAmount = hp / 100f;

        if (totem_ready_1 && totem_ready_2 && !die_check)
        {
            shield.SetActive(true);
            can_attack = true;
            anim.SetBool("stun", false);
        }
        else
        {
            shield.SetActive(false);
            can_attack = false;
            anim.SetBool("stun", true);
        }
        
        if (myRigid.velocity.x > max_speed)
        {
            myRigid.velocity = new Vector2(max_speed, myRigid.velocity.y);
        }
        else if (myRigid.velocity.x < -max_speed)
        {
            myRigid.velocity = new Vector2(-max_speed, myRigid.velocity.y);
        }

        if (hp <= 0 && !die_check)
        {
            die_check = true;
            StartCoroutine("Dying");
        }

        if (alert && !check)
        {
            check_time = Time.time + charge_timer;
            check = true;
        }

        else if (Time.time > check_time && can_attack && !die_check)
        {
            StartCoroutine(Charging());
            check_time = Time.time + charge_timer;
        }

        if (grounded && can_move && !die_check)
        {

            myRigid.velocity = easeVelocity;

            if (distance_a <= alert_distance && distance_a <= distance_b && can_attack)
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

            else if (distance_b <= alert_distance && distance_b < distance_a && can_attack)
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
        if (col.gameObject.name == "Punch_Check" && !can_attack)
        {
            damage = damage + Player_Attack_1.punch_damage;
            StartCoroutine(Hurting());
        }

        if (col.gameObject.name == "Sword_Check" && !can_attack)
        {
            damage = damage + Player_Attack_1.sword_damage;
            StartCoroutine(Hurting());
        }

        if (col.gameObject.tag == "Bullet" && !can_attack)
        {
            damage = damage + Player_Attack_2.bullet_damage;
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
        can_attack = false;
        can_move = false;
        Player_Movement_1.can_move = false;
        Player_Movement_2.can_move = false;
        shield.SetActive(false);
        myRigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Player_Movement_1.can_move = true;
        Player_Movement_2.can_move = true;
        SceneManager.LoadScene("CS_B2to3");
    }

}
