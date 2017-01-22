using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float duration = 0.2f;
    public float strength = 0.5f;

    public Vector3 StartPos;


    CoroutineManager.Item shakeSequence = new CoroutineManager.Item();

    void Start()
    {
        StartPos = transform.position;
        MessageController.StartListening("VehicleCrashed", VehicleChrashed);
    }

    public void VehicleChrashed(object[] args)
    {
        shakeSequence.value = Play();
    }

    public IEnumerator Play()
    {
        
        yield return transform.DOShakePosition(duration, strength, 10, 90, false, true).WaitForCompletion();
        transform.position = StartPos;
    }
}
