using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHull : MonoBehaviour {

    [SerializeField]
    private float air = 100f;
    List<ShipHole> holes = new List<ShipHole>();
    public GameObject holePrefab; 
    private float airLossRate = 0.07f;

	private float xDim = 2f; 
	private float yDim = 4.5f; 
	
	// Update is called once per frame
	void Update () {
        RandomHoleGenerator();
        CheckForLeaks();

        if (air <= 0)
        {
            Debug.Log("Game over");
        }
    }

    void RandomHoleGenerator()
    {
		if (Random.value < 0.1)
		{
			BoxCollider2D shipCollider = transform.parent.gameObject.GetComponent<BoxCollider2D> ();
			float xPos = transform.position.x;
			float yPos = transform.position.y;
			float randomX = Random.Range (- xDim, xDim); 
			float randomY = Random.Range (- yDim, yDim); 
			/*float angle = Mathf.Deg2Rad * transform.eulerAngles.z; 
			 * float xFinal = randomX * Mathf.Cos (angle) - randomY * Mathf.Sin (angle);
			float yFinal = randomY * Mathf.Cos (angle) + randomX * Mathf.Sin (angle);

			CreateHole(xFinal + xPos, yFinal + yPos);*/
			Vector2 v2 = ShipControls.RotatePointAroundOrigin (transform.position, new Vector2 (randomX, randomY), transform.eulerAngles.z);
			CreateHole (v2.x, v2.y); 
       }
    }

    void CreateHole(float x, float y)
    {
        holes.Add(Instantiate(holePrefab, new Vector3(x, y, 3f), Quaternion.Euler(90, 0, 0), transform.parent).GetComponent<ShipHole>());
    }

    void CheckForLeaks()
    {
        foreach (ShipHole hole in holes)
        {
            if (hole == null)
            {
                holes.Remove(hole);
            }
            if (hole.IsLeaking)
            {
                air -= airLossRate;
            }
        }
    }

    public void RepairHole(ShipHole hole, float repairRate)
    {
        if (hole.fixHole(repairRate))
            holes.Remove(hole);
    }

}
