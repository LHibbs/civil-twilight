using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHull : MonoBehaviour {

    [SerializeField]
    private float air = 100f;
    List<ShipHole> holes = new List<ShipHole>();
    public GameObject holePrefab;
    private float airLossRate = 0.07f;

	
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
        if (Random.value < 0.005)
        {
            CreateHole(Random.Range(transform.position.x - 1.95f, transform.position.x + 1.95f), Random.Range(transform.position.y - 4f, transform.position.y + 4f));
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
