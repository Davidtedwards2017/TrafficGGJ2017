﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class StreetController : MonoBehaviour {

    public bool Open;
    public DataTypes.Direction Direction;
    public LanePath LanePathData;

    private Vehicle m_LastVehicle;

    public GameObject signHighlight;

    // Use this for initialization
    void Start () {
        MessageController.StartListening("DirectionInputChanged", OnDirectionInputChanged);
	}
	 
	// Update is called once per frame
	void Update () {
	    
	}

    public void VehicleSpawned(Vehicle vehicle)
    {
        vehicle.Street = this;
        vehicle.NextVehicle = m_LastVehicle;
        m_LastVehicle = vehicle;
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
            MessageController.SendMessage("LaneOpened", Direction);
        }
        else
        {
            MessageController.SendMessage("LaneClosed", Direction);
        }

        signHighlight.SetActive(Open);
    }
}
