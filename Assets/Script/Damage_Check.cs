using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Check : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Mid_Point.hp = Mid_Point.hp - 0.1f;
        }
    }

}
