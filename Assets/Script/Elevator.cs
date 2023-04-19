using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public Rigidbody2D platform;
    public float power = 20f;
    public float max_speed = 2f;
    public bool triggered = false;
    public GameObject elevator_block;
    public GameObject button_red;
    public GameObject button_green;

    // Use this for initialization
    void Start () {

        elevator_block.SetActive(false);
        button_green.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        if (triggered)
        {
            elevator_block.SetActive(true);
            button_green.SetActive(true);
            button_red.SetActive(false);
            platform.AddForce(Vector2.up * power);
        }

        if (platform.velocity.y > max_speed)
        {
            platform.velocity = new Vector2(0, max_speed);
            Player_Attack_2.type = 2;
        }
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            if (Input.GetButtonDown("Trigger_1"))
            {
                triggered = true;
            }
        }

        else if (col.gameObject.name == "Player_2")
        {
            if (Input.GetButtonDown("Trigger_2"))
            {
                triggered = true;
            }
        }
    }

}
