﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Play was clicked on -> ");
            SceneManager.LoadScene("Test Scene");
            
        }
    }
}
