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
		Vector3 leftVertex = new Vector3(2f, 1.12f + leftRopeLength, 4);
		Vector3 rightVertex = new Vector3 (- 2f, 1.12f + rightRopeLength, 4);
		//loosing the z coordinate, it is getting set to zero so the line is hidden. Need to change
		Vector3 rotLeft = RotatePointAroundOrigin(transform.position, leftVertex, transform.eulerAngles.z);
		Vector3 rotRight = RotatePointAroundOrigin (transform.position, rightVertex, transform.eulerAngles.z);
		LineRenderer lr = sail.GetComponent<LineRenderer>();
		lr.SetPosition(0, rotLeft);
		lr.SetPosition (1, rotRight);
		//Debug.Log ("Left: " + leftVertex + " | Right: " + rightVertex);
		Debug.Log ("Rot Left: " + rotLeft + " | RotRight: " + rotRight);

    }

	/*angle is in degrees*/ 
	public static Vector2 RotatePointAroundOrigin(Vector2 localOrigin, Vector2 newLoc, float angle) {
		angle = angle * Mathf.Deg2Rad; 
		float xFinal = newLoc.x * Mathf.Cos (angle) - newLoc.y * Mathf.Sin (angle);
		float yFinal = newLoc.y * Mathf.Cos (angle) + newLoc.x * Mathf.Sin (angle);

		return new Vector2(xFinal + localOrigin.x, yFinal + localOrigin.y);


		/*float angle = Mathf.Deg2Rad * a;
		float xFinal = p.x * Mathf.Cos (angle) - p.y * Mathf.Sin (angle);
		float yFinal = p.y * Mathf.Cos (angle) + p.x * Mathf.Sin (angle); 

		return new Vector2(xFinal + p.x, yFinal + p.y);*/
	}
		
	void OnTriggerStay2D(Collider2D coll) {
		if(coll.tag == "Wind") {
			float magnitude = 2f;
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
			Vector2 v2 = new Vector2(newMag * Mathf.Cos(newAngle), newMag * Mathf.Sin(newAngle)); 
			Vector2 pos = new Vector2 (0, 1.12f + ((leftRopeLength + rightRopeLength) / 2)); 
			rb.AddForceAtPosition(v2,pos); 

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
