using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class StreetController : MonoBehaviour {

    public bool Open;
    public DataTypes.Direction Direction;
    public LanePath LanePathData;

    public Queue<Vehicle> Vehicles;

    // Use this for initialization
    void Start () {
        MessageController.StartListening("DirectionInputChanged", OnDirectionInputChanged);
	}
	 
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnDirectionInputChanged(object[] args)
    {
        var dir = (DataTypes.Direction) args[0];
        if(!dir.Equals(Direction))
        {
            return;
        }

        Open = (bool) args[1];

        if(Open)
        {
            Debug.Log(string.Format("{0} lane opened", Direction));
            MessageController.SendMessage("LaneOpened", Direction);
        }
        else
        {
            Debug.Log(string.Format("{0} lane closed", Direction));
            MessageController.SendMessage("LaneClosed", Direction);
        }
    }
}
