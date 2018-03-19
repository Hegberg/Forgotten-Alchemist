using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionScript : MonoBehaviour {

    float deathTime = 0.5f;

	// Use this for initialization
	void Start () {
        int z = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, z));
    }
	
	// Update is called once per frame
	void Update () {
        CheckTimeToFade();
	}

    void CheckTimeToFade()
    {
        deathTime -= 0.01f;
        if (deathTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
