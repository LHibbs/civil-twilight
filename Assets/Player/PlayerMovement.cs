using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	private float moveSpeed = 2f; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); 
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector3 (-moveSpeed * Input.GetAxis("Horizontal"), moveSpeed * Input.GetAxis("Vertical"), 0f);
	}
}
