using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	private List<GameObject> adjInteractables = new List<GameObject>(); 
	private float repairRate = 5f; 
	private GameObject shipHull; 

	void Start () {
		shipHull = GameObject.Find ("Hull"); 
	}

	void Update () {
		if(Input.GetButton("Fire1")) { 
			if(adjInteractables.Count != 0) {
                shipHull.GetComponent<ShipHull>().RepairHole(adjInteractables[0].GetComponent<ShipHole>(), repairRate); 
				//adjInteractables [0].interact (); 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "ShipHole") {
			adjInteractables.Add(col.gameObject); 
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (adjInteractables.Contains(col.gameObject)) {
			adjInteractables.Remove(col.gameObject); 
		}
	}

	ShipHole getHoleFromCol(Collider2D col){
		return col.gameObject.GetComponent<ShipHole> (); 
	}
}
