﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Vehicle : MonoBehaviour {
    
    public bool ShowDebugLines = true;

    public float TaleGatingDistance = 1;
    public bool PastStopLight;
    public StreetController Street;
    public Vehicle NextVehicle;

    private Rigidbody RidgetBody;
    private Collider collider;

    public float Patience = 2.0f;

    VehicleStateController StateCtrl = new VehicleStateController();

    public VehicleAnimatorController anim;
    //public DataTypes.Direction direction;

    public Tween MovementTween;
    private Vector3 m_TargetPosition;
    public Vector3 TargetPosition
    {
        get { return m_TargetPosition; }
        set
        {
            if(value.Equals(m_TargetPosition))
            {
                return;
            }
            
            m_TargetPosition = value;
            var distance = Vector3.Distance(transform.position, m_TargetPosition);

            MovementTween.Pause();
            MovementTween = transform.DOMove(m_TargetPosition, distance / Speed);
        }
    }

    public float Speed = 1;

    public bool HasPassedStopLight()
    {
        var distanceTraveled = Vector3.Distance(Street.LanePathData.LaneStartPosition, transform.position);
        return (distanceTraveled > Street.LanePathData.DistanceFromStartToStopLight);
    }

	// Use this for initialization
	void Start () {
        RidgetBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        MessageController.StartListening("LaneOpened", LaneOpened);
        MessageController.StartListening("LaneClosed", LaneClosed);

        DrivingTowardsIntersection.Init(this);
        StoppingAtIntersection.Init(this);
        DrivingPastIntersection.Init(this);
        Crashing.Init(this);

        StateCtrl.ChangeState(DrivingTowardsIntersection);
	}

    public void InitializeAnimator(DataTypes.Direction direction)
    {
        anim.Initialize(direction);
    }

    // Update is called once per frame
    void Update()
    {
        StateCtrl.UpdateState();
        PastStopLight = HasPassedStopLight();
        
        if (ShowDebugLines)
        {
            Debug.DrawLine(transform.position, GetTaleGatingPostion(), Color.yellow);
        }

    }

    public void LaneOpened(object[] args)
    {
        if (!Street.Direction.Equals((DataTypes.Direction)args[0]))
        {
            return;
        }
        StateCtrl.OnLaneOpened();
    }

    public void LaneClosed(object[] args)
    {
        if (!Street.Direction.Equals((DataTypes.Direction)args[0]))
        {
            return;
        }
        StateCtrl.OnLaneClosed();
    }
    
    public Vector3 GetTaleGatingPostion()
    {
        return transform.position - (Street.LanePathData.LaneVectorDirection * TaleGatingDistance);
    }

    public DrivingTowardsIntersectionState DrivingTowardsIntersection = new DrivingTowardsIntersectionState();
    public class DrivingTowardsIntersectionState : VehicleStateController.VehicleState
    {
        public override void OnEnter()
        {
            Vehicle.TargetPosition = Vehicle.Street.LanePathData.LaneEndPosition;
        }

        public override void OnLaneClosed()
        {
            Vehicle.StateCtrl.ChangeState(Vehicle.StoppingAtIntersection);
        }

        public override void OnUpdate()
        {
            if (Vehicle.NextVehicle != null)
            {
                if (!(Vehicle.NextVehicle.StateCtrl.CurrentState is CrashingState))
                {
                    Vehicle.TargetPosition = Vehicle.NextVehicle.GetTaleGatingPostion();
                }
            }
            
            if (Vehicle.HasPassedStopLight())
            {
                Vehicle.StateCtrl.ChangeState(Vehicle.DrivingPastIntersection);
            }
            else if(!Vehicle.Street.Open)
            {
                Vehicle.StateCtrl.ChangeState(Vehicle.StoppingAtIntersection);
            }
        }
    }

    public StoppingAtIntersectionState StoppingAtIntersection = new StoppingAtIntersectionState();
    public class StoppingAtIntersectionState : VehicleStateController.VehicleState
    {   
        public override void OnEnter()
        {
            Vehicle.TargetPosition = Vehicle.Street.LanePathData.StopLightPosition;
        }

        public override void OnUpdate()
        {
            Vehicle.Patience -= Time.deltaTime;

            if(Vehicle.Patience <= 0)
            {
                MessageController.SendMessage("VehicleLostPatience", Vehicle);
                Vehicle.StateCtrl.ChangeState(Vehicle.DrivingPastIntersection);
            }

            if (Vehicle.NextVehicle != null)
            {
                if ((Vehicle.NextVehicle.StateCtrl.CurrentState is StoppingAtIntersectionState))
                {
                    Vehicle.TargetPosition = Vehicle.NextVehicle.GetTaleGatingPostion();
                }

                if(Vehicle.NextVehicle.HasPassedStopLight())
                {
                    Vehicle.TargetPosition = Vehicle.Street.LanePathData.StopLightPosition;
                }
            }
        }

        public override void OnLaneOpened()
        {
            Vehicle.StateCtrl.ChangeState(Vehicle.DrivingTowardsIntersection);
        }
    }

    public DrivingAwayFromIntersectionState DrivingPastIntersection = new DrivingAwayFromIntersectionState();
    public class DrivingAwayFromIntersectionState : VehicleStateController.VehicleState
    {
        public override void OnEnter()
        {
            Vehicle.TargetPosition = Vehicle.Street.LanePathData.LaneEndPosition;
        }

        public override void OnUpdate()
        {
            if (Vehicle.NextVehicle != null)
            {
                if (!(Vehicle.NextVehicle.StateCtrl.CurrentState is CrashingState))
                {
                    Vehicle.TargetPosition = Vehicle.NextVehicle.GetTaleGatingPostion();
                }
            }
            else
            {
                Vehicle.TargetPosition = Vehicle.Street.LanePathData.LaneEndPosition;
            }

            if(Vector3.Distance(Vehicle.TargetPosition, Vehicle.transform.position) < 0.1f)
            {
                MessageController.SendMessage("VehicleReachedDestination", Vehicle);
                Destroy(Vehicle.gameObject);
            }
        }
    }

    public CrashingState Crashing = new CrashingState();
    public class CrashingState : VehicleStateController.VehicleState
    {
        public override void OnEnter()
        {
            this.Vehicle.RidgetBody.useGravity = true;
            MessageController.SendMessage("VehicleCrashed", Vehicle);
        }
    }



}
