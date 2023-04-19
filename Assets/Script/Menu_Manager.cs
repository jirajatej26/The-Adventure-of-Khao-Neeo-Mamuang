using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

    public GameObject text_1;
    public GameObject text_2;

	// Use this for initialization
	void Start () {

        text_2.SetActive(false);

	}

    // Update is called once per frame
    void Update() {

        if (Input.anyKey)
        {
            StartCoroutine(Game_Start());
        }
		
	}

    IEnumerator Game_Start()
    {
        text_1.SetActive(false);
        text_2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        text_2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        text_2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        text_2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        text_2.SetActive(true);
        yield return new WaitForSeconds(1f);
        text_2.SetActive(false);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("1");
    }
}
