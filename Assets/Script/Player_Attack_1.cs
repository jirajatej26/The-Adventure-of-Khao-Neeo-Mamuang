using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_1 : MonoBehaviour {

    public Animator anim;
    public bool attacked = false;
    public int combo = 0;
    public float delay = 0.2f;
    private float timing;
    public bool punching;
    public Collider2D sword;
    public Collider2D punch;
    public static float punch_damage = 2f;
    public static float sword_damage = 10f;
    public float cooldown = 5f;

    public static bool can_attack = true;
    public static bool sword_ready = true;
    public static bool can_punch = true;

    private AudioSource audi_sword;
    private AudioSource audi_punch;

    // Use this for initialization
    void Start () {

        sword.enabled = false;
        punch.enabled = false;

        audi_sword = GameObject.Find("audi_money").GetComponent<AudioSource>();
        audi_punch = GameObject.Find("audi_map").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.H))
        {
            can_attack = true;
            attacked = false;
        }

        if (Input.GetButton("Swing") && !attacked && can_attack)
        {
            anim.SetBool("drink", false);
            StartCoroutine(Attacking());
        }

        if (Input.GetButtonDown("Punch") && can_punch)
        {
            anim.SetBool("drink", false);
            audi_punch.Play();
            if (combo == 0 && !punching)
            {
                StartCoroutine(Punch_1());
                timing = Time.time + 1;
            }
            else if (combo == 1 && Time.time < timing && !punching)
            {
                StartCoroutine(Punch_2());
                timing = Time.time + 1;
            }

            else if (combo == 2 && Time.time < timing && !punching)
            {
                StartCoroutine(Punch_3());
                timing = Time.time + 1;
            }

            else if (combo == 3 && Time.time < timing && !punching)
            {
                StartCoroutine(Punch_4());
                timing = Time.time + 1;
            }

            else if (combo == 4 && Time.time < timing && !punching)
            {
                StartCoroutine(Punch_5());
                timing = Time.time + 1;
            }
        }

        if (Time.time > timing)
            {
                combo = 0;
                anim.SetBool("punch", false);
                anim.SetInteger("combo", 0);
            }

    }

    IEnumerator Attacking()
    {
        audi_sword.Play();
        Player_Movement_1.can_move = false;
        Player_Movement_1.immue = true;
        attacked = true;
        anim.SetBool("attack",true);
        sword.enabled = true;
        can_attack = false;
        sword_ready = false;
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("attack", false);
        sword.enabled = false;
        attacked = false;
        Player_Movement_1.can_move = true;
        Player_Movement_1.immue = false;
        yield return new WaitForSeconds(cooldown);
        can_attack = true;
        sword_ready = true;
    }

    IEnumerator Punch_1()
    {
        anim.SetBool("punch",true);
        punch.enabled = true;
        punching = true;
        Player_Movement_1.immue = true;
        yield return new WaitForSeconds(delay);
        punching = false;
        anim.SetBool("punch", false);
        anim.SetInteger("combo", 1);
        punch.enabled = false;
        Player_Movement_1.immue = false;
        combo = 1;
    }

    IEnumerator Punch_2()
    {
        anim.SetBool("punch", true);
        punch.enabled = true;
        punching = true;
        Player_Movement_1.immue = true;
        yield return new WaitForSeconds(delay);
        punching = false;
        anim.SetBool("punch", false);
        anim.SetInteger("combo", 2);
        punch.enabled = false;
        Player_Movement_1.immue = false;
        combo = 2;
    }

    IEnumerator Punch_3()
    {
        anim.SetBool("punch", true);
        punch.enabled = true;
        punching = true;
        Player_Movement_1.immue = true;
        yield return new WaitForSeconds(delay);
        punching = false;
        anim.SetBool("punch", false);
        anim.SetInteger("combo", 3);
        punch.enabled = false;
        Player_Movement_1.immue = false;
        combo = 3;
    }

    IEnumerator Punch_4()
    {
        anim.SetBool("punch", true);
        punch.enabled = true;
        punching = true;
        Player_Movement_1.immue = true;
        yield return new WaitForSeconds(delay);
        punching = false;
        anim.SetBool("punch", false);
        anim.SetInteger("combo", 4);
        punch.enabled = false;
        Player_Movement_1.immue = false;
        combo = 4;
    }

    IEnumerator Punch_5()
    {
        anim.SetBool("punch", true);
        punch.enabled = true;
        punching = true;
        Player_Movement_1.immue = true;
        yield return new WaitForSeconds(delay);
        punching = false;
        anim.SetBool("punch", false);
        anim.SetInteger("combo", 0);
        punch.enabled = false;
        Player_Movement_1.immue = false;
        combo = 0;
    }

}
