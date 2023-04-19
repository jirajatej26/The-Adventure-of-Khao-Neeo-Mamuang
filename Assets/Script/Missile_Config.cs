using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Config : MonoBehaviour {

    public GameObject target;
    public float missile_speed = 3f;
    public GameObject explosion;

    private Vector3 locked_position;
    private string target_name;

	// Use this for initialization
	void Start () {

        target_name = target.name;
        locked_position = GameObject.Find(target_name).transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(locked_position.x,locked_position.y - 7f,locked_position.z), missile_speed);
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Ground")
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
