using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Config : MonoBehaviour {

    public bool bump_mm;
    public float speed = 0.5f;

    private Vector3 player_1;
    private Vector3 player_2;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {

        player_1 = GameObject.Find("Player_1").transform.position;
        player_2 = GameObject.Find("Player_2").transform.position;



        if (!bump_mm)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player_2.x, player_2.y - 2f, player_2.z), speed);
            if (transform.position.x == player_2.x)
            {
                bump_mm = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player_1.x, player_1.y - 2f, player_1.z), speed);
            if (transform.position.x == player_1.x)
            {
                bump_mm = false;
            }
        }
    }
}
