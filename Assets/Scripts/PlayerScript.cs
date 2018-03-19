using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Sprite fire;
    public Sprite gas;
    public Sprite lightning;

    private List<Sprite> skins = new List<Sprite>();

    private Vector3 mousePosition;
    private Vector3 direction;
    private float speed = 0.08f;
    private int current_projectile = 0;
    private int number_of_projectiles = 3;
    private List<int> ammo_count = new List<int> { 0, 0, 0 };
    private float life = 1.0f;
    private float damage = 0.25f;
    private bool can_be_hit = true;
    private bool reset_countdown = false;
    private float hit_timer = 0.5f;

    public AudioClip hit_sound;
    public AudioClip throw_sound;
    public AudioClip pickup_sound;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {

        audioSource = GameControllerScript.control.GetAudioSource();

        skins.Add(fire);
        skins.Add(lightning);
        skins.Add(gas);
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

    void LoseHealth(float health_to_lose)
    {
        if (can_be_hit)
        {
            can_be_hit = false;
            life -= health_to_lose;
            GameControllerScript.control.SetHealthText(life);
            audioSource.PlayOneShot(hit_sound, 1F);
            if (life <= 0)
            {
                GameControllerScript.control.RestartGame();
                Destroy(this.gameObject);
            }
            reset_countdown = true;
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

    private void Rotate()
    {
        mousePosition = GameControllerScript.control.GetMaincamera().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - GameControllerScript.control.GetMaincamera().transform.position.z));
        //Debug.Log(mousePosition);
        //Rotates toward the mouse
        GetComponent<Rigidbody2D>().transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
    }

    private void Move()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo_count[current_projectile] > 0)
            {
                Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
                Transform projectile = (Transform)Instantiate(PrefabManagerscript.PlayerProjectiles[current_projectile], new Vector3(rigid.transform.position.x, rigid.transform.position.y, 0), Quaternion.identity);
                projectile.SetParent(ProjectileControllerScript.control.projectile_parent.transform);
                ammo_count[current_projectile] -= 1;
                audioSource.PlayOneShot(throw_sound, 1F);
                if (current_projectile == 0)
                {
                    GameControllerScript.control.SetFireCountText(ammo_count[current_projectile]);
                } else if (current_projectile == 1)
                {
                    GameControllerScript.control.SetLightningCountText(ammo_count[current_projectile]);
                } else
                {
                    GameControllerScript.control.SetGasCountText(ammo_count[current_projectile]);
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            current_projectile = (current_projectile + 1);
            if (current_projectile > number_of_projectiles - 1)
            {
                current_projectile = 0;
            }
            GetComponent<SpriteRenderer>().sprite = skins[current_projectile];
            //Debug.Log(current_projectile);
        } else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            current_projectile = (current_projectile - 1);
            if (current_projectile < 0)
            {
                current_projectile = 2;
            }
            GetComponent<SpriteRenderer>().sprite = skins[current_projectile];

        }

        if (Input.GetKey("w"))
        {
            MoveUp();
        }
        else if (Input.GetKey("s"))
        {
            MoveDown();
        }
        if (Input.GetKey("a"))
        {
            MoveLeft();
        }
        else if (Input.GetKey("d"))
        {
            MoveRight();
        }

        GameControllerScript.control.SetPlayerPosition(this.transform.position.x, this.transform.position.y);
    }

    private void MoveLeft()
    {
        transform.Translate(new Vector3(-speed, 0, 0), Space.World);
    }

    private void MoveRight()
    {
        transform.Translate(new Vector3(speed, 0, 0), Space.World);
    }

    private void MoveUp()
    {
        transform.Translate(new Vector3(0, speed, 0), Space.World);
    }

    private void MoveDown()
    {
        transform.Translate(new Vector3(0, -speed, 0), Space.World);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Red_Drop(Clone)")
        {
            ammo_count[0] += 1;
            Destroy(other.gameObject);
            GameControllerScript.control.SetFireCountText(ammo_count[0]);
            audioSource.PlayOneShot(pickup_sound, 1F);
        }
        else if (other.gameObject.name == "Blue_Drop(Clone)")
        {
            ammo_count[1] += 1;
            Destroy(other.gameObject);
            GameControllerScript.control.SetLightningCountText(ammo_count[1]);
            audioSource.PlayOneShot(pickup_sound, 1F);
        }
        else if (other.gameObject.name == "Green_Drop(Clone)")
        {
            ammo_count[2] += 1;
            Destroy(other.gameObject);
            GameControllerScript.control.SetGasCountText(ammo_count[2]);
            audioSource.PlayOneShot(pickup_sound, 1F);
        }

        if (other.gameObject.name == "Enemy(Clone)")
        {
            LoseHealth(damage);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Enemy(Clone)")
        {
            LoseHealth(damage);
        }
    }
}
