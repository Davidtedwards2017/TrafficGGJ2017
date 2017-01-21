using UnityEngine;
using System.Collections;

public class CollisionMessageBehavior : MonoBehaviour
{
    public delegate void CollisionHandler(Collision col);
    public event CollisionHandler onCollision;

    public delegate void TriggerHandler(Collider col);
    public event TriggerHandler onTrigger;

    public void OnCollisionEnter(Collision collision)
    {
        if (onCollision != null) onCollision(collision);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (onTrigger != null) onTrigger(other);
    }
}
