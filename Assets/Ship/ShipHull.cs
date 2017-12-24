using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHull : MonoBehaviour {

    [SerializeField]
    private float air = 100f;
    List<ShipHole> holes = new List<ShipHole>();
    public GameObject holePrefab;
	public GameObject friendlyCube; 
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
		if (Random.value < 0.01) //.005
		{
			BoxCollider2D shipCollider = transform.parent.gameObject.GetComponent<BoxCollider2D> (); 
			float xPos = transform.position.x;
			float yPos = transform.position.y;
			float angle = transform.eulerAngles.z; 

			angle = Mathf.Deg2Rad * angle; 
        
			float randomX = Random.Range (xPos - xDim, xPos + xDim); 
			float randomY = Random.Range (yPos - yDim, yPos + yDim); 
			float xFinal = randomX * Mathf.Cos (angle) - randomY * Mathf.Sin (angle);
			float yFinal = randomY * Mathf.Cos (angle) + randomX * Mathf.Sin (angle); 

			CreateHole(xFinal, yFinal);
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
