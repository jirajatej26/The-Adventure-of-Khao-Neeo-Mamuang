using System.Collections;
using UnityEngine;

public class Game_Camera : MonoBehaviour {

    public Transform midpoint;
    public bool smooth = true;
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public float min_zoom = 4f;
    public float max_zoom = 14f;
    public bool can_rotation = true;
    public float distance_per_zoom = 0.25f;
    public float zoom_power = 0.25f;
    public bool locked = false;
    public Vector3 static_offset;
    public int current_event;

	public Transform point_A, point_B;
	float distance;
	float move_distance;
    float current_check;

    public static int events;
    public static bool trigger_a;
    public static bool trigger_b;

	void Start() {

        move_distance = -min_zoom;
        current_check = min_zoom + distance_per_zoom;
        static_offset = offset;

	}

	void FixedUpdate () {

        distance = Vector3.Distance(point_A.position, point_B.position);
        current_event = events;

        if (!locked)
        {

            Vector3 desiredPostion = midpoint.transform.position + offset + new Vector3(0, 0, move_distance);

            if (can_rotation)
            {
                transform.LookAt(new Vector2(transform.position.x, midpoint.position.y));
            }

            if (distance > min_zoom)
            {
                if (distance > current_check)
                {
                    move_distance = -current_check + -zoom_power;
                    current_check = current_check + distance_per_zoom;
                }
                else if (distance < current_check - distance_per_zoom)
                {
                    move_distance = -current_check + zoom_power;
                    current_check = current_check - distance_per_zoom;
                }
            }

            if (smooth)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPostion, smooth_speed);
            }
            else
            {
                transform.position = desiredPostion;
            }
        }

        // Cameara Events

        if (events == 0)
        {
            locked = false;
            offset = static_offset;
            can_rotation = true;
        }
        else
        {
            if (events == 1)
            {
                if (trigger_a || trigger_b)
                {
                    offset = new Vector3(offset.x, static_offset.y, static_offset.z - 10f);
                }
            }

            if (events == 2)
            {
                if (trigger_a || trigger_b)
                {
                    offset = new Vector3(offset.x, static_offset.y, static_offset.z - 10f);
                }
            }

            if (events == 3)
            {
                if (trigger_a && trigger_b)
                {
                    locked = true;
                    offset = new Vector3(offset.x, static_offset.y + 2f, static_offset.z - 15f);
                    Vector3 desiredPostion = midpoint.transform.position + offset;
                    transform.position = Vector3.Lerp(transform.position, desiredPostion, smooth_speed);
                }
                else
                {
                    locked = false;
                }
            }

            if (events == 4)
            {
                if (trigger_a && trigger_b)
                {
                    offset = new Vector3(offset.x, static_offset.y + 5f, static_offset.z - 28f);
                }
            }

            if (events == 5)
            {
                if (trigger_a || trigger_b)
                {
                    offset = new Vector3(offset.x, static_offset.y, static_offset.z - 20f);
                }
            }

            if (events == 6)
            {
                if (trigger_a && trigger_b)
                {
                    offset = new Vector3(offset.x, static_offset.y, static_offset.z - 15f);
                }
            }

            if (events == 7)
            {
                if (trigger_a && trigger_b)
                {
                    can_rotation = false;
                    offset = new Vector3(offset.x, static_offset.y + 1f, static_offset.z - 7f);
                }
            }

            if (events == 8)
            {
                offset = new Vector3(static_offset.x + 8f, static_offset.y, static_offset.z);
            }

            if (events == 10)
            {
                can_rotation = false;
                offset = new Vector3(offset.x, static_offset.y - 2f, static_offset.z - 5f);
            }

            if (events == 11)
            {
                can_rotation = false;
                offset = new Vector3(offset.x, static_offset.y + 8f, static_offset.z - 60f);
                distance_per_zoom = 0;
                zoom_power = 0;
            }
            if (events == 12)
            {
                can_rotation = false;
                offset = new Vector3(offset.x, static_offset.y, static_offset.z);
                distance_per_zoom = 0.25f;
                zoom_power = 0.25f;
            }
        }
	} 
}