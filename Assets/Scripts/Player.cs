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
    private GameObject interactableDetector;
    private GameObject attackRange;

    private Rigidbody2D rb;
    private Vector2 currentspeed;
    private bool carrying = true;

    public Sprite spriteWithoutGuy;
    public Sprite spriteWithGuy;


    void Start () {
        //save parts of the Player prefab that will be useful later
        groundDetector = transform.FindChild("GroundDetector").gameObject;
        wallDetectorLeft = transform.FindChild("WallDetectorLeft").gameObject;
        wallDetectorRight = transform.FindChild("WallDetectorRight").gameObject;
        interactableDetector = transform.FindChild("InteractArea").gameObject;
        attackRange = transform.FindChild("Attack Range").gameObject;
        rb = GetComponent<Rigidbody2D>();


    }
	
	void Update () {
        WalkHorizontally();
        Jump();
        HandleWallTouching();
        PutDownOrPickupMan();
        if (carrying == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                attackRange.GetComponent<PlayerAttackScript>().HitEnemies();
                attackRange.GetComponent<PlayerAttackScript>().OpenDoor();
            }
        }
        else {
            if (Input.GetButtonDown("Fire1"))
            {
                attackRange.GetComponent<PlayerAttackScript>().OpenDoor();
            }
        }


    }

    bool IsGrounded(){
        return Physics2D.Linecast(transform.position, groundDetector.transform.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    bool IsTouchingWall() {
        return (Physics2D.Linecast(transform.position, wallDetectorLeft.transform.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, wallDetectorRight.transform.position, 1 << LayerMask.NameToLayer("Ground")));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Little Man") {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    private void WalkHorizontally() {
        currentspeed = rb.velocity;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed, currentspeed.y);
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump(){
        if (IsGrounded())
        {
            if (Input.GetButton("Jump"))
            {
                currentspeed = rb.velocity;
                rb.velocity = new Vector2(currentspeed.x, jumpStrenght);
            }
        }
    }
    private void HandleWallTouching() {
        if (IsTouchingWall())
        {
            rb.gravityScale = 0.5f;
            if (Input.GetAxis("Vertical") != 0)
            {
                currentspeed = rb.velocity;
                rb.velocity = new Vector2(currentspeed.x, Input.GetAxis("Vertical") * climbingSpeed);
            }
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void PutDownOrPickupMan() {
        if (Input.GetButtonDown("Fire2"))
        {
            if (carrying)
            {
                carrying = false;

                littleManInstance = Instantiate(littleManPrefab, attackRange.transform.position, Quaternion.identity);
                Camera.main.GetComponent<FollowPlayer>().ChangeFollowTarget(littleManInstance);
                GetComponent<SpriteRenderer>().sprite = spriteWithoutGuy;
            }
            else if (interactableDetector.GetComponent<InteractableDetection>().manInRange)
            {
                Camera.main.GetComponent<FollowPlayer>().ChangeFollowTarget(gameObject);
                GetComponent<SpriteRenderer>().sprite = spriteWithGuy;
                carrying = true;
                Destroy(littleManInstance);
            }
        }
    }

}
