using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour {

    public bool playerInRange=false;
    public GameObject bullet;
    public GameObject endOfGun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInRange) {
            Instantiate(bullet,endOfGun.transform.position,Quaternion.identity);
        }
        playerInRange = false;
	}

    public void PlayerSpotted() {
        playerInRange = true;
    }
}
