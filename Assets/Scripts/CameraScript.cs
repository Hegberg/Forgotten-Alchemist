using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        float x = GameControllerScript.control.GetPlayerPosition()[0];
        float y = GameControllerScript.control.GetPlayerPosition()[1];
        this.transform.position = new Vector3(x,y,-10);
    }
}
