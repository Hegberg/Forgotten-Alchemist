using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControllerScript : MonoBehaviour {

    public static ProjectileControllerScript control;

    public Transform projectile_parent;

    // Use this for initialization
    void Start () {
        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
