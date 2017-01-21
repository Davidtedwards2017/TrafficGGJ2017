using UnityEngine;
using System.Collections.Generic;

public class VehicleStateController  {
    
    public VehicleState CurrentState;

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    public void OnLaneOpened()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLaneOpened();
        }
    }


    public void OnLaneClosed()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLaneClosed();
        }
    }

    public void ChangeState(VehicleState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = newState;
        CurrentState.OnEnter();
    }

    [System.Serializable]
    public abstract class VehicleState
    {
        protected Vehicle Vehicle;

        public void Init(Vehicle vehicle)
        {
            Vehicle = vehicle;


        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLaneOpened()
        {

        }

        public virtual void OnLaneClosed()
        {

        }
    }

}
