﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputController : Singleton<InputController> {

    public DirectionInput[] DirectionInputs;

    [System.Serializable]
    public class DirectionInput
    {
        public DataTypes.Direction Direction;
        public bool Value;
        public bool CachedValue;
        public string Key;

        public void UpdateInput()
        {
            Value = Input.GetKey(Key);
            if(Value != CachedValue)
            {
                CachedValue = Value;
                MessageController.SendMessage("DirectionInputChanged", Direction, Value);
            }
        }
    }

    public bool IsDirectionPressed(DataTypes.Direction direction)
    {
        var dirInput = DirectionInputs.FirstOrDefault(d => d.Direction.Equals(direction));
        if(dirInput == null)
        {
            return false;
        }

        return dirInput.Value;
    }


	// Use this for initialization
	void Start () {
        DirectionInputs = new DirectionInput[4];
        DirectionInputs[0] = new DirectionInput { Key = "a", Direction = DataTypes.Direction.West };
        DirectionInputs[1] = new DirectionInput { Key = "w", Direction = DataTypes.Direction.North };
        DirectionInputs[2] = new DirectionInput { Key = "d", Direction = DataTypes.Direction.East };
        DirectionInputs[3] = new DirectionInput { Key = "s", Direction = DataTypes.Direction.South };

    }
	
	// Update is called once per frame
	void Update () {
        
        foreach(var dirInput in DirectionInputs)
        {
            dirInput.UpdateInput();
        }
    }
}
