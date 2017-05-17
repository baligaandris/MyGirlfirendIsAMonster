using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryScript : MonoBehaviour {

    public GameObject[] EnemiesToAlert;
    private Transform startingTransform;
    private Quaternion q;
    public GameObject lookAt1;
    public GameObject lookAt2;
    public float speed;

    // Use this for initialization
    void Start () {
        startingTransform = transform;
        q = Quaternion.LookRotation(lookAt1.transform.position-transform.position);
	}
	
	// Update is called once per frame
	void Update () {

        transform.right = Vector3.RotateTowards(transform.rotation.ToEuler(), lookAt1.transform.position-transform.position, speed * Time.deltaTime,1);
        //Vector3 newdir = Vector3.RotateTowards(transform.rotation.eulerAngles, new Vector3 (startingTransform.rotation.x, startingTransform.rotation.y, startingTransform.rotation.z+60),1,-1);
        //transform.rotation = Quaternion.Euler(newdir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            for (int i = 0; i> EnemiesToAlert.Length; i++) {
                EnemiesToAlert[i].GetComponent<PatrolScript>().SetEnemyState(PatrolScript.Statemachine.Chasing);
            }
        }
    }
}
