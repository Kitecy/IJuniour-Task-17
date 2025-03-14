using System.Collections.Generic;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;

    private List<Rigidbody> _contactingObjects = new();

    private void Awake()
    {
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            _contactingObjects.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_contactingObjects.Contains(other.attachedRigidbody))
            _contactingObjects.Remove(other.attachedRigidbody);
    }

    public void SetRadius(float radius)
    {
        _collider.radius = radius;
    }

    public List<Rigidbody> GetAllContactingObjects()
    {
        return _contactingObjects;
    }
}
