using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Events : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Player_Movement_1.can_move = false;
        Player_Movement_2.can_move = false;
        StartCoroutine(Cut_Scene());
    }

    IEnumerator Cut_Scene()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("CS_2-1toB1");
    }
}
