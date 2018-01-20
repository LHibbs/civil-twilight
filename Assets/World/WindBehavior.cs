using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehavior : MonoBehaviour {

	//my one wind as a degree of change of 0.1 (derivative of slope). PLACEHOLDER
	float deltaWind = 0.01f; 
	float startHeading = 0f; 
	float currentHeading; 

	public float CurrentHeading {
		get { 
			currentHeading += deltaWind;
			return currentHeading; 
		}
	}

	// Use this for initialization
	void Start () {
		currentHeading = startHeading; 
	}
	
	// Update is called once per frame
	void Update () { 
	}
}
