using System.Collections;
using UnityEngine;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _firstSpawnposition;
    [SerializeField] private Transform _secondSpawnposition;
    [SerializeField] private BombsSpawner _bombsSpawner;

    private WaitForSeconds _delay;
    private bool _isSpawning = true;

    protected override void Awake()
    {
        base.Awake();
        _delay = new(_spawnDelay);
    }

    private void Start()
    {
        StartCoroutine(WaitingForSpawn());
    }

    private void Spawn()
    {
        float x = Random.Range(_firstSpawnposition.position.x, _secondSpawnposition.position.x);
        Vector3 spawnposition = new(x, _firstSpawnposition.position.y, 0);

        Cube cube = Pool.Get();
        cube.Destroyed += OnCubeDestroyed;
        cube.transform.position = spawnposition;

        if (_isSpawning)
            StartCoroutine(WaitingForSpawn());
    }

    private IEnumerator WaitingForSpawn()
    {
        yield return _delay;
        Spawn();
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Destroyed -= OnCubeDestroyed;

        Bomb bomb = _bombsSpawner.GetBomb();
        bomb.transform.position = cube.transform.position;

        Pool.Release(cube);
    }
}
