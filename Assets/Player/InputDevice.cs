//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public class InputDevice {

	private string horizontal, vertical, interact;

	public InputDevice (string h, string v, string i) {
		horizontal = h;
		vertical = v;
		interact = i;
	}

	public string Horizontal {
		get{ return horizontal; }
	}

	public string Vertical {
		get{ return vertical; }
	}

	public string Interact {
		get{ return interact; }
	}
}
