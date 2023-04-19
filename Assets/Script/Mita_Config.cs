using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mita_Config : MonoBehaviour {

    public float stop_distance = 3;
    public float move_power = 50;
    public float move_speed = 1;
    public float max_speed = 4;

    public GameObject follow;
    public GameObject text_box;
    public GameObject A;
    public GameObject Y;
    public GameObject B;
    public GameObject X;
    public GameObject drink_1;
    public GameObject drink_2;
    public GameObject drink_3;
    public GameObject audi_hello;
    public GameObject audi_follow;

    private Rigidbody2D myRigid;
    private Vector3 player_1;
    private Vector3 player_2;
    private float distance_a;
    private float distance_b;
    private Vector3 mid_point;
    private Vector3 current_position;

    public static int events;
    public static bool trigger_a;
    public static bool trigger_b;

    // Use this for initialization
    void Start () {

        myRigid = gameObject.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

        player_1 = GameObject.Find("Player_1").transform.position;
        player_2 = GameObject.Find("Player_2").transform.position;
        mid_point = GameObject.Find("Mid_Player").transform.position;

        distance_a = Vector3.Distance(transform.position, player_1);
        distance_b = Vector3.Distance(transform.position, player_2);

        current_position = transform.position;

        if (myRigid.velocity.x > max_speed)
        {
            myRigid.velocity = new Vector2(max_speed, myRigid.velocity.y);
        }
        else if (myRigid.velocity.x < -max_speed)
        {
            myRigid.velocity = new Vector2(-max_speed, myRigid.velocity.y);
        }

        if (distance_a <= distance_b)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player_1.y - 1f, transform.position.z), 0.0625f);
        }
        else if (distance_b < distance_a)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player_2.y - 1f, transform.position.z), 0.0625f);
        }

        if (distance_a < stop_distance || distance_b < stop_distance)
        {
            if (myRigid.velocity.x > -0.0078125 && myRigid.velocity.x < 0.0078125)
            {
                myRigid.velocity = new Vector2(0, myRigid.velocity.y);
            }
            else if (myRigid.velocity.x > 0)
            {
                myRigid.velocity = new Vector2(myRigid.velocity.x / 2, myRigid.velocity.y);
            }
            else if (myRigid.velocity.x < 0)
            {
                myRigid.velocity = new Vector2(myRigid.velocity.x / 2, myRigid.velocity.y);
            }
        }

        else if (distance_a <= distance_b)
        {

            transform.position = Vector3.MoveTowards(transform.position, player_1, move_speed);

            if (transform.position.x < current_position.x)
            {
                myRigid.AddForce(Vector2.left * move_power);
            }
            else if (transform.position.x > current_position.x)
            {
                myRigid.AddForce(Vector2.right * move_power);
            }

        }

        else if (distance_b < distance_a)
        {

            transform.position = Vector3.MoveTowards(transform.position, player_2, move_speed);

            if (transform.position.x < current_position.x)
            {
                myRigid.AddForce(Vector2.left * move_power);
            }
            else if (transform.position.x > current_position.x)
            {
                myRigid.AddForce(Vector2.right * move_power);
            }

        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T))
        {
            if (Mid_Point.potion[Mid_Point.selected] > 0)
            {
                if (Mid_Point.selected == 0)
                {
                    StartCoroutine(Drinking_1());
                }
                else if (Mid_Point.selected == 1)
                {
                    StartCoroutine(Drinking_2());
                }
                else if (Mid_Point.selected == 2)
                {
                    StartCoroutine(Drinking_3());
                }
            }
        }

        Debug.Log(events);

        if (events == 0)
        {
            follow.SetActive(false);
            text_box.SetActive(false);
            A.SetActive(false);
            Y.SetActive(false);
            B.SetActive(false);
            X.SetActive(false);
            drink_1.SetActive(false);
            drink_2.SetActive(false);
            drink_3.SetActive(false);
            audi_follow.SetActive(false);
        }
        else
        {

            if (events == -3)
            {
                if (Mid_Point.potion[Mid_Point.selected] > 0)
                {
                    text_box.SetActive(true);
                    drink_3.SetActive(true);
                }
            }

            else if (events == -2)
            {
                if (Mid_Point.potion[Mid_Point.selected] > 0)
                {
                    text_box.SetActive(true);
                    drink_2.SetActive(true);
                }
            }

            else if (events == -1)
            {
                if (Mid_Point.potion[Mid_Point.selected] > 0)
                {
                    text_box.SetActive(true);
                    drink_1.SetActive(true);
                }
            }

            else if (events == 1)
            {
                if (trigger_a || trigger_b)
                {
                    follow.SetActive(true);
                    audi_hello.SetActive(true);
                }
            }

            else if (events == 2)
            {
                if (trigger_a || trigger_b)
                {
                    text_box.SetActive(true);
                    A.SetActive(true);
                    audi_follow.SetActive(true);
                }
            }

            else if (events == 3)
            {
                if (trigger_a || trigger_b)
                {
                    text_box.SetActive(true);
                    X.SetActive(true);
                    audi_follow.SetActive(true);
                }
            }

        }

    }

    IEnumerator Drinking_1()
    {
        events = -1;
        yield return new WaitForSeconds(2f);
        events = 0;
    }

    IEnumerator Drinking_2()
    {
        events = -2;
        yield return new WaitForSeconds(2f);
        events = 0;
    }

    IEnumerator Drinking_3()
    {
        events = -3;
        yield return new WaitForSeconds(2f);
        events = 0;
    }

}