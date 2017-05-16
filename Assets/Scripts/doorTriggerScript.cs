using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTriggerScript : MonoBehaviour {
    private DoorScript doorScript;

	// Use this for initialization
	void Start () {
        doorScript = transform.parent.gameObject.GetComponent<DoorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponentInChildren<PlayerAttackScript>().door = transform.parent.gameObject;
            collision.gameObject.GetComponentInChildren<PlayerAttackScript>().doorInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlayerAttackScript>().door = null;
            collision.gameObject.GetComponentInChildren<PlayerAttackScript>().doorInRange = false;
        }
    }

}
