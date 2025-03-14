using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using @Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _radius;
    [SerializeField] private float _strength;
    [SerializeField] private ObjectsDetector _objectsDetector;

    private Color _transparentColor = new(1, 1, 1, 0);

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _objectsDetector.SetRadius(_radius);
    }

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_meshRenderer.material.DOColor(_transparentColor, Random.Range(_minLifeTime, _maxLifeTime)));
        sequence.AppendCallback(Explosion);
    }

    public void Explosion()
    {
        List<Rigidbody> objects = _objectsDetector.GetAllContactingObjects();

        foreach (Rigidbody rigidbody in objects)
            rigidbody.AddExplosionForce(_strength, transform.position, _radius);

        Exploded?.Invoke(this);
        ResetToBase();
    }

    public void ResetToBase()
    {
        _meshRenderer.material.color = Color.black;
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
