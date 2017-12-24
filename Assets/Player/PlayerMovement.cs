using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	private float moveSpeed = 2f;

	private InputDevice input;

    private GameObject ship;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
        ship = GameObject.Find("Ship");
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate around ship
		transform.RotateAround(ship.transform.position, Vector3.forward, ship.GetComponent<ShipControls>().RotationThisFrame);

		Vector3 shipV = new Vector3 (ship.GetComponent<Rigidbody2D> ().velocity.x, ship.GetComponent<Rigidbody2D> ().velocity.y);
		Vector3 playerV = new Vector3 (-moveSpeed * Input.GetAxis (input.Horizontal), 
			                  moveSpeed * Input.GetAxis (input.Vertical), 
			                  0f);
		rb.velocity =  shipV + transform.TransformDirection(playerV); 
    }

	public void SetInput(InputDevice i) {
		input = i;
	}
}
