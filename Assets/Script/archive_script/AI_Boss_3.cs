using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI_Boss_3 : MonoBehaviour {

    public GameObject player_1;
    public GameObject player_2;
    public GameObject right_hand;
    public GameObject left_hand;
    public Rigidbody2D rigid_right;
    public Rigidbody2D rigid_left;
    public GameObject laser;
    public bool left_hand_used = false;
    public bool right_hand_used = false;
    public Transform point_a;
    public Transform point_b;
    public bool bump_right = false;
    public bool can_move = false;
    public float timer = 10f;
    public bool right_check = false;
    public bool left_check = false;
    public float check_time;
    public bool died = false;

    public static bool ready = false;
    public static bool right_die;
    public static bool left_die;

    private bool move_right;
    private bool move_left;
    private float distance_a;
    private float distance_b;
    private bool ready_1 = false;
    private bool ready_2 = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown("o"))
        {
            right_die = true;
            left_die = true;
        }

        if (ready)
        {
            if (!left_hand_used && !right_hand_used && !ready_1)
            {
                ready_1 = true;
                StartCoroutine(Right_Hand_Use());
            }
            if (right_die && !ready_2)
            {
                Destroy(right_hand);
                right_hand_used = false;
                can_move = false;
                ready_2 = true;
                StartCoroutine(Left_Hand_Use());
            }
            if (left_die && !died)
            {
                died = true;
                Destroy(left_hand);
                left_hand_used = false;
                StartCoroutine(Dying());
            }
        }

        if (move_right)
        {
            if (right_hand.transform.position.z > 7f)
            {
                right_hand.transform.position = new Vector3(right_hand.transform.position.x, right_hand.transform.position.y, right_hand.transform.position.z - 0.8f);
            }
            else
            {
                move_right = false;
                StartCoroutine(Float_Right_Hand());
            }
        }

        if (move_left)
        {
            if (left_hand.transform.position.z > 7f)
            {
                left_hand.transform.position = new Vector3(left_hand.transform.position.x, left_hand.transform.position.y, left_hand.transform.position.z - 0.8f);
            }
            else
            {
                move_left = false;
                StartCoroutine(Float_Left_Hand());
            }
        }

        if (right_hand_used)
        {
            distance_a = Vector3.Distance(right_hand.transform.position, point_a.transform.position);
            distance_b = Vector3.Distance(right_hand.transform.position, point_b.transform.position);
            if (!bump_right && can_move)
            {
                rigid_right.AddForce(Vector2.right * 100);
                if (rigid_right.velocity.x > 4f)
                {
                    rigid_right.velocity = new Vector2(3f, rigid_right.velocity.y);
                }
                if (distance_b <= 5f)
                {
                    bump_right = true;
                }
            }
            else if (bump_right && can_move)
            {
                rigid_right.AddForce(Vector2.left * 100);
                if (rigid_right.velocity.x < -4f)
                {
                    rigid_right.velocity = new Vector2(-3f, rigid_right.velocity.y);
                }
                if (distance_a <= 5f)
                {
                    bump_right = false;
                }
            }
            if (right_hand_used && !right_check)
            {
                check_time = Time.time + timer;
                right_check = true;
            }

            else if (Time.time > check_time && can_move)
            {
                StartCoroutine(Drop_Right_Hand());
            }

        }

        if (left_hand_used)
        {
            distance_a = Vector3.Distance(left_hand.transform.position, point_a.transform.position);
            distance_b = Vector3.Distance(left_hand.transform.position, point_b.transform.position);
            if (!bump_right && can_move)
            {
                rigid_left.AddForce(Vector2.right * 100);
                if (rigid_left.velocity.x > 4f)
                {
                    rigid_left.velocity = new Vector2(3f, rigid_left.velocity.y);
                }
                if (distance_b <= 5f)
                {
                    bump_right = true;
                }
            }
            else if (bump_right && can_move)
            {
                rigid_left.AddForce(Vector2.left * 100);
                if (rigid_left.velocity.x < -4f)
                {
                    rigid_left.velocity = new Vector2(-3f, rigid_left.velocity.y);
                }
                if (distance_a <= 5f)
                {
                    bump_right = false;
                }
            }
            if (right_hand_used && !left_check)
            {
                check_time = Time.time + timer;
                left_check = true;
            }

            else if (Time.time > check_time && can_move)
            {
                StartCoroutine(Drop_Left_Hand());
            }
        }
    }

    IEnumerator Right_Hand_Use()
    {
        yield return new WaitForSeconds(2f);
        move_right = true;
    }

    IEnumerator Left_Hand_Use()
    {
        yield return new WaitForSeconds(2f);
        move_left = true;
    }

    IEnumerator Float_Right_Hand()
    {
        rigid_right.gravityScale = -1f;
        yield return new WaitForSeconds(1f);
        rigid_right.gravityScale = 1f;
        yield return new WaitForSeconds(3f);
        rigid_right.gravityScale = -1f;
        yield return new WaitForSeconds(3f);
        right_hand_used = true;
        can_move = true;
    }

    IEnumerator Float_Left_Hand()
    {
        rigid_left.gravityScale = -1f;
        yield return new WaitForSeconds(1f);
        rigid_left.gravityScale = 1f;
        yield return new WaitForSeconds(3f);
        rigid_left.gravityScale = -1f;
        yield return new WaitForSeconds(3f);
        left_hand_used = true;
        can_move = true;
        Boss_Manager.phase_2 = true;
    }

    IEnumerator Drop_Right_Hand()
    {
        can_move = false;
        rigid_right.gravityScale = 1f;
        rigid_right.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(5f);
        rigid_right.gravityScale = -1f;
        check_time = Time.time + timer;
        can_move = true;
    }

    IEnumerator Drop_Left_Hand()
    {
        can_move = false;
        rigid_left.gravityScale = 1f;
        rigid_left.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(5f);
        rigid_left.gravityScale = -1f;
        check_time = Time.time + timer;
        can_move = true;
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2f);
        float fadeTime = GameObject.Find("Game_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("cutscene-ending");
    }
}
