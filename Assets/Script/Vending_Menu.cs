using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vending_Menu : MonoBehaviour {

    public int x = 0;
    public int y = 0;
    public int selected = 0;

    public GameObject[] button;
    public GameObject[] led;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (x < 5)
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
            if (y > 0)
            {
                y = y - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (y < 2)
            {
                y = y + 1;
            }
        }

        selected = x + (y * 6);
        for (int i = 0; i < button.Length; i++)
        {
            if (i == selected)
            {
                led[selected].SetActive(true);
            }
            else
            {
                button[i].SetActive(true);
                led[i].SetActive(false);
            }
        }

        if (Input.GetButtonDown("Jump_1") || Input.GetButtonDown("Jump_2"))
        {
            led[selected].SetActive(false);
            if (selected == 6 || selected == 7 || selected == 8 || selected == 9 || selected == 10 || selected == 11 && Mid_Point.score >= 90)
            {
                if (Mid_Point.score >= 90)
                {
                    Mid_Point.score = Mid_Point.score - 90;
                    Mid_Point.potion[0] = Mid_Point.potion[0] + 1;
                }
            }
            else if (selected == 0 || selected == 5 || selected == 12 || selected == 17 && Mid_Point.score >= 120)
            {
                if (Mid_Point.score >= 120)
                {
                    Mid_Point.score = Mid_Point.score - 120;
                    Mid_Point.potion[1] = Mid_Point.potion[1] + 1;
                }
            }
            else if (selected == 1 || selected == 2 || selected == 3 || selected == 4 || selected == 13 || selected == 14 || selected == 15 || selected == 16)
            {
                if (Mid_Point.score >= 150)
                {
                    Mid_Point.score = Mid_Point.score - 150;
                    Mid_Point.potion[2] = Mid_Point.potion[2] + 1;
                }
            }
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
}
