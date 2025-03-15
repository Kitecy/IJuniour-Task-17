using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    protected ObjectPool<T> Pool;

    public event Action InfoChanged;

    public int ActiveObjectsCount => Pool.CountActive;
    public int CreatedObjectsCount { get; private set; }
    public int SpawnedObjectCount { get; private set; }

    protected virtual void Awake()
    {
        Pool = new(CreateObject, OnGetObject, OnReleaseObject);
    }

    protected virtual T CreateObject()
    {
        T obj = Instantiate(_prefab);
        CreatedObjectsCount++;
        InfoChanged?.Invoke();
        return obj;
    }

    protected virtual void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
        SpawnedObjectCount++;
        InfoChanged?.Invoke();
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
        InfoChanged?.Invoke();
    }
}