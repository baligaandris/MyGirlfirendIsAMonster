using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Vector3 targetVector;
    public float speed = 5;

	// Use this for initialization
	void Start () {
        targetVector = (GameData.player.transform.position- transform.position).normalized;
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(targetVector * speed*Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameData.player) {
            //Destroy(GameData.player);
        }


        Destroy(gameObject);
    }
}
