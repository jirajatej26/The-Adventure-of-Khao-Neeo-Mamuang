using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Hand : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Punch_Check")
        {
           Boss_Manager.damage = Boss_Manager.damage + (Player_Attack_1.punch_damage / 2f);
            StartCoroutine(Hurting());
        }

        if (col.gameObject.name == "Sword_Check")
        {
            Boss_Manager.damage = Boss_Manager.damage + (Player_Attack_1.sword_damage / 2f);
            StartCoroutine(Hurting());
        }

        if (col.gameObject.tag == "Bullet")
        {
            Boss_Manager.damage = Boss_Manager.damage + (Player_Attack_2.bullet_damage / 2f);
            StartCoroutine(Hurting());
        }
    }

    IEnumerator Hurting()
    {
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("hurt", false);
    }

}
