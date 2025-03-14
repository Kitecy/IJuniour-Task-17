using System;
using System.Collections;
using UnityEngine;
using @Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private bool _isColorised;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorised)
            return;

        if (collision.transform.GetComponent<Platform>() != null)
        {
            _isColorised = true;
            _meshRenderer.material.color = Random.ColorHSV();
            StartCoroutine(WaitingForDie());
        }
    }

    public void ResetToBase()
    {
        _meshRenderer.material.color = Color.gray;
        _rigidbody.linearVelocity = Vector3.zero;
        _isColorised = false;
    }

    private IEnumerator WaitingForDie()
    {
        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));
        Destroyed?.Invoke(this);
        ResetToBase();
    }
}
