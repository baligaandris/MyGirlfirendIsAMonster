using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float walkingSpeed;
    public float jumpStrenght;
    public float climbingSpeed;
    private GameObject groundDetector;
    private GameObject wallDetectorLeft;
    private GameObject wallDetectorRight;
    private GameObject littleMan;

    private Rigidbody2D rb;
    private Vector2 currentspeed;
    private bool carrying = true;

	// Use this for initialization
	void Start () {
        groundDetector = transform.FindChild("GroundDetector").gameObject;
        wallDetectorLeft = transform.FindChild("WallDetectorLeft").gameObject;
        wallDetectorRight = transform.FindChild("WallDetectorRight").gameObject;
        rb = GetComponent<Rigidbody2D>();
        littleMan = transform.FindChild("Little Man").gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        currentspeed = rb.velocity;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed, currentspeed.y);
        if (IsGrounded()) {
            //transform.Translate(Vector2.right * walkingSpeed*Input.GetAxis("Horizontal")*Time.deltaTime);
            
            if (Input.GetButton("Jump"))
            {
                currentspeed = rb.velocity;
                rb.velocity = new Vector2(currentspeed.x,jumpStrenght);
            }
        }
        if (IsTouchingWall())
        {
            rb.gravityScale = 0.5f;
            if (Input.GetAxis("Vertical")!=0) {
                currentspeed = rb.velocity;
                rb.velocity = new Vector2(currentspeed.x, Input.GetAxis("Vertical") * climbingSpeed);
            }
            //currentspeed = rb.velocity;
            //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed, currentspeed.y);
        }
        else {
            rb.gravityScale = 1f;
        }
        if (Input.GetButton("Fire1")) {
            if (carrying) {
                carrying = false;
                littleMan.GetComponent<Rigidbody2D>().simulated = true;
                littleMan.transform.parent = null;
            }
        }

	}
    bool IsGrounded(){
        return Physics2D.Linecast(transform.position, groundDetector.transform.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    bool IsTouchingWall() {
        return (Physics2D.Linecast(transform.position, wallDetectorLeft.transform.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, wallDetectorRight.transform.position, 1 << LayerMask.NameToLayer("Ground")));
    }



}
