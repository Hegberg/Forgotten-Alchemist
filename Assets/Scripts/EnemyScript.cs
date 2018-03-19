using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private float speed = 0.04f;
    private List<float> target_position = new List<float>();
    private float health = 1.0f;
    bool can_be_hit = true;
    bool reset_countdown = false;
    float hit_timer = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Rotate();

        if (reset_countdown)
        {
            ResetCountdown();
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 0, 1f);
    }

    void LoseHealth(float health_to_lose)
    {
        if (can_be_hit)
        {
            health -= health_to_lose;
            if (health <= 0)
            {
                GameControllerScript.control.SetScoreText(1.0f);
                Destroy(this.gameObject);
            }
            reset_countdown = true;
            can_be_hit = false;
        }
    }

    void ResetCountdown()
    {
        hit_timer -= 0.01f;
        if (hit_timer < 0)
        {
            can_be_hit = true;
            reset_countdown = false;
            hit_timer = 0.5f;
        }
    }

    void Move()
    {
        target_position = GameControllerScript.control.GetPlayerPosition();

        if (this.transform.position.x > target_position[0])
        {
            transform.Translate(new Vector3(-speed, 0, 0), Space.World);
        } else
        {
            transform.Translate(new Vector3(speed, 0, 0), Space.World);
        }

        if (this.transform.position.y > target_position[1])
        {
            transform.Translate(new Vector3(0, -speed, 0), Space.World);
        }
        else
        {
            transform.Translate(new Vector3(0, speed, 0), Space.World);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Fire_Explosion(Clone)")
        {
            LoseHealth(1.0f);
        }
        else if (other.name == "Lightning_Explosion(Clone)")
        {
            LoseHealth(0.50f);
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Gas_Explosion(Clone)")
        {
            LoseHealth(0.34f);
        }
    }
}
