using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Vehicle : MonoBehaviour
{
    public float DestructionAmount = 0.0f;
    public bool ShowDebugLines = true;

    public float TaleGatingDistance = 1;
    public bool PastStopLight;
    public StreetController Street;
    public Vehicle NextVehicle;

    public AudioSource audioSource;
    private Rigidbody rigidbody;
    private Collider collider;

    public Vector3 lastPosition;
    public float AccelThreshold = 1;
    public float DecelThreshold = -0.5f;
    public float AccelerationCheckInterval = 0.1f;

    public MinMaxEventFloat patience = new MinMaxEventFloat(0f, 3f, 3f);

    VehicleStateController StateCtrl = new VehicleStateController();

    public VehicleAnimatorController anim;
    //public DataTypes.Direction direction;

    public GameObject crashFX;

    public AudioClip honkSound;
    public AudioClip movementSound;
    public AudioClip startSound;
    public AudioClip stopSound;

    public Tween MovementTween;
    private Vector3 m_TargetPosition;
    public Vector3 TargetPosition
    {
        get { return m_TargetPosition; }
        set
        {
            if (value.Equals(m_TargetPosition))
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
    public float DiffDistance;

    IEnumerator AccelCheck()
    {
        while(true)
        {
            var lastDistance = Vector3.Distance(lastPosition, transform.position);
            lastPosition = transform.position;
            yield return new WaitForSeconds(AccelerationCheckInterval);
            var newDistance = Vector3.Distance(lastPosition, transform.position);

            DiffDistance = (newDistance - lastDistance);
            if (DiffDistance > AccelThreshold)
            {
                OnAcceleration();
            }
            else if(DiffDistance < DecelThreshold)
            {
                OnDeceleration();
            }
            
        }
    }
    
    public void OnAcceleration()
    {
        //PlayStartMotor();
    }

    public void OnDeceleration()
    {
        //PlayHitBrakes();
    }

    public bool HasPassedStopLight()
    {
        var distanceTraveled = Vector3.Distance(Street.LanePathData.LaneStartPosition, transform.position);
        return (distanceTraveled > Street.LanePathData.DistanceFromStartToStopLight);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    // Use this for initialization
    void Start()
    {


        DrivingTowardsIntersection.Init(this);
        StoppingAtIntersection.Init(this);
        DrivingPastIntersection.Init(this);
        Crashing.Init(this);

        MessageController.StartListening("LaneOpened", LaneOpened);
        MessageController.StartListening("LaneClosed", LaneClosed);

        StartCoroutine(AccelCheck());

        StateCtrl.ChangeState(DrivingTowardsIntersection);

        patience.OnValueBelowHalf += PlayHonk;
        patience.OnValueChangeTo += anim.HandlePatience;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Vehicle"))
        {
            Crash();
            rigidbody.AddExplosionForce(700f, collision.contacts[0].point, 5f);
        }
    }

    void Crash()
    {
        rigidbody.useGravity = true;
        anim.HandleCrash();

        Instantiate(crashFX, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(AudioManager.instance.crashes.PickRandom());
        MovementTween.Pause();

        
        
    }

    void PlayHonk()
    {
        AudioClip clip = honkSound != null ? honkSound : AudioManager.instance.horns.PickRandom();
        audioSource.PlayOneShot(clip);
    }

    void PlayStartMotor()
    {
        AudioClip clip = startSound != null ? startSound : AudioManager.instance.motorStarts.PickRandom();
        audioSource.PlayOneShot(clip);
    }

    void PlayHitBrakes()
    {
        AudioClip clip = stopSound != null ? stopSound : AudioManager.instance.skids.PickRandom();
        audioSource.PlayOneShot(clip);
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
            else if (!Vehicle.Street.Open)
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
            Vehicle.patience.value -= Time.deltaTime;

            if (Vehicle.patience.value <= 0)
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

                if (Vehicle.NextVehicle.HasPassedStopLight())
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

            if (Vector3.Distance(Vehicle.TargetPosition, Vehicle.transform.position) < 0.1f)
            {
                MessageController.SendMessage("VehicleReachedDestination", Vehicle);
                Destroy(Vehicle.gameObject);
            }

            Vehicle.patience.value += Time.deltaTime * 3f;
        }
    }

    public CrashingState Crashing = new CrashingState();
    public class CrashingState : VehicleStateController.VehicleState
    {
        public override void OnEnter()
        {
            this.Vehicle.rigidbody.useGravity = true;
            GameController.instance.Destruction.value += Vehicle.DestructionAmount;
            MessageController.SendMessage("VehicleCrashed", Vehicle);
        }
    }



}
