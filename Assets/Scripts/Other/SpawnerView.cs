using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TMP_Text _activeObjectsCountText;
    [SerializeField] private TMP_Text _createdObjectsCountText;
    [SerializeField] private TMP_Text _spawnedObjectsCountText;

    private void OnEnable()
    {
        _spawner.InfoChanged += OnInfoChanged;
    }

    private void OnDisable()
    {
        _spawner.InfoChanged -= OnInfoChanged;
    }

    private void OnInfoChanged()
    {
        _activeObjectsCountText.text = _spawner.ActiveObjectsCount.ToString();
        _createdObjectsCountText.text = _spawner.CreatedObjectsCount.ToString();
        _spawnedObjectsCountText.text = _spawner.SpawnedObjectCount.ToString();
    }
}
