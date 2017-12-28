using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour {

    private Rigidbody2D rb;
    private float moveSpeed = 10f;
	private float maxMoveSpeed = 20f;
    private float rotateSpeed = 10f;
	private float maxRotateSpeed = 40f;
	private CaptainsChair captainsChair; 

	private GameObject sail;
	private float leftRopeLength = 3f;
	private float rightRopeLength = 3f;

	private float lastAngle = 0f;
	private float rotationThisFrame;

	public float RotationThisFrame {
		get { return rotationThisFrame; }
	}

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
		captainsChair = (CaptainsChair) GameObject.Find ("CaptainsChair").GetComponent<CaptainsChair>();  
		sail = GameObject.Find ("Sail");
    }

    // Update is called once per frame
    void Update () {


		/*This is VELOCITY. We are now using forces
        // Forward/Backwards
        rb.velocity = transform.up * Input.GetAxis("ShipVertical") * moveSpeed;
        // Turn Left/Right
        rb.angularVelocity = rotateSpeed * Input.GetAxis("ShipHorizontal");
		*/
		 
		steerShip (); 

		//Get my new rotation
		float curAngle = transform.eulerAngles.z;
		rotationThisFrame = curAngle - lastAngle;
		lastAngle = curAngle;


		//Sail controls
		float moveAmount = 0.01f;
		//Left
		if(Input.GetKey(KeyCode.I)) {
			leftRopeLength += moveAmount;
		} else if(Input.GetKey(KeyCode.K)) {
			leftRopeLength -= moveAmount;
		}
		//Right
		if(Input.GetKey(KeyCode.O)) {
			rightRopeLength += moveAmount;
		} else if (Input.GetKey(KeyCode.L)) {
			rightRopeLength -= moveAmount;
		}
		//Line Renderer
		Vector3 leftVertex = new Vector3(transform.position.x + 2f, transform.position.y + 1.12f + leftRopeLength, 4);
		Vector3 rightVertex = new Vector3 (transform.position.x - 2f, transform.position.y + 1.12f + rightRopeLength, 4);
				
		LineRenderer lr = sail.GetComponent<LineRenderer>();
		lr.SetPosition(0, leftVertex);
		lr.SetPosition (1, rightVertex);
		//Debug.Log ("Left: " + leftVertex + " | Right: " + rightVertex);

    }
		
	void OnTriggerStay2D(Collider2D coll) {
		if(coll.tag == "Wind") {
			float magnitude = 10f;
			//Assuming: Angle = 0 deg
			//Assuming: Magnitude = 10f
			/*
			 * new angle = 90 - tan^-1(left-right / width of ship) //Angle to vertical 
			 * new vector with x = mag * cos theta, y = mag * sin theta
			 * 
			 */
			float newAngle = Mathf.Atan ((leftRopeLength - rightRopeLength) / 4);
			float newMag = Mathf.Cos (newAngle) * magnitude; 
			//float newMag = Mathf.Sqrt(Mathf.Pow(magnitude * Mathf.Cos (newAngle),2) + Mathf.Pow(magnitude * Mathf.Sin (newAngle),2));
			Debug.Log ("New angle " + Mathf.Rad2Deg * newAngle + "New mag " + newMag); 
			//rb.AddForceAtPosition ();
		}
	}

	void steerShip(){
		if(captainsChair.PlayerInput != null)
		{
			if(rb.velocity.magnitude > -maxMoveSpeed && rb.velocity.magnitude < maxMoveSpeed) {
				rb.AddForce(transform.up * Input.GetAxis("ShipVertical") * moveSpeed);
			}
			//Turn
			if(rb.angularVelocity > -maxRotateSpeed && rb.angularVelocity < maxRotateSpeed) {
				rb.AddTorque (Input.GetAxis ("ShipHorizontal") * rotateSpeed);
			}
						
		}
	}
		
}
