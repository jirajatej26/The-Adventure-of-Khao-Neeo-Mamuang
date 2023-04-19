using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Lever : MonoBehaviour {

    public Rigidbody2D door;
    public float speed = 10f;
    public bool triggered = false;
    public bool go_up;
    public GameObject non_active;
    public GameObject activated;

    // Use this for initialization
    void Start()
    {
        activated.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (triggered && go_up)
        {
            door.AddForce(Vector2.up * speed);
        }

        else if (triggered && !go_up)
        {
            door.AddForce(Vector2.down * speed);
            if (door.velocity.y < -0.1f)
            {
                door.velocity = new Vector2(0, -0.1f);
            }
        }

    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            triggered = true;
            non_active.SetActive(false);
            activated.SetActive(true);
        }

        if (col.gameObject.name == "Player_2")
        {
            triggered = true;
            non_active.SetActive(false);
            activated.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (go_up)
        {
            triggered = false;
            non_active.SetActive(true);
            activated.SetActive(false);
        }
        else
        {
            non_active.SetActive(true);
            activated.SetActive(false);
        }
    }

}
