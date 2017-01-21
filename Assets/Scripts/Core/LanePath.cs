﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LanePath : MonoBehaviour {

    public bool ShowDebugLines = true;
    public float DistanceFromStartToStopLight;
    public Vector3 LaneVectorDirection;
    public Transform StartNode;
    public Transform EndNode;

    public Vector3 StopLightPosition;
    public float PercentTowardStop = 0.5f;

    public Vector3 LaneStartPosition
    {
        get { return StartNode.position; }
    }
    public Vector3 LaneEndPosition
    {
        get
        {
            return EndNode.position;
        }
    }

    void Start()
    {
        UpdateLaneProperties();
    }

    public void UpdateLaneProperties()
    {
        var distance = Vector3.Distance(LaneStartPosition, LaneEndPosition);
        LaneVectorDirection = (LaneEndPosition - LaneStartPosition).normalized;
        DistanceFromStartToStopLight = distance * PercentTowardStop;
        StopLightPosition = LaneStartPosition + (LaneVectorDirection * DistanceFromStartToStopLight);
    }

    public void Update()
    {
        if (ShowDebugLines)
        {
            Debug.DrawLine(LaneStartPosition, LaneEndPosition, Color.blue);
            Debug.DrawLine(LaneStartPosition, StopLightPosition, Color.red);
        }

    }




    
}
