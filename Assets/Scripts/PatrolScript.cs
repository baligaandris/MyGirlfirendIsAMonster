using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    private GameObject target;
    private Rigidbody2D rb;
    public float speed = 5;
    public float walkingSpeed = 5;
    public float runningSpeed = 10;
    public enum Statemachine {Idle, Patrolling, Alert, Chasing};
    public Statemachine currentState = Statemachine.Patrolling;
    private float waitTime =0;

    // Use this for initialization
    void Start () {
        target = patrolPoint1;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState == Statemachine.Patrolling)
        {
            if (speed != walkingSpeed)
            {
                speed = walkingSpeed;
            }

            //if (GetComponent<EnemyAttackScript>().playerInRange == false)
            //{
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            //}
            
            //rb.velocity = (target.transform.position - transform.position).normalized * speed;
            LookWhereYouAreGoing();
            if (transform.position == patrolPoint1.transform.position)
            {
                target = patrolPoint2;
            }
            if (transform.position == patrolPoint2.transform.position)
            {
                target = patrolPoint1;
            }
        }
        else if (currentState == Statemachine.Idle)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) {
                TurnAround();
                waitTime= Random.Range(7f, 9f);
            }
        }
        else if (currentState == Statemachine.Alert)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                TurnAround();
                waitTime = Random.Range(0.5f, 2f);
            }
        }
        else if (currentState == Statemachine.Chasing)
        {
            //if (speed != runningSpeed)
            //{
            //    speed = runningSpeed;
            //}
            target = GameData.player;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            LookWhereYouAreGoing();
        }

	}

    private void TurnAround() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void LookWhereYouAreGoing() {
        transform.localScale = new Vector3(1 * Mathf.Sign((transform.position - target.transform.position).x), 1, 1);
    }
    
}
