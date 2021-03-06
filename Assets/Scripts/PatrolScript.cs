﻿using System.Collections;
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
    private float stateCooldown;
    public float chaseCooldown = 10;
    public float AlertCooldown = 10;

    private GameObject visionCone;

    // Use this for initialization
    void Start () {
        target = patrolPoint1;
        rb = GetComponent<Rigidbody2D>();
        visionCone = transform.FindChild("ConeOfVision").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState == Statemachine.Patrolling)
        {
            if (speed != walkingSpeed)
            {
                speed = walkingSpeed;
            }
            GetComponent<Animator>().SetBool("Walking", true);

            gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y), speed * Time.deltaTime);

            
            if (transform.position == new Vector3(patrolPoint1.transform.position.x,transform.position.y,transform.position.z))
            {
                target = patrolPoint2;
                LookWhereYouAreGoing();
            }
            if (transform.position == new Vector3(patrolPoint2.transform.position.x, transform.position.y, transform.position.z))
            {
                target = patrolPoint1;
                LookWhereYouAreGoing();
            }

        }
        else if (currentState == Statemachine.Idle)
        {
            GetComponent<Animator>().SetBool("Walking", false);
            visionCone.transform.right = transform.right;
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) {
                TurnAround();
                waitTime= Random.Range(7f, 9f);
            }
        }
        else if (currentState == Statemachine.Alert)
        {
            GetComponent<Animator>().SetBool("Walking", false);
            visionCone.transform.right = transform.right;
            stateCooldown -= Time.deltaTime;
            if (stateCooldown <= 0) {
                SetEnemyState(Statemachine.Idle);
            }
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                TurnAround();
                waitTime = Random.Range(0.5f, 2f);
            }
        }
        else if (currentState == Statemachine.Chasing)
        {
            GetComponent<Animator>().SetBool("Walking", true);
            stateCooldown -= Time.deltaTime;
            target = GameData.player;
            if (stateCooldown <= 0)
            {
                SetEnemyState(Statemachine.Alert);

                stateCooldown = AlertCooldown;
            }
            gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y), speed * Time.deltaTime);
            LookWhereYouAreGoing();
            LookAtPlayer();
        }

	}

    private void TurnAround() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void LookWhereYouAreGoing() {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign((transform.position.x - target.transform.position.x)), transform.localScale.y, transform.localScale.z);
    }

    public void SetEnemyState(Statemachine newState) {
        currentState = newState;
       
    }

    public void SetChaseCooldown()
    {
        stateCooldown = chaseCooldown;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(),gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void LookAtPlayer() {
        visionCone.transform.right = (target.transform.position - transform.position)*-transform.localScale.x;
    }
}
