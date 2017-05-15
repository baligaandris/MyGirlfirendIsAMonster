using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    private Vector3 openPosition;
    private Vector3 closedPosition;
    private Vector3 target;
    private bool open;

	// Use this for initialization
	void Start () {
        closedPosition = transform.position;
        target = transform.position;
        openPosition = new Vector3(transform.position.x, transform.position.y + 1);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, 1);
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
