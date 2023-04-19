using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_1 : MonoBehaviour {

    private GameObject audi;

	// Use this for initialization
	void Start ()
    {

        audi = GameObject.Find("audi_stage_1");
        StartCoroutine(Next_Scene());
    
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator Next_Scene()
    {
        yield return new WaitForSeconds(4.8f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Destroy(audi);
        SceneManager.LoadScene("stage2-1");
    }
}
