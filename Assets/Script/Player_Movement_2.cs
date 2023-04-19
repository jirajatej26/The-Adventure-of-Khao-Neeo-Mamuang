using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_2 : MonoBehaviour
{
    public Animator anim;
    public float speed = 50f;
    public float max_speed = 3;
    public float jump_power = 200;
    public static bool facing_right = true;
    public bool attacking;
    public float ladder_speed = 50f;
    public float max_ladder = 3f;
    public bool on_ladder;
    public bool grounded;
    public bool jumping = false;
    public float knockback_power_dir = 300f;
    public float knockback_power_up = 50f;
    public float bomb_power = 500f;
    public float smash_power = 1000f;

    public static bool can_move = true;

    private Rigidbody2D myRigid;
    private Vector3 scale;
    private bool knockbacking = false;
    private AudioSource audi;

    // Use this for initialization

    void Start()
    {

        myRigid = gameObject.GetComponent<Rigidbody2D>();
        audi = gameObject.GetComponent<AudioSource>();

        scale = transform.localScale;
        can_move = true;

    }

    void Update()
    {

        attacking = anim.GetCurrentAnimatorStateInfo(0).IsName("MM_Rattack_01") || anim.GetCurrentAnimatorStateInfo(0).IsName("MM_Lattack_01");

        anim.SetBool("right_side", facing_right);

        if (Input.GetKeyDown(KeyCode.V) && Mid_Point.potion[Mid_Point.selected] > 0)
        {
            StartCoroutine(Drinking());
        }

        if (grounded)
        {
            if (Input.GetButtonDown("Jump_2") && !jumping)
            {
                anim.SetBool("jump", true);
                myRigid.AddForce(Vector2.up * jump_power);
                jumping = true;
            }

        }

        if (jumping && anim.GetCurrentAnimatorStateInfo(0).IsName("MM_Ljumponair") || anim.GetCurrentAnimatorStateInfo(0).IsName("MM_Rjumponair"))
        {
            if (grounded)
            {
                jumping = false;
                anim.SetBool("jump", false);
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 easeVelocity = myRigid.velocity;
        easeVelocity.x = myRigid.velocity.x * 0.75f;
        easeVelocity.y = myRigid.velocity.y;
        easeVelocity.z = 0.0f;

        if (Input.GetAxis("Horizontal_2") > 0.1f && can_move && !attacking)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            anim.SetInteger("horizontal", 1);
            facing_right = true;
            anim.SetBool("drink", false);
            if (!audi.isPlaying && grounded)
            {
                audi.volume = Random.Range(0.5f, 1);
                audi.pitch = Random.Range(0.5f, 1);
                audi.Play();
            }
        }

        else if (Input.GetAxis("Horizontal_2") < -0.1f && can_move && !attacking)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            anim.SetInteger("horizontal", -1);
            facing_right = false;
            anim.SetBool("drink", false);
            if (!audi.isPlaying && grounded)
            {
                audi.volume = Random.Range(0.7f, 1);
                audi.pitch = Random.Range(0.5f, 1);
                audi.Play();
            }
        }

        else
        {
            anim.SetInteger("horizontal", 0);
        }

        if (grounded)
        {
            myRigid.velocity = easeVelocity;
        }

        float horizontal = Input.GetAxis("Horizontal_2");
        if (can_move && !attacking)
        {
            myRigid.AddForce((Vector2.right * speed) * horizontal);
        }

        if (myRigid.velocity.x > max_speed)
        {
            myRigid.velocity = new Vector2(max_speed, myRigid.velocity.y);
        }

        if (myRigid.velocity.x < -max_speed)
        {
            myRigid.velocity = new Vector2(-max_speed, myRigid.velocity.y);
        }

        Vector3 wallVelocity = myRigid.velocity;

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
        if (col.gameObject.tag == "Bomb")
        {
            Mid_Point.damage = Mid_Point.damage + 7f;
            if (transform.position.x < col.transform.position.x)
            {
                myRigid.AddForce(Vector2.left * bomb_power);
            }
            else if (transform.position.x > col.transform.position.x)
            {
                myRigid.AddForce(Vector2.right * bomb_power);
            }
        }
        if (col.gameObject.tag == "Smash")
        {
            Mid_Point.damage = Mid_Point.damage + 7f;
            if (transform.position.x < col.transform.position.x)
            {
                StartCoroutine(Knockback_left());
            }
            else if (transform.position.x > col.transform.position.x)
            {
                StartCoroutine(Knockback_right());
            }
        }
        if (col.gameObject.tag == "Sting" && !attacking)
        {
            Mid_Point.damage = Mid_Point.damage + 3f;
            if (transform.position.x < col.transform.position.x)
            {
                StartCoroutine(Knockback_left());
            }
            else if (transform.position.x > col.transform.position.x)
            {
                StartCoroutine(Knockback_right());
            }
        }
        if (col.gameObject.tag == "Wave")
        {
            Mid_Point.damage = Mid_Point.damage + 7f;
            if (transform.position.x < col.transform.position.x)
            {
                StartCoroutine(Knockback_left());
            }
            else if (transform.position.x > col.transform.position.x)
            {
                StartCoroutine(Knockback_right());
            }
        }
        if (col.gameObject.tag == "Missile")
        {
            Mid_Point.damage = Mid_Point.damage + 3f;
            if (transform.position.x < col.transform.position.x)
            {
                StartCoroutine(Knockback_left());
            }
            else if (transform.position.x > col.transform.position.x)
            {
                StartCoroutine(Knockback_right());
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bee" && !attacking)
        {
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 2f;
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 2f;
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Dieshit" && !attacking)
        {
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 1f;
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 1f;
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Fire" && !attacking)
        {
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 7f;
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 7f;
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Boss" && !attacking)
        {
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 2f;
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 2f;
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Laser")
        {
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 7f;
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    Mid_Point.damage = Mid_Point.damage + 7f;
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Spike")
        {
            Mid_Point.damage = Mid_Point.damage + 0.1f;
            if (transform.position.x < col.transform.position.x)
            {
                if (!knockbacking)
                {
                    StartCoroutine(Knockback_left());
                }
            }
            else if (transform.position.x > col.transform.position.x)
            {
                if (!knockbacking)
                {
                    StartCoroutine(Knockback_right());
                }
            }
        }
        if (col.gameObject.tag == "Ladder")
        {
            if (Input.GetAxis("Vertical_2") > 0.1f)
            {
                can_move = false;
                anim.SetBool("climb", true);
                on_ladder = true;
                myRigid.gravityScale = 0f;
                myRigid.AddForce(Vector2.up * ladder_speed);
                if (myRigid.velocity.y > max_ladder)
                {
                    myRigid.velocity = new Vector2(0f, max_ladder);
                }
            }
            else if (Input.GetAxis("Vertical_2") < -0.1f)
            {
                can_move = false;
                anim.SetBool("climb", true);
                on_ladder = true;
                myRigid.gravityScale = 0f;
                myRigid.AddForce(Vector2.down * ladder_speed);
                if (myRigid.velocity.y < -max_ladder)
                {
                    myRigid.velocity = new Vector2(0f, -max_ladder);
                }
            }
            else if (on_ladder)
            {
                can_move = false;
                myRigid.gravityScale = 0f;
                myRigid.velocity = new Vector2(0f, 0f);
            }
            if (Input.GetAxis("Horizontal_2") > 0.1f)
            {
                can_move = true;
                anim.SetBool("climb", false);
                on_ladder = false;
                myRigid.gravityScale = 1f;
            }
            else if (Input.GetAxis("Horizontal_2") < -0.1f)
            {
                can_move = true;
                anim.SetBool("climb", false);
                on_ladder = false;
                myRigid.gravityScale = 1f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            can_move = true;
            anim.SetBool("climb", false);
            on_ladder = false;
            myRigid.gravityScale = 1f;
        }
    }

    IEnumerator Knockback_left()
    {
        knockbacking = true;
        can_move = false;
        anim.SetBool("hurt", true);
        myRigid.AddForce(Vector2.left * knockback_power_dir);
        myRigid.AddForce(Vector2.up * knockback_power_up);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("hurt", false);
        can_move = true;
        yield return new WaitForSeconds(2f);
        knockbacking = false;
    }

    IEnumerator Knockback_right()
    {
        knockbacking = true;
        can_move = false;
        anim.SetBool("hurt", true);
        myRigid.AddForce(Vector2.right * knockback_power_dir);
        myRigid.AddForce(Vector2.up * knockback_power_up);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("hurt", false);
        can_move = true;
        yield return new WaitForSeconds(2f);
        knockbacking = false;
    }

    IEnumerator Smash_left()
    {
        Mid_Point.damage = Mid_Point.damage + 3f;
        can_move = false;
        anim.SetBool("hurt", true);
        myRigid.AddForce(Vector2.left * smash_power);
        myRigid.AddForce(Vector2.up * knockback_power_up);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("hurt", false);
        can_move = true;
    }

    IEnumerator Smash_right()
    {
        Mid_Point.damage = Mid_Point.damage + 3f;
        can_move = false;
        anim.SetBool("hurt", true);
        myRigid.AddForce(Vector2.right * smash_power);
        myRigid.AddForce(Vector2.up * knockback_power_up);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("hurt", false);
        can_move = true;
    }

    IEnumerator Drinking()
    {
        anim.SetBool("drink", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("drink", false);
    }

}
