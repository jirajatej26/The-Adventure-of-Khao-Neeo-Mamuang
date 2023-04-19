using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Config : MonoBehaviour {

    public float destroy_time = 3f;

    // Use this for initialization
    void Start () {

        Destroy(gameObject, destroy_time);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
