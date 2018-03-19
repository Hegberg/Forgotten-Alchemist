using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasExplosion : MonoBehaviour {

    float deathTime = 1.2f;
    Vector3 movement = new Vector3(0.1f, 0.1f, 0);
    Vector4 temp_colour = new Vector4();
    float growth = 1.01f;

    // Use this for initialization
    void Start()
    {
        int z = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, z));
        temp_colour = gameObject.GetComponent<Renderer>().material.color;
        temp_colour[3] = 0.5f;
        gameObject.GetComponent<Renderer>().material.color = temp_colour;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeToFade();
        //transform.position -= Vector3.Scale(transform.right, movement);
        transform.localScale = new Vector3(transform.localScale[0] * growth, transform.localScale[1] * growth, transform.localScale[2]);
    }

    void CheckTimeToFade()
    {
        deathTime -= 0.01f;
        if (deathTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(this.gameObject);
    }
}
