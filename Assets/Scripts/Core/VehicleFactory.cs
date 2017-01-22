using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class VehicleFactory : Singleton<VehicleFactory> {
    
    public GameObject[] spawnableVehicles;

    public float DifficultyIncreaseAmt = 0.005f;
    public int MaxVehicles = 10;
    public float Interval = 1.5f;

    public MinMaxEventFloat Difficulty = new MinMaxEventFloat(0, 1, 0);
    private MinMaxFloat SpawnInterval = new MinMaxFloat(0.1f, 3.0f, 3.0f);
    
    public List<Vehicle> SpawnedVehicles;
    public CoroutineManager.Item spawningSequence = new CoroutineManager.Item();

    public void IncreseDifficulty()
    {
        Difficulty.value += DifficultyIncreaseAmt;
    }

    public void Reset()
    {
        Difficulty.value = 0;

        foreach(var vehicle in FindObjectsOfType<Vehicle>())
        {
            Destroy(vehicle.gameObject);
        }
    }

    void Start()
    {
        Difficulty.OnValueChangeTo += (val) => {
            SpawnInterval.SetToPercent(1 - val);
        };

        GameController.state.values[GameController.State.Playing].OnEnter += OnEnterPlayingState;
        GameController.state.values[GameController.State.Playing].OnExit += OnExitPlayingState;

        //StartCoroutine(SpawnNext());
        MessageController.StartListening("VehicleReachedDestination", OnVehicleLeavingPlay);
        MessageController.StartListening("VehicleCrashed", OnVehicleLeavingPlay);
       
    }

    public void OnEnterPlayingState()
    {
        spawningSequence.value = SpawnNext();
    }

    public void OnExitPlayingState()
    {
        spawningSequence.value = null;
    }
    
    public void OnVehicleLeavingPlay(object[] args)
    {
        var vehicle = (Vehicle)args[0];
        SpawnedVehicles.SafeRemove(vehicle);
    }

    public IEnumerator SpawnNext()
    {
        while(FindObjectsOfType<Vehicle>().Length >= MaxVehicles)
        {
            yield return new WaitForEndOfFrame();
        }
    
        Array values = Enum.GetValues(typeof(DataTypes.Direction));
        var random = new System.Random();
        DataTypes.Direction rndDir = (DataTypes.Direction)values.GetValue(random.Next(values.Length));
    
        SpawnRandomVehicle(rndDir);

        yield return new WaitForSeconds(SpawnInterval.value);

        spawningSequence.value = SpawnNext();
    }

    public void SpawnRandomVehicle(DataTypes.Direction direction)
    {
        //GameObject vehicle = ;
        //if(vehicle == null)
        //{
        //    return;
        //}

        var street = IntersectionController.instance.GetStreet(direction);

        //Debug.Log(vehicle);

        GameObject go = Instantiate(instance.spawnableVehicles.PickRandom(), street.LanePathData.LaneStartPosition, Quaternion.identity) as GameObject;
        //var vehicleInstance = go;
        Vehicle newVehicle = go.GetComponent<Vehicle>();
        newVehicle.InitializeAnimator(direction);
        street.VehicleSpawned(newVehicle);
        //SpawnedVehicles.Add(vehicle);
    }

    //private VehicleData GetRandomVehicleData(DataTypes.Direction direction)
    //{
    //    var validCars = Vehicles.Where(v => v.Direction.Equals(direction)).ToArray();

    //    if(!validCars.Any())
    //    {
    //        return null;
    //    }

    //    return Vehicles.PickRandom();
    //}
}
