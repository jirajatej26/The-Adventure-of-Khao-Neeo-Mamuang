using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public Rigidbody2D ladder;
    public float speed = 30f;
    public bool triggered = false;
    public GameObject red;
    public GameObject green;
    public GameObject audi_nice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (triggered)
        {
            ladder.AddForce(Vector2.down * speed);
            red.SetActive(false);
            green.SetActive(true);
        }
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            if (Input.GetButton("Trigger_1") && !triggered)
            {
                triggered = true;
                audi_nice.SetActive(true);
            }
        }
    }

}
