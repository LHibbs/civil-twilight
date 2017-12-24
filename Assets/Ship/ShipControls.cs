using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour {

    private Rigidbody2D rb;
    private float moveSpeed = 10f;
    private float rotateSpeed = 50f;

	private float lastAngle = 0f;
	private float rotationThisFrame;

	public float RotationThisFrame {
		get { return rotationThisFrame; }
	}

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        // Forward/Backwards
        rb.velocity = transform.up * Input.GetAxis("ShipVertical") * moveSpeed;
        // Turn Left/Right
        rb.angularVelocity = rotateSpeed * Input.GetAxis("ShipHorizontal");

		//Get my new rotation
		float curAngle = transform.eulerAngles.z;
		rotationThisFrame = curAngle - lastAngle;
		//Debug.Log ("Last: " + lastAngle + " | Current: " + curAngle + " | Rotation: " + rotationThisFrame);
		lastAngle = curAngle;

    }

 


}
