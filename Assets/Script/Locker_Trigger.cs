using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Locker_Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            if (Input.GetButtonDown("Trigger_1"))
            {
                StartCoroutine(Entering());
            }
        }

        if (col.gameObject.name == "Player_2")
        {
            if (Input.GetButtonDown("Trigger_2"))
            {
                StartCoroutine(Entering());
            }
        }
    }

    IEnumerator Entering()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Locker");
    }
}
