using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        if (Input.GetKey("p"))
        {
            StartCoroutine((Next_Scene()));
        }

    }

    IEnumerator Next_Scene()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
