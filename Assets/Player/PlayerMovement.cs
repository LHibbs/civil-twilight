using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	private float moveSpeed = 2f;

    private GameObject ship;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
        ship = GameObject.Find("Ship");
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector3 (ship.GetComponent<Rigidbody2D>().velocity.x + -moveSpeed * Input.GetAxis("Horizontal"), ship.GetComponent<Rigidbody2D>().velocity.y + moveSpeed * Input.GetAxis("Vertical"), 0f);
	}
}
