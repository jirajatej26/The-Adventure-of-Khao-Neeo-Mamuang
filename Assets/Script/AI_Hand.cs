using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Hand : MonoBehaviour {

    public int hand_type;
    public int status;
    public GameObject smash_check;
    public GameObject thrust_1;
    public GameObject drop_particle;
    public GameObject wave;
    public Rigidbody2D wave_col;
    public float wave_speed = 4f;

    private Rigidbody2D rigid_left;
    private Rigidbody2D rigid_right;

    private float damage;
    private Animator anim;
    private float delay;

    // Use this for initialization
    void Start () {

        anim = gameObject.GetComponent<Animator>();
        smash_check.SetActive(true);
        delay = Boss_3_Manager.delay;

    }
	
	// Update is called once per frame
	void Update () {

        if (hand_type == 1 && damage > 0)
        {
            damage = damage - 0.25f;
            Boss_3_Manager.hp_hand_1 = Boss_3_Manager.hp_hand_1 - 0.25f;
        }

        else if (hand_type == 2 && damage > 0)
        {
            damage = damage - 0.25f;
            Boss_3_Manager.hp_hand_2 = Boss_3_Manager.hp_hand_2 - 0.25f;
        }

        status = Boss_3_Manager.hand_status;

        if (hand_type == 1)
        {
            if (status == 0)
            {
                thrust_1.SetActive(false);
            }
            else if (status == 1)
            {
                thrust_1.SetActive(false);
            }
            else if (status == 2)
            {
                thrust_1.SetActive(true);
            }
        }

        else if (hand_type == 2)
        {
            if (status == 5)
            {
                thrust_1.SetActive(false);
            }
            else if (status == 6)
            {
                thrust_1.SetActive(false);
            }
            else if (status == 7)
            {
                thrust_1.SetActive(true);
            }
        }

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hand_Block")
        {
            StartCoroutine(Drop_Effect());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Punch_Check")
        {
            damage = damage + Player_Attack_1.punch_damage;
        }

        if (col.gameObject.name == "Sword_Check")
        {
            damage = damage + Player_Attack_1.sword_damage;
        }

        if (col.gameObject.tag == "Bullet")
        {
            damage = damage + Player_Attack_2.bullet_damage;
        }
    }


    IEnumerator Drop_Effect()
    {
        drop_particle.SetActive(true);
        anim.SetBool("punch", true);
        yield return new WaitForSeconds(0.3f);
        smash_check.SetActive(false);
        if (hand_type == 2)
        {
            Instantiate(wave, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), wave.transform.rotation);
            rigid_left = Instantiate(wave_col, new Vector3(transform.position.x + 2.5f, transform.position.y - 3f, transform.position.z), wave_col.transform.rotation);
            rigid_left.AddForce(Vector2.left * wave_speed);
            rigid_right = Instantiate(wave_col, new Vector3(transform.position.x - 2.5f, transform.position.y - 3f, transform.position.z), wave_col.transform.rotation);
            rigid_right.AddForce(Vector2.right * wave_speed);
        }
        yield return new WaitForSeconds(1f);
        drop_particle.SetActive(false);
        yield return new WaitForSeconds(delay);
        smash_check.SetActive(true);
    }
}
