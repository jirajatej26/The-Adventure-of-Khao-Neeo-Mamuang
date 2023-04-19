using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Config : MonoBehaviour {

    public float score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            Mid_Point.score = Mid_Point.score + score;
            Destroy(gameObject);
        }
        else if (col.gameObject.name == "Player_2")
        {
            Mid_Point.score = Mid_Point.score + score;
            Destroy(gameObject);
        }
    }

}
