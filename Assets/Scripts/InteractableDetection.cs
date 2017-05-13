﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetection : MonoBehaviour {

    public bool manInRange;
    public bool enemyInRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Little Man")
        {
            manInRange = true;
        }

        if (other.gameObject.tag == "Enemy")
        {
            enemyInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Little Man")
        {
            manInRange = false;
        }
    }

}
