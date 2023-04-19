using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss_3_Manager : MonoBehaviour {

    public bool boss_ready = false;
    public GameObject hand_1;
    public GameObject hand_2;
    public bool hand_1_sent = false;
    public bool hand_2_sent = false;
    public GameObject point_a;
    public GameObject point_b;
    public float hand_speed = 50;
    public float max_speed = 4;
    public bool can_move = false;
    public float delay_per_punch = 7f;
    public float delay_per_float = 3f;
    public bool aim_KN = true;
    public float old_standard = 0;
    public float random_value = 0;
    public float standard_value = 0.5f;
    public int aim_count = 0;
    public float right_hp = 50;
    public float left_hp = 50;
    public float body_hp = 100;
    public Image right_foreground;
    public Image right_background;
    public Image left_foreground;
    public Image left_background;
    public Image boss_foreground;
    public Image boss_background;
    public GameObject spawn_1;
    public GameObject spawn_2;
    public float spawn_timer = 10;
    public GameObject monster_1;
    public int min_mon = 5;
    public int max_mon = 7;
    public float spawn_delay = 0.2f;
    public int boss_ran = 0;
    public Rigidbody2D laser;
    public GameObject laser_col;
    public bool bump_right = false;
    public GameObject missile_a;
    public GameObject missile_b;
    public int min_missile = 5;
    public int max_missile = 8;
    public int miss_ran = 0;
    public float missile_delay = 0.2f;
    public GameObject camera_1;
    public GameObject camera_2;

    public static float hp_hand_1 = 0;
    public static float hp_hand_2 = 0;
    public static float hp_boss = 0;
    public static int hand_status = 0;
    public static float delay;

    private Rigidbody2D hand_1_rigid;
    private Rigidbody2D hand_2_rigid;
    private bool hand_1_intro = false;
    private bool hand_2_intro = false;
    private float countdown = 10000;
    private bool hand_1_died = false;
    private bool hand_2_died = false;
    private int mon_ran_1 = 0;
    private int mon_ran_2 = 0;
    private bool door_active = false;
    private bool spawned = false;
    private bool mon_died = false;
    private bool missile_spawn = false;
    private bool missile_died = false;
    private Animator anim_left;
    private Animator anim_right;
    private AudioSource audi_hand;
    private AudioSource audi_handwave;
    private AudioSource audi_slotidle;
    private AudioSource audi_slotchar;

    // Use this for initialization
    void Start() {

        hand_1_rigid = hand_1.GetComponent<Rigidbody2D>();
        hand_2_rigid = hand_2.GetComponent<Rigidbody2D>();
        anim_right = hand_1.GetComponent<Animator>();
        anim_left = hand_2.GetComponent<Animator>();
        audi_hand = GameObject.Find("audi_hand").GetComponent<AudioSource>();
        audi_handwave = GameObject.Find("audi_handwave").GetComponent<AudioSource>();
        audi_slotidle = GameObject.Find("audi_slotidle").GetComponent<AudioSource>();
        audi_slotchar = GameObject.Find("audi_slotchar").GetComponent<AudioSource>();


        audi_slotidle.Play();
        StartCoroutine(Boss_Intro());

        // For Set HP Only.
        hp_hand_1 = right_hp;
        hp_hand_2 = left_hp;
        hp_boss = body_hp;

        delay = delay_per_float;
        laser_col.SetActive(false);

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            if (hp_hand_1 > 0)
            {
                hp_hand_1 = 0;
            }
            else if (hp_hand_2 > 0)
            {
                hp_hand_2 = 0;
            }
            else if (hp_boss > 0)
            {
                hp_boss = 0;
            }
        }

        right_foreground.fillAmount = (hp_hand_1 * 2) / 100f;
        left_foreground.fillAmount = (hp_hand_2 * 2) / 100f;
        boss_foreground.fillAmount = hp_boss / 100f;

        // For Check Value Only.
        right_hp = hp_hand_1;
        left_hp = hp_hand_2;
        body_hp = hp_boss;

        if (hand_1_rigid.velocity.x > max_speed)
        {
            hand_1_rigid.velocity = new Vector2(max_speed, hand_1_rigid.velocity.y);
        }
        else if (hand_1_rigid.velocity.x < -max_speed)
        {
            hand_1_rigid.velocity = new Vector2(-max_speed, hand_1_rigid.velocity.y);
        }

        if (hand_2_rigid.velocity.x > max_speed)
        {
            hand_2_rigid.velocity = new Vector2(max_speed, hand_2_rigid.velocity.y);
        }
        else if (hand_2_rigid.velocity.x < -max_speed)
        {
            hand_2_rigid.velocity = new Vector2(-max_speed, hand_2_rigid.velocity.y);
        }

        if (boss_ready)
        {
            if (hp_hand_1 > 0)
            {
                if (hand_1.transform.position.z > 6)
                {
                    hand_1.transform.position = new Vector3(hand_1.transform.position.x, hand_1.transform.position.y, hand_1.transform.position.z - 0.8f);
                }
                else
                {
                    if (!hand_1_intro)
                    {
                        StartCoroutine(Hand_Intro());
                    }
                    else
                    {

                        if (Time.time >= countdown && can_move)
                        {
                            can_move = false;
                            hand_1_rigid.velocity = new Vector2(0, 0);
                            StartCoroutine(Hand_Drop());
                            countdown = 10000;
                        }
                        else if (aim_KN && can_move)
                        {
                            if (hand_1.transform.position.x > point_a.transform.position.x)
                            {
                                hand_1_rigid.AddForce(Vector2.left * hand_speed);
                            }
                            else
                            {
                                hand_1_rigid.AddForce(Vector2.right * hand_speed);
                            }
                        }
                        else if (!aim_KN && can_move)
                        {
                            if (hand_1.transform.position.x > point_b.transform.position.x)
                            {
                                hand_1_rigid.AddForce(Vector2.left * hand_speed);
                            }
                            else
                            {
                                hand_1_rigid.AddForce(Vector2.right * hand_speed);
                            }
                        }
                    }
                }
            }
            else if (hp_hand_2 > 0)
            {
                if (!hand_1_died)
                {
                    StartCoroutine(Dying());
                }
                else
                {
                    if (hand_2.transform.position.z > 6)
                    {
                        hand_2.transform.position = new Vector3(hand_2.transform.position.x, hand_2.transform.position.y, hand_2.transform.position.z - 0.8f);
                    }
                    else
                    {
                        if (!hand_2_intro)
                        {
                            StartCoroutine(Hand_Intro());
                        }
                        else
                        {
                            if (Time.time >= countdown && can_move)
                            {
                                can_move = false;
                                hand_2_rigid.velocity = new Vector2(0, 0);
                                StartCoroutine(Hand_Drop());
                                countdown = 10000;
                            }
                            else if (aim_KN && can_move)
                            {
                                if (hand_2.transform.position.x > point_a.transform.position.x)
                                {
                                    hand_2_rigid.AddForce(Vector2.left * hand_speed);
                                }
                                else
                                {
                                    hand_2_rigid.AddForce(Vector2.right * hand_speed);
                                }
                            }
                            else if (!aim_KN && can_move)
                            {
                                if (hand_2.transform.position.x > point_b.transform.position.x)
                                {
                                    hand_2_rigid.AddForce(Vector2.left * hand_speed);
                                }
                                else
                                {
                                    hand_2_rigid.AddForce(Vector2.right * hand_speed);
                                }
                            }
                        }
                    }
                }
            }
            else if (hp_boss > 0)
            {
                if (!hand_2_died)
                {
                    StartCoroutine(Dying());
                }

                else
                {
                    if (!door_active)
                    {
                        StartCoroutine(Spawning());
                    }
                    else
                    {
                        if (spawned && !mon_died)
                        {
                            if (GameObject.Find("Dieshit(Clone)") == null)
                            {
                                mon_died = true;
                            }
                        }
                        else if (spawned && mon_died)
                        {
                            StartCoroutine(Boss_Attacking());
                        }
                        else if (missile_spawn && !missile_died)
                        {
                            if (GameObject.Find("missile_a(Clone)") == null && GameObject.Find("missile_b(Clone)") == null)
                            {
                                missile_died = true;
                            }
                        }
                        else if (missile_spawn && missile_died)
                        {
                            missile_died = true;
                            missile_spawn = false;
                            door_active = false;
                        }
                    }
                } 
            }
            else if (hp_boss <=0)
            {
                StartCoroutine(Dying());
            }
        }
            
    }

    void Random_Number()
    {
        random_value = Random.value;
        old_standard = standard_value;
        if (random_value < standard_value)
        {
            if (aim_KN)
            {
                standard_value = standard_value + Mathf.Pow(-2, -(aim_count + 1));
                aim_count = aim_count + 1;
                if (hp_hand_1 > 0)
                {
                    anim_right.SetBool("randomed", true);
                    anim_right.SetBool("aim_kn", true);
                    anim_right.SetBool("punch", false);
                }
                else if (hp_hand_2 > 0)
                {
                    anim_left.SetBool("randomed", true);
                    anim_left.SetBool("aim_kn", true);
                    anim_left.SetBool("punch", false);
                }
            }
            else
            {
                standard_value = 0.5f;
                aim_KN = true;
                aim_count = 1;
                if (hp_hand_1 > 0)
                {
                    anim_right.SetBool("randomed", true);
                    anim_right.SetBool("aim_kn", true);
                    anim_right.SetBool("punch", false);
                }
                else if (hp_hand_2 > 0)
                {
                    anim_left.SetBool("randomed", true);
                    anim_left.SetBool("aim_kn", true);
                    anim_left.SetBool("punch", false);
                }
            }
        }
        else
        {
            if (!aim_KN)
            {
                standard_value = standard_value + Mathf.Pow(2, -(aim_count + 1));
                aim_count = aim_count + 1;
                if (hp_hand_1 > 0)
                {
                    anim_right.SetBool("randomed", true);
                    anim_right.SetBool("aim_kn", false);
                    anim_right.SetBool("punch", false);
                }
                else if (hp_hand_2 > 0)
                {
                    anim_left.SetBool("randomed", true);
                    anim_left.SetBool("aim_kn", false);
                    anim_left.SetBool("punch", false);
                }
            }
            else
            {
                standard_value = 0.5f;
                aim_KN = false;
                aim_count = 1;
                if (hp_hand_1 > 0)
                {
                    anim_right.SetBool("randomed", true);
                    anim_right.SetBool("aim_kn", false);
                    anim_right.SetBool("punch", false);
                }
                else if (hp_hand_2 > 0)
                {
                    anim_left.SetBool("randomed", true);
                    anim_left.SetBool("aim_kn", false);
                    anim_left.SetBool("punch", false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dieshit")
        {
            hp_boss = hp_boss - 0.8f;
        }
    }

    IEnumerator Boss_Intro()
    {
        yield return new WaitForSeconds(3f);
        boss_ready = true;
    }

    IEnumerator Hand_Intro()
    {
        if (!hand_1_intro)
        {
            hand_1_intro = true;
            yield return new WaitForSeconds(0.3f);
            hand_status = 1;
            hand_1_rigid.gravityScale = -1;
            yield return new WaitForSeconds(0.7f);
            hand_status = 2;
            hand_1_rigid.gravityScale = 1;
            yield return new WaitForSeconds(2.2f);
            audi_hand.Play();
            audi_slotidle.Stop();
            audi_slotchar.Play();
            yield return new WaitForSeconds(1.8f);
            Random_Number();
            aim_count = 1;
            standard_value = 0.5f;
            yield return new WaitForSeconds(3f);
            hand_status = 1;
            hand_1_rigid.gravityScale = -1;
            yield return new WaitForSeconds(1.2f);
            anim_right.SetBool("randomed", false);
            anim_right.SetBool("punch", false);
            countdown = Time.time + delay_per_punch;
            can_move = true;
        }
        else if (!hand_2_intro)
        {
            can_move = false;
            hand_2_intro = true;
            yield return new WaitForSeconds(0.3f);
            hand_status = 6;
            hand_2_rigid.gravityScale = -1;
            yield return new WaitForSeconds(0.7f);
            hand_2_rigid.velocity = new Vector2(0, hand_2_rigid.velocity.y);
            hand_status = 7;
            hand_2_rigid.gravityScale = 1;
            yield return new WaitForSeconds(2.2f);
            audi_handwave.Play();
            audi_slotidle.Stop();
            audi_slotchar.Play();
            yield return new WaitForSeconds(1.8f);
            Random_Number();
            aim_count = 1;
            standard_value = 0.5f;
            yield return new WaitForSeconds(3f);
            hand_status = 6;
            hand_2_rigid.gravityScale = -1;
            yield return new WaitForSeconds(1.2f);
            anim_left.SetBool("randomed", false);
            anim_left.SetBool("punch", false);
            countdown = Time.time + delay_per_punch;
            can_move = true;
        }
    }

    IEnumerator Hand_Drop()
    {
        if (!hand_2_intro)
        {
            hand_1_rigid.velocity = new Vector2(0, hand_1_rigid.velocity.y);
            hand_status = 2;
            hand_1_rigid.gravityScale = 1;
            yield return new WaitForSeconds(1.5f);
            audi_hand.Play();
            audi_slotchar.Play();
            yield return new WaitForSeconds(1.5f);
            Random_Number();
            yield return new WaitForSeconds(delay_per_float);
            anim_right.SetBool("randomed", false);
            hand_status = 1;
            hand_1_rigid.gravityScale = -1;
            yield return new WaitForSeconds(1.2f);
            countdown = Time.time + delay_per_punch;
            can_move = true;
        }
        else if (hand_2_intro)
        {
            hand_2_rigid.velocity = new Vector2(0, hand_2_rigid.velocity.y);
            hand_status = 7;
            hand_2_rigid.gravityScale = 1;
            yield return new WaitForSeconds(1.5f);
            audi_handwave.Play();
            audi_slotchar.Play();
            yield return new WaitForSeconds(1.5f);
            Random_Number();
            yield return new WaitForSeconds(delay_per_float);
            anim_left.SetBool("randomed", false);
            hand_status = 6;
            hand_2_rigid.gravityScale = -1;
            yield return new WaitForSeconds(1.2f);
            countdown = Time.time + delay_per_punch;
            can_move = true;
        }
    }

    IEnumerator Dying()
    {
        if (hp_hand_2 > 0)
        {
            can_move = false;
            hand_1_rigid.velocity = new Vector2(0, hand_1_rigid.velocity.y);
            countdown = 10000;
            yield return new WaitForSeconds(3f);
            hand_1.SetActive(false);
            hand_1_died = true;
        }
        else if (hp_boss > 0)
        {
            can_move = false;
            hand_2_rigid.velocity = new Vector2(0, hand_2_rigid.velocity.y);
            countdown = 10000;
            yield return new WaitForSeconds(3f);
            hand_2.SetActive(false);
            hand_2_died = true;
        }
        else if (hp_boss <= 0)
        {
            yield return new WaitForSeconds(1f);
            float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
            camera_2.SetActive(true);
            camera_1.SetActive(false);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene("CS_B3end");
        }
    }

    IEnumerator Spawning()
    {
        door_active = true;
        yield return new WaitForSeconds(2f);
        spawned = true;
        mon_died = false;
        mon_ran_1 = Random.Range(min_mon, max_mon + 1);
        mon_ran_2 = Random.Range(min_mon, max_mon + 1);
        for (int i = 1; i <= max_mon; i++)
        {
            Instantiate(monster_1, spawn_1.transform.position, monster_1.transform.rotation);
            Instantiate(monster_1, spawn_2.transform.position, monster_1.transform.rotation);
            yield return new WaitForSeconds(spawn_delay);
        }
    }

    IEnumerator Boss_Attacking()
    {
        spawned = false;
        yield return new WaitForSeconds(2f);
        boss_ran = Random.Range(1, 3);
        if (boss_ran == 1)
        {
            if (!bump_right)
            {
                laser.velocity = new Vector2(0, -7);
                yield return new WaitForSeconds(1.5f);
                laser.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);
                laser_col.SetActive(true);
                laser.velocity = new Vector2(7, 0);
                yield return new WaitForSeconds(5f);
                laser.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);
                laser_col.SetActive(false);
                laser.velocity = new Vector2(0, 7);
                yield return new WaitForSeconds(1.5f);
                laser.velocity = new Vector2(7, 0);
                yield return new WaitForSeconds(1f);
                laser.velocity = new Vector2(0, 0);
                bump_right = true;
                door_active = false;
            }
            else
            {
                laser.velocity = new Vector2(0, -7);
                yield return new WaitForSeconds(1.5f);
                laser.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);
                laser_col.SetActive(true);
                laser.velocity = new Vector2(-7, 0);
                yield return new WaitForSeconds(5f);
                laser.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);
                laser_col.SetActive(false);
                laser.velocity = new Vector2(0, 7);
                yield return new WaitForSeconds(1.5f);
                laser.velocity = new Vector2(-7, 0);
                yield return new WaitForSeconds(1f);
                laser.velocity = new Vector2(0, 0);
                bump_right = false;
                door_active = false;
            }
        }
        else if (boss_ran == 2)
        {
            missile_spawn = true;
            missile_died = false;
            miss_ran = Random.Range(min_missile,max_missile + 1);
            aim_count = 1;
            standard_value = 0.5f;
            Random_Number();
            if (aim_KN)
            {
                for (int i = 1; i <= max_missile; i++)
                {
                    Instantiate(missile_a, transform.transform.position, missile_a.transform.rotation);
                    yield return new WaitForSeconds(missile_delay);
                }
            }
            else
            {
                for (int i = 1; i <= max_missile; i++)
                {
                    Instantiate(missile_b, transform.transform.position, missile_b.transform.rotation);
                    yield return new WaitForSeconds(missile_delay);
                }
            }
        }
    }

}
