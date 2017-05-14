using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieScript : MonoBehaviour {
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHitAndDie() {
        //StartCoroutine(GetHitAndDestroy());
        Destroy(gameObject);
    }

    IEnumerator GetHitAndDestroy()
    {
        rb.AddForce((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position)*1000);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
