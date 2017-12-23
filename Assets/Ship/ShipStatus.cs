using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour {

	[SerializeField]
	private float air = 100f;
	List<ShipHole> holes = new List<ShipHole>(); 
	public GameObject holePrefab; 
	private float airLossRate = 0.07f; 

	private Rigidbody2D rb; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); 
	}
	
	// Update is called once per frame
	void Update () {

		rb.velocity = new Vector3 (1, 0, 0); 

		RandomHoleGenerator (); 
		CheckForLeaks(); 

		if(air <= 0) {
			Debug.Log ("Game over"); 
		}
	}

	void RandomHoleGenerator() {
		if (Random.value < 0.005) {
			CreateHole (Random.Range (-7f, 7f), Random.Range (-4f, 4f)); 
		}
	}

	void CreateHole(float x, float y){
		holes.Add (Instantiate (holePrefab, new Vector3 (x, y, 0.005f), Quaternion.Euler(90,0,0)).GetComponent<ShipHole>()); 
	}

	void CheckForLeaks(){
		foreach (ShipHole hole in holes){
			if(hole == null )
			{
				holes.Remove (hole); 
			}
			if(hole.IsLeaking) {
				air -= airLossRate; 
			}
		}
	}

	public void repairHole(ShipHole hole, float repairRate){
		if (hole.fixHole (repairRate))
			holes.Remove (hole); 
	}
		
}
