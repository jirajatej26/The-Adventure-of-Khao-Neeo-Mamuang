using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Underground_Vending : MonoBehaviour {

    public bool triggered;
    public Rigidbody2D door;
    private Animator anim_1;
    private Animator anim_2;

    // Use this for initialization
    void Start () {

        anim_1 = GameObject.Find("Player_1").GetComponent<Animator>();
        anim_2 = GameObject.Find("Player_2").GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        if (triggered)
        {
            if (Mid_Point.hp == 100)
            {
                door.gravityScale = -1f;
            }
        }
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            if (Input.GetButton("Trigger_1"))
            {
                triggered = true;
                Mid_Point.hp = Mid_Point.hp + 0.2f;
                anim_1.SetBool("drink", true);
            }
        }

        if (col.gameObject.name == "Player_2")
        {
            if (Input.GetButton("Trigger_2"))
            {
                triggered = true;
                Mid_Point.hp = Mid_Point.hp + 0.2f;
                anim_2.SetBool("drink", true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            anim_1.SetBool("drink", false);
        }
        if (col.gameObject.name == "Player_1")
        {
            anim_2.SetBool("drink", false);
        }
    }

    IEnumerator Next_Stage()
    {
        yield return new WaitForSeconds(1f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("boss-01");
    }

}
