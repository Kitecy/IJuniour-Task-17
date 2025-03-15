using UnityEngine;

public class BombsSpawner : Spawner<Bomb>
{
    [SerializeField] private Exploader _exploader;

    protected override void OnGetObject(Bomb obj)
    {
        base.OnGetObject(obj);
        obj.Exploded += OnBombExploded;
    }

    protected override Bomb CreateObject()
    {
        Bomb bomb = base.CreateObject();
        bomb.SetExploader(_exploader);
        return bomb;
    }

    public Bomb GetBomb()
    {
        return Pool.Get();
    }

    private void OnBombExploded(Bomb bomb)
    {
        bomb.Exploded -= OnBombExploded;
        Pool.Release(bomb);
    }
}
