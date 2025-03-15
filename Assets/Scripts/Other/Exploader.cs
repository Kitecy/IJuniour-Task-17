using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    public void Explode(Vector3 position, IReadOnlyList<Rigidbody> objects, float strength, float radius)
    {
        foreach (Rigidbody rigidbody in objects)
            rigidbody.AddExplosionForce(strength, transform.position, radius);
    }
}
