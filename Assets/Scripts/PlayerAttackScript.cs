using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {

    public List<GameObject> targetsInRange;
    public bool doorInRange = false;
    public GameObject door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targetsInRange.Add(collision.gameObject);
        }
        //if (collision.gameObject.tag == "Door")
        //{
        //    doorInRange = true;
        //    door = collision.gameObject;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targetsInRange.Remove(collision.gameObject);
        }
        //if (collision.gameObject.tag == "Door")
        //{
        //    doorInRange = false;
        //    door = null;
        //}
    }

    public void HitEnemies() {
        for (int i = 0; i < targetsInRange.Count; i++) {
            targetsInRange[i].GetComponent<EnemyDieScript>().GetHitAndDie();
        }
        targetsInRange.Clear();
    }

    public void OpenDoor() {
        if (doorInRange) {
            door.GetComponent<DoorScript>().OpenClose();
        }
    }
}
