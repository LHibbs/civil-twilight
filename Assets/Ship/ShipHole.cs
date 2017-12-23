using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHole : MonoBehaviour {

	private float health = 100f;
	private float startTime; 
	private bool isLeaking = false; 
	private int timeBeforeDmg = 10;

	public bool IsLeaking {
		get { return isLeaking; }
	}
		
	// Use this for initialization
	void Start () {
		startTime = Time.time; 
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > timeBeforeDmg){
			isLeaking = true; 
		}
	}

	public bool fixHole(float repairRate){
		health -= repairRate; 
		if(health <= 0){
			Destroy (gameObject);
			return true; 
		}
		return false; 
	}
}
