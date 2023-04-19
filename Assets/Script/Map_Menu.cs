using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_Menu : MonoBehaviour {

    public int x = 2;
    public Animator map_1;
    public Animator map_2;
    public Animator map_3;
    public Animator map_4;
    public Animator map_5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (x == 0)
        {
            map_1.SetBool("select", true);
        }
        else
        {
            map_1.SetBool("select", false);
        }

        if (x == 1)
        {
            map_2.SetBool("select", true);
        }
        else
        {
            map_2.SetBool("select", false);
        }

        if (x == 2)
        {
            map_3.SetBool("select", true);
        }
        else
        {
            map_3.SetBool("select", false);
        }

        if (x == 3)
        {
            map_4.SetBool("select", true);
        }
        else
        {
            map_4.SetBool("select", false);
        }

        if (x == 4)
        {
            map_5.SetBool("select", true);
        }
        else
        {
            map_5.SetBool("select", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (x < 4)
            {
                x = x + 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (x > 0)
            {
                x = x - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (x > 0)
            {
                if (x == 4)
                {
                    x = x - 2;
                }
                else
                {
                    x = x - 1;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (x < 4)
            {
                if (x == 0)
                {
                    x = x + 2;
                }
                else
                {
                    x = x + 1;
                }
            }
        }

        if (Input.GetButtonDown("Jump_1") || Input.GetButtonDown("Jump_2"))
        {
            StartCoroutine(Entering());
        }

        if (Input.GetButtonDown("Punch") || Input.GetButtonDown("Fire_1"))
        {
            StartCoroutine(Backing());
        }

    }

    IEnumerator Backing()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Base");
    }

    IEnumerator Entering()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        if (x == 0)
        {
            SceneManager.LoadScene("stage2-1");
        }
        else if (x == 2)
        {
            SceneManager.LoadScene("Base");
        }
    }

}
