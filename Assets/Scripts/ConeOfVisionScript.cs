using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfVisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //detect if the player is in the vision cone, and if he is, we check if the enemy has direct line of sight. If he does, then the player is spotted
            RaycastHit2D rch = new RaycastHit2D();
            rch = Physics2D.Raycast(transform.position, collision.gameObject.transform.position - transform.position);
            Debug.Log(rch.collider.gameObject.name);
            if (rch.collider.gameObject.tag == "Player")
            {
                transform.parent.GetComponent<EnemyAttackScript>().PlayerSpotted();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            transform.parent.GetComponent<EnemyAttackScript>().PlayerGone();
        }
    }
}
