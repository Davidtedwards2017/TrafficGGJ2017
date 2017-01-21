using UnityEngine;
using System.Collections;
using System.Linq;

public class IntersectionController : Singleton<IntersectionController> {

    public StreetController[] Streets;

    public StreetController GetStreet(DataTypes.Direction direction)
    {
        return Streets.FirstOrDefault(s => s.Direction.Equals(direction));
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
