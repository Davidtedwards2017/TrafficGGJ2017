using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class StateManager<T>
{

    public string displayedStateName = "";

    public Action<T> OnChanged;

    private Dictionary<object, State> stateLookup;

    private T _value; // The current state as represented by the matching Enum value 
    public T value
    {
        get
        {
            return _value;
        }
        set
        {
            if (state == stateLookup[value]) return;
            //Debug.Log("State is: " + _value + " || Changing state to: " + value);
            _value = value;
            state = stateLookup[_value];
        }
    }

    //Dictionary of Enum to State object
    public Dictionary<T, State> values
    {
        get; private set;
    }

    private State _state; // The current state object
    [SerializeField]
    public State state
    {
        get
        {
            return _state;
        }
        set
        {
            if (value == null)
            {
                Debug.LogError("Attempting to enter null state. Ensure state has been constructed before assigning it to a StateManager.");
            }

            if (_state == value)
            {
                Debug.Log("Not performing redundant state change.");
                return;
            }

            if (_state != null && _state.OnExit != null)
            {
                _state.OnExit();
                _state.StopCoroutines();
                //"Calling OnExit for state".DebugLogJustin();
            }
            else
            {
                //"OnExit for state is null".DebugLogDom();
            }

            _state = value;

            if (_state.OnEnter != null) _state.OnEnter();
            _state.StartCoroutines();
            if (OnChanged != null) OnChanged(this.value);
        }
    }

    public static StateManager<T> CreateNew()
    {
        var stateManager = new StateManager<T>();

        stateManager.Initialize();

        return stateManager;
    }

    private void Initialize()
    {
        //Define States
        var values = Enum.GetValues(typeof(T));
        if (values.Length < 1) { throw new ArgumentException("Enum provided to Initialize must have at least 1 visible definition"); }

        //Assign values a Dictionary of the Enum type to State
        this.values = new Dictionary<T, State>();

        //Assign stateLookup a Dictionary of object to State
        stateLookup = new Dictionary<object, State>();

        //State defaultState = null;

        //For each value in the Enum,
        for (int i = 0; i < values.Length; i++)
        {
            //Create a new State
            var mapping = new State((Enum)values.GetValue(i));
            //Add it to the stateLookup dictionary
            stateLookup.Add(mapping.state, mapping);
            //Add the State to the values dictionary
            this.values.Add((T)values.GetValue(i), stateLookup[mapping.state]);
        }
        //Assign the default state as the first item in the Enum
        //this.value = default(T); 
        //this.value = (T)values.GetEnumerator().Current;
        //this.value = defaultState;

    }

    public class State
    {
        public object state;

        public Action OnEnter;

        public Action OnExit;

        public List<CoroutineManager.Item> coroutines = new List<CoroutineManager.Item>();

        public State(object state)
        {
            this.state = state;
        }

        public void StartCoroutines()
        {
            for (int n = 0; n < coroutines.Count; n++)
            {
                coroutines[n].Start();
            }
        }

        public void StopCoroutines()
        {
            for (int n = 0; n < coroutines.Count; n++)
            {
                coroutines[n].Stop();
            }
        }

        /// <summary>
        /// Add the an IENumerator which should run when entering this state. Stops when exiting state.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static State operator +(State s, IEnumerator i)
        {
            CoroutineManager.Item item = new CoroutineManager.Item(i, false);
            s.coroutines.Add(item);
            return s;
        }
        /// <summary>
        /// Removes an IENumerator which should run when entering this state, and stops it immediately.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static State operator -(State s, IEnumerator i)
        {
            for (int n = 0; n < s.coroutines.Count; n++)
            {
                CoroutineManager.Item item = s.coroutines[n];
                if (item.value == i)
                {
                    item.Stop();
                    s.coroutines.Remove(item);
                }
            }

            return s;
        }
    }
}
