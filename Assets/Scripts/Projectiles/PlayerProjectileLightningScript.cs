using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLightningScript : MonoBehaviour {

    Vector3 movement = new Vector3(0.1f, 0.1f, 0);

    float spread_range = 3.0f;

    public AudioClip hit_object_sound;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        Ignore();

        audioSource = GameControllerScript.control.GetAudioSource();

        //faces projectile at mouse pointer
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
        transform.Rotate(0, 0, -90);
    }
	
	// Update is called once per frame
	void Update () {
        //Ignore();
        //transform.position -= Vector3.Scale(transform.right, movement);
    }

    void FixedUpdate()
    {
        Ignore();
        transform.position -= Vector3.Scale(transform.right, movement);
    }

    void Ignore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player(Clone)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileLightning(Clone)").GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileFire(Clone)").GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileGas(Clone)").GetComponent<Collider2D>());
    }

    //need isTrigger set to true for projectile in collider options for this to work
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Red_Drop(Clone)" && other.name != "Blue_Drop(Clone)" && other.name != "Green_Drop(Clone)" && other.name != "Player(Clone)")
        {
            audioSource.PlayOneShot(hit_object_sound, 1F);
            List<Transform> all_enemies = EnemyParentScript.control.GetAllEnemies();
            for (int i = 0; i < all_enemies.Count; ++i)
            {
                float distance = Mathf.Abs(Mathf.Sqrt(Mathf.Pow((this.transform.position.x - all_enemies[i].transform.position.x), 2) + Mathf.Pow((this.transform.position.y - all_enemies[i].transform.position.y), 2)));
                if (distance < spread_range)
                {
                    Transform another_explosion = (Transform)Instantiate(PrefabManagerscript.ProjectileExplosions[1], new Vector3(all_enemies[i].transform.position.x, all_enemies[i].transform.position.y, 0), Quaternion.identity);
                    int z = Random.Range(0, 360);
                    another_explosion.rotation = new Quaternion(another_explosion.rotation.x, another_explosion.rotation.y, z, another_explosion.rotation.w);

                    if (all_enemies[i] != other.transform)
                    {
                        Transform connector_explosion = (Transform)Instantiate(PrefabManagerscript.LightningExplosionConnector, new Vector3((all_enemies[i].transform.position.x + other.transform.position.x) / 2, (all_enemies[i].transform.position.y + other.transform.position.y) / 2, 0), Quaternion.identity);
                        Vector3 targetDir = all_enemies[i].position - connector_explosion.transform.position;
                        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        connector_explosion.rotation = Quaternion.RotateTowards(connector_explosion.rotation, q, 360f);
                        connector_explosion.Rotate(new Vector3(0, 0, 90));
                        if (distance > 1) {
                            connector_explosion.localScale += new Vector3(0, (1f - distance) * 1.4f, 0);
                        } else
                        {
                            connector_explosion.localScale -= new Vector3(0, 1 - distance, 0);
                        }
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
