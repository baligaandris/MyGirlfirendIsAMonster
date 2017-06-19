using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryScript : MonoBehaviour {

    public GameObject[] EnemiesToAlert;
    public GameObject lookAt1;
    public GameObject lookAt2;
    private GameObject target;
    public float speed;
    private int rotationDirection=1;
    public float howOftenToTurn = 5;
    private float turncooldown;

    // Use this for initialization
    void Start () {
        target = lookAt1;
        turncooldown = howOftenToTurn;
	}
	
	// Update is called once per frame
	void Update () {
        turncooldown -= Time.deltaTime;
        transform.right = Vector3.RotateTowards(transform.right, target.transform.position-transform.position, speed * Time.deltaTime,rotationDirection);

        if (turncooldown<=0) {
            if (target == lookAt1) {
                turncooldown = howOftenToTurn;
                target = lookAt2;

            } else if (target == lookAt2)
            {
                turncooldown = howOftenToTurn;
                target = lookAt1;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //itt valami nem stimmel.
            RaycastHit2D rch = new RaycastHit2D();
            rch = Physics2D.Raycast(transform.position, collision.gameObject.transform.position-transform.position);
            Debug.Log(rch.collider.gameObject.name);
            if (rch.collider.gameObject.tag == "Player")
            {
                for (int i = 0; i< EnemiesToAlert.Length; i++) {
                    if (EnemiesToAlert[i] != null) {
                        EnemiesToAlert[i].GetComponent<PatrolScript>().SetEnemyState(PatrolScript.Statemachine.Chasing);
                        Debug.Log("Enemies Alerted");
                }

            }

            
            }
        }
    }
}
