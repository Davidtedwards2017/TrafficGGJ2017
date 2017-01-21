using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="vehicle", menuName ="Game/vehicle")]
public class VehicleData : ScriptableObject {

    public GameObject Asset;
    public DataTypes.Direction Direction;
    public int Size = 1;
}
