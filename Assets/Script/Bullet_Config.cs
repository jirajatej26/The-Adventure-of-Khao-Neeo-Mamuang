using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Config : MonoBehaviour {

    public float destroy_time = 2f;

    private Rigidbody2D myRigid;
    private Animator anim;
    private Collider2D mycol;
    private float countdown;

	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        mycol = gameObject.GetComponent<BoxCollider2D>();

        countdown = Time.time;

    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= countdown + destroy_time)
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dog")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Wall")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Bee")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Walker")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Spike")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Boss")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1);
        }
        if (col.gameObject.tag == "Empty")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Hand")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
        if (col.gameObject.tag == "Door")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2f);

    }

}
