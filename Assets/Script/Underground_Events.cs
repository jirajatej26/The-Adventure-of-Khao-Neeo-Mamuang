using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underground_Events : MonoBehaviour {

    public GameObject camera_trigger;
    public GameObject wave_1;
    public GameObject wave_2;
    public GameObject wave_3;
    public GameObject wave_4;

    // Use this for initialization
    void Start () {

        camera_trigger.SetActive(false);
        wave_1.SetActive(false);
        wave_2.SetActive(false);
        wave_3.SetActive(false);
        wave_4.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        camera_trigger.SetActive(true);
        StartCoroutine(Next_Wave());
    }

    IEnumerator Next_Wave()
    {
        wave_1.SetActive(true);
        yield return new WaitForSeconds(3f);
        wave_2.SetActive(true);
        yield return new WaitForSeconds(3f);
        wave_3.SetActive(true);
        yield return new WaitForSeconds(33f);
        wave_4.SetActive(true);
    }

}
