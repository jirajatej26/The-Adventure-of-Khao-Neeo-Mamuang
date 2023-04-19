using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vending_Machine : MonoBehaviour {

    public bool test_mode = false;
    public GameObject audi_restore;
    public GameObject mita_trigger;

    private Animator anim_1;
    private Animator anim_2;

    // Use this for initialization
    void Start () {

        anim_1 = GameObject.Find("Player_1").GetComponent<Animator>();
        anim_2 = GameObject.Find("Player_2").GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            if (Input.GetButton("Trigger_1"))
            {
                anim_1.SetBool("drink", true);
                Mid_Point.hp = Mid_Point.hp + 0.1f;
            }
        }

        if (col.gameObject.name == "Player_2")
        {
            if (Input.GetButton("Trigger_2"))
            {
                anim_2.SetBool("drink", true);
                Mid_Point.hp = Mid_Point.hp + 0.1f;
            }
        }
        if (Mid_Point.hp == 100 & !test_mode)
        {
            Player_Movement_1.can_move = false;
            Player_Movement_2.can_move = false;
            StartCoroutine(Starting());
        }
    }

    IEnumerator Starting()
    {
        audi_restore.SetActive(true);
        mita_trigger.SetActive(false);
        Mita_Config.events = 0;
        yield return new WaitForSeconds(2f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Player_Movement_1.can_move = true;
        Player_Movement_2.can_move = true;
        SceneManager.LoadScene("CS_1to2-1");
    }
}
