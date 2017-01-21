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
            myScript.SpawnRandomVehicle(DataTypes.Direction.South);
        }

        if (GUILayout.Button("Spawn From NE"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.East);
        }

        if (GUILayout.Button("Spawn From SW"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.West);
        }

        if (GUILayout.Button("Spawn From NW"))
        {
            myScript.SpawnRandomVehicle(DataTypes.Direction.North);
        }
    }

}
