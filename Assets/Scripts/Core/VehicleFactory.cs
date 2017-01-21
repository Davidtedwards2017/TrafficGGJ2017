﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class VehicleFactory : Singleton<VehicleFactory> {
    
    public VehicleData[] Vehicles;

    public int MaxVehicles = 10;
    public float Interval = 1.5f;
    
    public List<Vehicle> SpawnedVehicles;

    void Start()
    {
        StartCoroutine(SpawnNext());
        MessageController.StartListening("VehicleReachedDestination", OnVehicleLeavingPlay);
        MessageController.StartListening("VehicleCrashed", OnVehicleLeavingPlay);
    }
    
    public void OnVehicleLeavingPlay(object[] args)
    {
        var vehicle = (Vehicle)args[0];
        SpawnedVehicles.SafeRemove(vehicle);
    }

    public IEnumerator SpawnNext()
    {
        while(SpawnedVehicles.Count >= MaxVehicles)
        {
            yield return new WaitForEndOfFrame();
        }
    
        Array values = Enum.GetValues(typeof(DataTypes.Direction));
        var random = new System.Random();
        DataTypes.Direction rndDir = (DataTypes.Direction)values.GetValue(random.Next(values.Length));
    
        SpawnRandomVehicle(rndDir);

        yield return new WaitForSeconds(Interval);
    
        StartCoroutine(SpawnNext());
    }

    public void SpawnRandomVehicle(DataTypes.Direction direction)
    {
        var vehicle = GetRandomVehicleData(direction);
        if(vehicle == null)
        {
            return;
        }

        var street = IntersectionController.instance.GetStreet(direction);

        var go = Instantiate(vehicle.Asset, street.LanePathData.LaneStartPosition, Quaternion.identity) as GameObject;
        var vehicleInstance = go.GetComponent<Vehicle>();

        street.VehicleSpawned(vehicleInstance);
        //SpawnedVehicles.Add(vehicle);
    }

    private VehicleData GetRandomVehicleData(DataTypes.Direction direction)
    {
        var validCars = Vehicles.Where(v => v.Direction.Equals(direction)).ToArray();

        if(!validCars.Any())
        {
            return null;
        }

        return Vehicles.PickRandom();
    }
}
