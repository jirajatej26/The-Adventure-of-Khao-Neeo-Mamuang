using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Walker : MonoBehaviour {

    public float move_speed = 30f;
    public float max_speed = 0f;
    public bool grounded;
    public bool facing_right;
    public Animator anim;
    public bool can_move = true;
    public float hp = 10f;
    public float alert_distance = 7f;
    public float stop_distance = 1f;
    public Collider2D bomb_coilder;
    public bool alerted = false;
    public Rigidbody2D coin;
    public bool die = false;

    private float distance_a;
    private float distance_b;
    private Rigidbody2D myRigid;
    private float speed = 0.0001f;
    private Vector3 current_position;
    private Vector3 scale;
    private Vector3 player_1;
    private Vector3 player_2;
    private Rigidbody2D spawned_coin;
    private AudioSource audi_die;

    // Use this for initialization
    void Start () {

        myRigid = gameObject.GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        bomb_coilder.enabled = false;
        audi_die = GameObject.Find("audi_mondie").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

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

        if (hp <= 0 && !die)
        {
            StartCoroutine(Dying());
            die = true;
        }

        if (grounded && can_move)
        {

            myRigid.velocity = easeVelocity;

            if (alerted)
            {

                if (distance_a < stop_distance || distance_b < stop_distance)
                {
                    StartCoroutine(Attacking());
                }

                else if (distance_a <= distance_b)
                {

                    transform.position = Vector3.MoveTowards(transform.position, player_1, speed);
                    anim.SetBool("walk", true);

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
                    anim.SetBool("walk", true);

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

            else if (distance_a <= alert_distance || distance_b <= alert_distance)
            {
                alerted = true;
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

    IEnumerator Attacking()
    {
        can_move = false;
        myRigid.velocity = new Vector2(0, 0);
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.4f);
        bomb_coilder.enabled = true;
        yield return new WaitForSeconds(0.8f);
        spawned_coin = Instantiate(coin, transform.position, coin.transform.rotation);
        spawned_coin.velocity = new Vector2(0, 2f);
        Destroy(gameObject);
    }

    IEnumerator Hurting()
    {
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("hurt", false);
    }

    IEnumerator Dying()
    {
        audi_die.Play();
        can_move = false;
        myRigid.velocity = new Vector2(0, 0);
        anim.SetBool("die", true);
        yield return new WaitForSeconds(0.8f);
        spawned_coin = Instantiate(coin, transform.position, coin.transform.rotation);
        spawned_coin.velocity = new Vector2(0, 2f);
        Destroy(gameObject);
    }

}
