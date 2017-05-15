using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour {

    public bool playerInRange=false;
    public GameObject bullet;
    public GameObject endOfGun;
    private float shootCoolDown;
    public float coolDownLenght=2f;

	// Use this for initialization
	void Start () {
        shootCoolDown = coolDownLenght;
	}
	
	// Update is called once per frame
	void Update () {
        shootCoolDown -= Time.deltaTime;
        if (playerInRange && shootCoolDown<=0) {
            Instantiate(bullet,endOfGun.transform.position,Quaternion.identity);
            shootCoolDown = coolDownLenght;
            GetComponent<PatrolScript>().SetEnemyState(PatrolScript.Statemachine.Chasing);
            GetComponent<PatrolScript>().SetChaseCooldown();
        }
	}

    public void PlayerSpotted() {
        playerInRange = true;
    }

    public void PlayerGone() {
        playerInRange = false;
    }
}
