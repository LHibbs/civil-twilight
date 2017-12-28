using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainsChair : MonoBehaviour {

	private Body activeBody;

	public InputDevice PlayerInput {
		get { 
			if(activeBody != null)
				return activeBody.PlayerInput;
			return null; 
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Player")
		{
			activeBody = coll.gameObject.GetComponent<Body> ();
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(coll.tag == "Player")
		{
			if( coll.gameObject.GetComponent<Body> () == activeBody) {
				activeBody = null; 
			}
		}
	}
}
