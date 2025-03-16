using System;
using System.Collections;
using UnityEngine;
using @Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : Base
{
    private bool _isColorised;

    public event Action<Cube> Destroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorised)
            return;

        if (collision.transform.GetComponent<Platform>() != null)
        {
            _isColorised = true;
            MeshRenderer.material.color = Random.ColorHSV();
            StartCoroutine(WaitingForDie());
        }
    }

    protected override void ResetToBaseState()
    {
        MeshRenderer.material.color = Color.gray;
        RigBody.linearVelocity = Vector3.zero;
        _isColorised = false;
    }

    private IEnumerator WaitingForDie()
    {
        yield return new WaitForSeconds(Random.Range(MinLifeTime, MaxLifeTime));
        Destroyed?.Invoke(this);
        ResetToBaseState();
    }
}
