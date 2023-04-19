using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Locker_Menu : MonoBehaviour {

    public int x = 0;
    public int y = 0;
    public int select = -1;

    public GameObject[] skill_bar;
    public GameObject[] skill_line;
    public GameObject[] skill_unlocked;
    public GameObject[] skill_locked;
    public GameObject[] skill_select_kn;
    public GameObject[] skill_select_mm;

    public AudioSource audi_select;
    public AudioSource audi_no;
    public AudioSource audi_complete;

    private int i = 0;

	// Use this for initialization
	void Start () {

        StartCoroutine(Showing());

    }

    // Update is called once per frame
    void Update()
    {

        if (i == 2)
        {
            skill_line[0].SetActive(true);
            skill_unlocked[0].SetActive(true);
            skill_locked[0].SetActive(false);
        }
        if (i == 3)
        {
            skill_line[1].SetActive(true);
            skill_unlocked[1].SetActive(true);
            skill_locked[1].SetActive(false);
        }
        if (i == 5)
        {
            skill_line[2].SetActive(true);
            skill_unlocked[2].SetActive(true);
            skill_locked[2].SetActive(false);
        }
        if (i == 6)
        {
            skill_line[3].SetActive(true);
            skill_unlocked[3].SetActive(true);
            skill_locked[3].SetActive(false);
        }
        if (i == 8)
        {
            skill_line[4].SetActive(true);
            skill_unlocked[4].SetActive(true);
            skill_locked[4].SetActive(false);
        }
        if (i == 9)
        {
            skill_line[5].SetActive(true);
            skill_unlocked[5].SetActive(true);
            skill_locked[5].SetActive(false);
        }
        if (i == 12)
        {
            skill_line[6].SetActive(true);
            skill_unlocked[6].SetActive(true);
            skill_locked[6].SetActive(false);
        }
        if (i == 13)
        {
            skill_line[7].SetActive(true);
            skill_unlocked[7].SetActive(true);
            skill_locked[7].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (x > 0)
            {
                x = x - 1;
                audi_select.Play();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (x < 4)
            {
                x = x + 1;
                audi_select.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (y > 0)
            {
                y = y - 1;
                audi_select.Play();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (y < 4)
            {
                y = y + 1;
                audi_select.Play();
            }
        }

        for (int j = 0; j < skill_select_kn.Length; j++)
        {
            if (j == x)
            {
                skill_select_kn[j].SetActive(true);
            }
            else
            {
                skill_select_kn[j].SetActive(false);
            }

        }
        for (int k = 0; k < skill_select_mm.Length; k++)
        {
            if (k == y)
            {
                skill_select_mm[k].SetActive(true);
            }
            else
            {
                skill_select_mm[k].SetActive(false);
            }
        }

        if (Input.GetButtonDown("Jump_1"))
        {
            if (x == 0)
            {
                audi_complete.Play();
            }
            else
            {
                audi_no.Play();
            }
        }

        else if (Input.GetButtonDown("Jump_2"))
        {

            if (y == 0)
            {
                Player_Attack_2.skill = 0;
                audi_complete.Play();
            }

            else if (y == 1)
            {
                if (Mid_Point.bread >= 2)
                {
                    Player_Attack_2.skill = 1;
                    audi_complete.Play();
                }
                else
                {
                    audi_no.Play();
                }
            }

            else
            {
                audi_no.Play();
            }
        }

        if (Input.GetButtonDown("Punch") || Input.GetButtonDown("Fire_1"))
        {
            StartCoroutine(Backing());
        }
    }

    IEnumerator Showing()
    {
        yield return new WaitForSeconds(0.3f);
        for (i = 0; i < Mid_Point.bread; i++)
        {
            yield return new WaitForSeconds(0.1f);
            skill_bar[i].SetActive(true);
        }
    }

    IEnumerator Backing()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Base");
    }
}
