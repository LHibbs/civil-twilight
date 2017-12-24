using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject playerPrefab;

	private GameObject psuedoPlayer;
	private GameObject player1;
	private GameObject player2;
	private GameObject player3;
	private GameObject player4;

	private InputDevice psuedoInput;
	private InputDevice I1;
	private InputDevice I2;
	private InputDevice I3;
	private InputDevice I4;

	// Use this for initialization
	void Start () {
		//Inputs
		psuedoInput = new InputDevice("Horizontal", "Vertical", "Interact");

		I1 = new InputDevice("Horizontal1", "Vertical1", "Interact1");
		I2 = new InputDevice("Horizontal2", "Vertical2", "Interact2");
		I3 = new InputDevice("Horizontal3", "Vertical3", "Interact3");
		I4 = new InputDevice("Horizontal4", "Vertical4", "Interact4");

		//Psuedo Player
		psuedoPlayer = Instantiate (playerPrefab, new Vector3(1, 1, 4f), Quaternion.identity, transform);
		Body ppb = psuedoPlayer.GetComponent<Body> (); //Set the body the player starts with to have the player's input
		ppb.SetInput (psuedoInput);
		//p1.SetRole();
	}
}
