using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public abstract class Base : MonoBehaviour
{
    [SerializeField] protected float MinLifeTime;
    [SerializeField] protected float MaxLifeTime;

    protected MeshRenderer MeshRenderer;
    protected Rigidbody RigBody;

    protected virtual void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        RigBody = GetComponent<Rigidbody>();
    }

    protected abstract void ResetToBaseState();
}
