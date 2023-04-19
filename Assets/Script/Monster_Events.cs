using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Events : MonoBehaviour {

    public GameObject wave;

	// Use this for initialization
	void Start () {

        wave.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            wave.SetActive(true);
        }
        else if (col.gameObject.name == "Player_2")
        {
            wave.SetActive(true);
        }
    }
}
