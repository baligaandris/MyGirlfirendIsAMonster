using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float walkingSpeed;
    public float jumpStrenght;
    public float climbingSpeed;

    public GameObject littleManPrefab;
    private GameObject littleManInstance;
    private GameObject groundDetector;
    private GameObject wallDetectorLeft;
    private GameObject wallDetectorRight;
    private GameObject littleMan;
    private GameObject interactableDetector;
    private GameObject attackRange;

    private Rigidbody2D rb;
    private Vector2 currentspeed;
    private bool carrying = true;




    // Use this for initialization
    void Start () {
        
        groundDetector = transform.FindChild("GroundDetector").gameObject;
        wallDetectorLeft = transform.FindChild("WallDetectorLeft").gameObject;
        wallDetectorRight = transform.FindChild("WallDetectorRight").gameObject;
        interactableDetector = transform.FindChild("InteractArea").gameObject;
        attackRange = transform.FindChild("Attack Range").gameObject;
        rb = GetComponent<Rigidbody2D>();
        littleMan = transform.FindChild("Little Man").gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        currentspeed = rb.velocity;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed, currentspeed.y);
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
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
        if (Input.GetButtonDown("Fire1")) {
            if (carrying) {

                carrying = false;
                littleMan.SetActive(false);
                littleManInstance = Instantiate(littleManPrefab, attackRange.transform.position, Quaternion.identity);
                Camera.main.GetComponent<FollowPlayer>().ChangeFollowTarget(littleManInstance);
                
            } else if (interactableDetector.GetComponent<InteractableDetection>().manInRange) {
                Camera.main.GetComponent<FollowPlayer>().ChangeFollowTarget(gameObject);
                littleMan.SetActive(true);
                carrying = true;
                Destroy(littleManInstance);
            }
        }
        if (carrying == false)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                attackRange.GetComponent<PlayerAttackScript>().HitEnemies();
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
