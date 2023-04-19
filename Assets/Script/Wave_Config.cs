using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Config : MonoBehaviour {

    public float destroy_time = 3.4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Destroy(gameObject, destroy_time);

    }
}
