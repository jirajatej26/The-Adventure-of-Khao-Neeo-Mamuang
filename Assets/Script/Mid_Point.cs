using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mid_Point : MonoBehaviour {

	public Transform pointA, pointB;
    public Image health_bar;
    public Image background_bar;
    public float starting_hp = 100f;
    public Image sword_ui;
    public Image shield_ui;
    public bool sword_ready = true;
    public bool shield_ready = true;
    public float sword_timer;
    public float shield_timer;
    public bool check_1;
    public bool check_2;
    public float score_show;
    public bool drinked = false;
    public Text point;
    public Text level;

    public static float hp = 100f;
    public static float damage = 0f;
    public static float score = 0;
    public static int[] potion = new int[3];
    public static int bread = 0;
    public static int selected = 0;

    private float speed;

    void Start()
    {

        hp = starting_hp;
        damage = 0;

    }

    void FixedUpdate ()
	{

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.K))
        {
            if (selected > 0)
            {
                selected = selected - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.L))
        {
            if (selected < 2)
            {
                selected = selected + 1;
            }
        }

        else if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Semicolon))
        {
            if (potion[selected] > 0 && !drinked)
            {
                StartCoroutine(Drinking());
            }
        }

        score_show = score;

        point.text = " " + score;
        level.text = bread + " ";

        sword_ready = Player_Attack_1.sword_ready;
        shield_ready = Player_Attack_2.shield_ready;

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(Dying());
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            bread = bread + 1;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            score = score + 100;
        }

        if (!sword_ready)
        {
            if (!check_1)
            {
                sword_timer = 0;
                check_1 = true;
                InvokeRepeating("sword_cooldown", 0, 0.05f);
            }
        }
        else
        {
            sword_timer = 6f;
            check_1 = false;
            CancelInvoke("sword_cooldown");
        }

        if (!shield_ready)
        {
            if (!check_2)
            {
                shield_timer = 0;
                check_2 = true;
                InvokeRepeating("shield_cooldown", 0, 0.05f);
            }
        }
        else
        {
            shield_timer = 6f;
            check_2 = false;
            CancelInvoke("shield_cooldown");
        }

        shield_ui.fillAmount = (shield_timer * 20) / 100f;
        sword_ui.fillAmount = (sword_timer * 20) / 100f;

        if (damage > 0)
        {
            hp = hp - 0.5f;
            damage = damage - 0.5f;
        }

        if (hp == 0.1f && starting_hp == 0.1f)
        {
            background_bar.fillAmount = 0;
            health_bar.fillAmount = 0;
        }
        else
        {
            background_bar.fillAmount = 1f;
        }

        if (hp >= 100)
        {
            hp = 100;
        }

        if (hp <=0)
        {
            StartCoroutine(Dying());
        }

        speed = 25 * Time.deltaTime;
        transform.position = Vector3.Lerp(new Vector3(pointA.position.x,pointA.position.y - 2f,pointA.position.z), new Vector3(pointB.position.x,pointB.position.y - 2f,pointB.position.z), speed);
        health_bar.fillAmount = hp / 100f;

	} 

    void sword_cooldown()
    {
        sword_timer = sword_timer + 0.05f;
    }

    void shield_cooldown()
    {
        shield_timer = shield_timer + 0.05f;
    }

    IEnumerator Dying()
    {
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Drinking()
    {
        drinked = true;
        Debug.Log("Triggered");
        Player_Movement_1.can_move = false;
        Player_Movement_2.can_move = false;
        Player_Attack_1.can_attack = false;
        Player_Attack_2.can_attack = false;
        Player_Attack_1.can_punch = false;
        if (selected == 0)
        {
            hp = hp + 25;
            potion[selected] = potion[selected] - 1;
        }
        else if (selected == 1)
        {
            potion[selected] = potion[selected] - 1;
        }
        else if (selected == 2)
        {
            potion[selected] = potion[selected] - 1;
        }
        yield return new WaitForSeconds(0.8f);
        Player_Movement_1.can_move = true;
        Player_Movement_2.can_move = true;
        Player_Attack_1.can_attack = true;
        Player_Attack_2.can_attack = true;
        Player_Attack_1.can_punch = true;
        drinked = false;
    }
}
