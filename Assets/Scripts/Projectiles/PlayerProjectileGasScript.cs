using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGasScript : MonoBehaviour {

    Vector3 movement = new Vector3(0.1f, 0.1f, 0);

    public AudioClip hit_object_sound;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        Ignore();
        audioSource = GameControllerScript.control.GetAudioSource();

        //faces projectile at mouse pointer
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
        transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        Ignore();
        transform.position -= Vector3.Scale(transform.right, movement);
    }

    void Ignore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player(Clone)").GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileLightning(Clone)").GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileFire(Clone)").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("PlayerProjectileGas(Clone)").GetComponent<Collider2D>());
    }

    //need isTrigger set to true for projectile in collider options for this to work
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Red_Drop(Clone)" && other.name != "Blue_Drop(Clone)" && other.name != "Green_Drop(Clone)" && other.name != "Player(Clone)")
        {
            audioSource.PlayOneShot(hit_object_sound, 1F);
            Transform explosion = (Transform)Instantiate(PrefabManagerscript.ProjectileExplosions[2], new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
