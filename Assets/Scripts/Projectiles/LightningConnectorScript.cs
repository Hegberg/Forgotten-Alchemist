﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningConnectorScript : MonoBehaviour {

    float deathTime = 0.3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CheckTimeToFade();
	}

    void FixedUpdate()
    {
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
