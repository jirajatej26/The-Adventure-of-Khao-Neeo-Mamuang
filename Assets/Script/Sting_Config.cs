using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting_Config : MonoBehaviour {

    public float destroy_time = 2f;

    private BoxCollider2D mycol;
    private Animator anim;
    private float countdown;
    private Rigidbody2D myRigid;

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
            Destroy(gameObject,0.8f);
        }
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 0.8f);
        }
        if (col.gameObject.tag == "Ground")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 0.8f);
        }
        if (col.gameObject.tag == "Player")
        {
            anim.SetBool("die", true);
            mycol.enabled = false;
            myRigid.velocity = new Vector2(0, 0);
            Destroy(gameObject, 0.8f);
        }
    }

}
