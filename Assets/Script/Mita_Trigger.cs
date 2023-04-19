using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mita_Trigger : MonoBehaviour {

    public int events;
    public bool trigger_a;
    public bool trigger_b;
    public bool triggered;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (trigger_a || trigger_b)
        {
            triggered = true;
        }

        if (triggered)
        {
            if (!trigger_a && !trigger_b)
            {
                triggered = false;
                Mita_Config.trigger_a = trigger_a;
                Mita_Config.trigger_b = trigger_b;
                Mita_Config.events = 0;
            }
            else
            {
                Mita_Config.trigger_a = trigger_a;
                Mita_Config.trigger_b = trigger_b;
                Mita_Config.events = events;
            }
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            trigger_a = true;
        }

        if (col.gameObject.name == "Player_2")
        {
            trigger_b = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player_1")
        {
            trigger_a = false;
        }

        if (col.gameObject.name == "Player_2")
        {
            trigger_b = false;
        }
    }

}
