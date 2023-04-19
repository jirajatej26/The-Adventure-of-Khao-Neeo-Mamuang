using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Manager : MonoBehaviour {

    public static float damage;
    public float hp = 100f;

    public static bool phase_1 = true;
    public static bool phase_2 = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (damage >= 0 && phase_1)
        {
            damage = damage - 0.2f;
            hp = hp - 0.2f;
        }

        if (hp <= 50f && phase_1)
        {
            damage = 0;
            phase_1 = false;
            AI_Boss_3.right_die = true;
        }

        if (damage >= 0 && phase_2)
        {
            damage = damage - 0.2f;
            hp = hp - 0.2f;
        }

        if (hp <= 0 && phase_2)
        {
            AI_Boss_3.left_die = true;
        }

    }
}
