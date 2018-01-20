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
	private float ropeMoveSpeed = 0.01f;
	private float leftRopeLength = 3f;
	private float rightRopeLength = 3f;
	private Vector2 leftAnchor;
	private Vector2 rightAnchor;
	float shipWidth = 2f;
	float shipHeight = 1.12f;

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
		//Left
		if(Input.GetKey(KeyCode.I)) {
			leftRopeLength += ropeMoveSpeed;
		} else if(Input.GetKey(KeyCode.K)) {
			leftRopeLength -= ropeMoveSpeed;
		}
		//Right
		if(Input.GetKey(KeyCode.O)) {
			rightRopeLength += ropeMoveSpeed;
		} else if (Input.GetKey(KeyCode.L)) {
			rightRopeLength -= ropeMoveSpeed;
		}

		MoveSail ();
		DrawSail ();
    }

	void MoveSail () {
		Vector2 leftVertex = new Vector3(shipWidth, shipHeight + leftRopeLength);
		Vector2 rightVertex = new Vector3 (-shipWidth, shipHeight + rightRopeLength);
		leftAnchor = RotatePointAroundOrigin(transform.position, leftVertex, transform.eulerAngles.z);
		rightAnchor = RotatePointAroundOrigin (transform.position, rightVertex, transform.eulerAngles.z);
	}

	void DrawSail() {
		float sailZLayer = 3f;
		LineRenderer lr = sail.GetComponent<LineRenderer>();
		lr.SetPosition(0, new Vector3(leftAnchor.x, leftAnchor.y, sailZLayer));
		lr.SetPosition (1, new Vector3(rightAnchor.x, rightAnchor.y, sailZLayer));
	}

	/*angle is in degrees*/ 
	public static Vector2 RotatePointAroundOrigin(Vector2 localOrigin, Vector2 point, float angle) {
		angle = angle * Mathf.Deg2Rad; 
		float xFinal = point.x * Mathf.Cos (angle) - point.y * Mathf.Sin (angle);
		float yFinal = point.y * Mathf.Cos (angle) + point.x * Mathf.Sin (angle);

		return new Vector2(xFinal + localOrigin.x, yFinal + localOrigin.y);
	}


		
	void OnTriggerStay2D(Collider2D coll) {
		if(coll.tag == "Wind") {
		/* magic number n = 10 degrees
		 * If the sail is within 10 degrees of the current heading, change the heading of the ship by that much
		 * The sail is static with the ship
		 * This means, if the current changes subtly the entire ship will track
		 * ex: current changes +8 degrees, ship changes + 8 degrees
		 * 
		 * ex2: current changes +30 degrees, sail at +20degrees, ship heading +10 degrees. 
		 * Assuming current keeps changing, the ship will slowly track 
		 *
		 *deltaWind is the rate of change of the heading of the wind at every moment
		 */ 
			//my one wind as a degree of change of 3 (derivative of slope) 

			float magicTolerance = 10f; 
			float magicMaxMove = .01f;

			float angleSail = Mathf.Atan((leftRopeLength - rightRopeLength) / shipWidth) * Mathf.Rad2Deg;
			angleSail += transform.eulerAngles.z; 

			WindBehavior wb = coll.GetComponent<WindBehavior> ();

			Debug.Log ("angle sail " + angleSail + " angle current " + wb.CurrentHeading); 
			if(Mathf.Abs(angleSail - wb.CurrentHeading) < magicTolerance) 
			{
				Vector3 temp = transform.eulerAngles;
				temp.z += magicMaxMove;
				transform.eulerAngles = temp;
				rb.AddForce (transform.up * 2f); 
			}

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
