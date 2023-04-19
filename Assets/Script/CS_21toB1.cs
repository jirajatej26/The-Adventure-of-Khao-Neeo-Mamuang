using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_21toB1 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        StartCoroutine(Entering());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Entering()
    {
        yield return new WaitForSeconds(9f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("boss-1");
    }
}
