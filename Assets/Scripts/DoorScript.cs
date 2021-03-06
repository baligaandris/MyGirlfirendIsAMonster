﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    private Vector3 openPosition;
    private Vector3 closedPosition;
    private Vector3 target;
    private bool open;
    public bool vertical = true;

	// Use this for initialization
	void Start () {
        closedPosition = transform.position;
        target = transform.position;
        if (vertical)
        {
            openPosition = new Vector3(transform.position.x, transform.position.y + 1.2f);
        }
        else {
            openPosition = new Vector3(transform.position.x + 1.2f, transform.position.y);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);
	}
    public void OpenClose() {
        if (open)
        {
            target = closedPosition;
            open = false;
        }
        else
        {
            target = openPosition;
            open = true;
        }
    }
}
