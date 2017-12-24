using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	Rigidbody2D rb;
	private float moveSpeed = 2f;
	private InputDevice playerInput;
	public InputDevice PlayerInput {
		get {return playerInput;}
	}

	private GameObject ship;
	private GameObject shipHull; 

	private List<GameObject> adjInteractables = new List<GameObject>(); 
	private float repairRate = 5f; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		ship = GameObject.Find("Ship");
		shipHull = GameObject.Find ("Hull"); 
	}

	// Update is called once per frame
	void Update () {
		//Rotate around ship
		transform.RotateAround(ship.transform.position, Vector3.forward, ship.GetComponent<ShipControls>().RotationThisFrame);
		//Move the player with the ship and with their input
		Rigidbody2D shipRB = ship.GetComponent<Rigidbody2D> ();
		Vector3 shipV = new Vector3 (shipRB.velocity.x, shipRB.velocity.y);
		Vector3 playerV = new Vector3 (-moveSpeed * Input.GetAxis (playerInput.Horizontal), moveSpeed * Input.GetAxis (playerInput.Vertical));
		rb.velocity =  shipV + transform.TransformDirection(playerV); 

		//If the player hits the interact button, then repair the hole they are standing on
		if(Input.GetButton("Interact")) { 
			if(adjInteractables.Count != 0) {
				shipHull.GetComponent<ShipHull>().RepairHole(adjInteractables[0].GetComponent<ShipHole>(), repairRate); 
				//adjInteractables [0].interact (); 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Add shiphole to list of shipholes
		if (col.tag == "ShipHole") {
			adjInteractables.Add(col.gameObject); 
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		//Remove shiphole from list of shipholes
		if (adjInteractables.Contains(col.gameObject)) {
			adjInteractables.Remove(col.gameObject); 
		}
	}

	public void SetInput(InputDevice i) {
		playerInput = i;
	}
		
}
