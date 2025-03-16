using DG.Tweening;
using System;
using UnityEngine;
using @Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Bomb : Base
{
    [SerializeField] private float _radius;
    [SerializeField] private float _strength;
    [SerializeField] private ObjectsDetector _objectsDetector;
    [SerializeField] private Exploader _exploader;

    private Color _transparentColor = new(1, 1, 1, 0);

    public event Action<Bomb> Exploded;

    protected override void Awake()
    {
        base.Awake();
        _objectsDetector.SetRadius(_radius);
    }

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(MeshRenderer.material.DOColor(_transparentColor, Random.Range(MinLifeTime, MaxLifeTime)));
        sequence.AppendCallback(Exploade);
    }

    protected override void ResetToBaseState()
    {
        MeshRenderer.material.color = Color.black;
        RigBody.linearVelocity = Vector3.zero;
    }

    public void SetExploader(Exploader exploader)
    {
        _exploader = exploader;
    }

    public void Exploade()
    {
        _exploader.Explode(transform.position, _objectsDetector.ContactingObjects, _strength, _radius);
        Exploded?.Invoke(this);
        ResetToBaseState();
    }
}
