using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VehicleFactory))]
public class VehicleFactoryInEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VehicleFactory myScript = (VehicleFactory)target;
        if (GUILayout.Button("Spawn From SE"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.SouthEast);
        }

        if (GUILayout.Button("Spawn From NE"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.NorthEast);
        }

        if (GUILayout.Button("Spawn From SW"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.SouthWest);
        }

        if (GUILayout.Button("Spawn From NW"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.NorthWest);
        }
    }

}
