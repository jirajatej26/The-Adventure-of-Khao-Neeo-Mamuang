using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_2 : MonoBehaviour {

    public Animator anim;
    public float firerate_1 = 0.25f;
    public float bullet_speed = 7f;
    public Rigidbody2D ammo;
    public Rigidbody2D wall;
    public float cooldown = 6f;
    public static float bullet_damage = 2f;
    public static int type = 1;
    public static int skill = 0;

    private  bool facing_right;
    private float nextfire;
    private Rigidbody2D bullet;
    private Rigidbody2D plasma_wall;
    private bool can_defend = true;
    private Vector3 ammo_scale;
    private AudioSource audi_bullet;
    private AudioSource audi_wall;

    public static bool shield_ready = true;
    public static bool can_attack = true;

	// Use this for initialization
	void Start () {

        ammo_scale = ammo.transform.localScale;
        audi_bullet = GameObject.Find("audi_mmatk").GetComponent<AudioSource>();
        audi_wall = GameObject.Find("audi_base").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        facing_right = Player_Movement_2.facing_right;
	
        if (Input.GetButton("Fire_1") && Time.time > nextfire && can_attack)
        {
            anim.SetBool("drink", false);
            Player_Movement_2.can_move = false;
            anim.SetBool("attack", true);
            StartCoroutine(Attacking());
            if (type == 1)
            {
                audi_bullet.pitch = Random.Range(0.95f, 1);
                audi_bullet.volume = Random.Range(0.7f, 1);
                audi_bullet.Play();
                nextfire = Time.time + firerate_1;
                if (facing_right)
                {
                    bullet = Instantiate(ammo, new Vector3(transform.position.x + 1.2f,transform.position.y - 2f, transform.position.z), transform.rotation);
                    bullet.transform.localScale = new Vector3(-ammo_scale.x, ammo_scale.y, ammo_scale.z);
                    bullet.velocity = transform.TransformDirection(new Vector2(bullet_speed, 0));
                }
                else
                {
                    bullet = Instantiate(ammo, new Vector3(transform.position.x - 1.2f, transform.position.y - 2f, transform.position.z), transform.rotation);
                    bullet.velocity = transform.TransformDirection(new Vector2(-bullet_speed, 0));
                }
            }
            else if (type == 2)
            {
                audi_bullet.pitch = Random.Range(0.95f, 1);
                audi_bullet.volume = Random.Range(0.7f, 1);
                audi_bullet.Play();
                nextfire = Time.time + firerate_1;
                bullet = Instantiate(ammo, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), transform.rotation);
                if (facing_right)
                {
                    bullet.velocity = transform.TransformDirection(new Vector2(bullet_speed, 0.3f));
                }
                else
                {
                    bullet.velocity = transform.TransformDirection(new Vector2(-bullet_speed, 0.3f));
                }
            }
        }

        else if(Input.GetButtonDown("Fire_2") && can_attack)
        {
            if (skill == 0 && can_defend)
            {
                Player_Movement_2.can_move = false;
                StartCoroutine(Defending());
            }
            else if (skill == 1 && can_defend)
            {
                Player_Movement_2.can_move = false;
                StartCoroutine(Faning());
            }
        }

	}

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("attack", false);
        Player_Movement_2.can_move = true;
    }

    IEnumerator Defending()
    {
        audi_wall.Play();
        can_defend = false;
        anim.SetBool("defend", true);
        shield_ready = false;
        if (facing_right)
        {
            plasma_wall = Instantiate(wall, new Vector3(transform.position.x + 1.5f,transform.position.y - 2f, transform.position.z), transform.rotation);
        }
        else if(!facing_right)
        {
            plasma_wall = Instantiate(wall, new Vector3(transform.position.x - 1.5f, transform.position.y - 2f, transform.position.z), transform.rotation);
        }
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("defend", false);
        Player_Movement_2.can_move = true;
        yield return new WaitForSeconds(cooldown);
        shield_ready = true;
        can_defend = true;
    }

    IEnumerator Faning()
    {
        can_defend = false;
        shield_ready = false;
        anim.SetBool("attack", true);
        for (int i = 0; i < 3; i++)
        {
            if (facing_right)
            {
                audi_bullet.pitch = Random.Range(0.95f, 1);
                audi_bullet.volume = Random.Range(0.7f, 1);
                audi_bullet.Play();
                bullet = Instantiate(ammo, new Vector3(transform.position.x + 1.2f, transform.position.y - 2f, transform.position.z), transform.rotation);
                bullet.transform.localScale = new Vector3(-ammo_scale.x, ammo_scale.y, ammo_scale.z);
                bullet.velocity = transform.TransformDirection(new Vector2(bullet_speed, 0));
            }
            else
            {
                bullet = Instantiate(ammo, new Vector3(transform.position.x - 1.2f, transform.position.y - 2f, transform.position.z), transform.rotation);
                bullet.velocity = transform.TransformDirection(new Vector2(-bullet_speed, 0));
            }
            yield return new WaitForSeconds(0.2f);
        }
        anim.SetBool("attack", false);
        Player_Movement_2.can_move = true;
        yield return new WaitForSeconds(cooldown);
        can_defend = true;
        shield_ready = true;
    }

}
