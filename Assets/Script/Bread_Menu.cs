using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bread_Menu : MonoBehaviour {

    public float x = 0;
    public Animator bread_1;
    public Animator bread_2;
    public Animator bread_3;
    public GameObject eat_1;
    public GameObject eat_2;
    public GameObject eat_3;
    public GameObject point_1;
    public GameObject point_2;
    public GameObject point_3;

    private AudioSource audi;

    // Use this for initialization
    void Start() {

        audi = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {

        if (x == 0)
        {
            bread_1.SetBool("select", true);
        }
        else
        {
            bread_1.SetBool("select", false);
        }

        if (x == 1)
        {
            bread_2.SetBool("select", true);
        }
        else
        {
            bread_2.SetBool("select", false);
        }

        if (x == 2)
        {
            bread_3.SetBool("select", true);
        }
        else
        {
            bread_3.SetBool("select", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (x < 2)
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

        if (Input.GetButtonDown("Jump_1") || Input.GetButtonDown("Jump_2"))
        {
            if (x == 0 && Mid_Point.score >= 600)
            {
                StartCoroutine(Eating_1());
            }
            else if (x == 1 && Mid_Point.score >= 700)
            {
                StartCoroutine(Eating_2());
            }
            else if (x == 2 && Mid_Point.score >= 900)
            {
                StartCoroutine(Eating_3());
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

    IEnumerator Eating_1()
    {
        audi.Play();
        Mid_Point.score = Mid_Point.score - 600;
        eat_1.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        eat_1.SetActive(false);
        point_1.SetActive(false);
        point_1.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        point_1.SetActive(false);
        Mid_Point.bread = Mid_Point.bread + 1;
    }

    IEnumerator Eating_2()
    {
        audi.Play();
        Mid_Point.score = Mid_Point.score - 700;
        eat_2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        eat_2.SetActive(false);
        point_2.SetActive(false);
        point_2.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        point_2.SetActive(false);
        Mid_Point.bread = Mid_Point.bread + 2;
    }

    IEnumerator Eating_3()
    {
        audi.Play();
        Mid_Point.score = Mid_Point.score - 900;
        eat_3.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        eat_3.SetActive(false);
        point_3.SetActive(false);
        point_3.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        point_3.SetActive(false);
        Mid_Point.bread = Mid_Point.bread + 3;
    }

}
