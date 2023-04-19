using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dieshit_Config : MonoBehaviour {

    public float speed = 1f;

    private Vector3 boss_3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        boss_3 = GameObject.Find("Boss_Manager").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, boss_3, speed);

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hand")
        {
            Destroy(gameObject);
        }
    }
}
