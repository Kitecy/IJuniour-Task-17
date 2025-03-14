public class BombsSpawner : Spawner<Bomb>
{
    protected override void OnGetObject(Bomb obj)
    {
        base.OnGetObject(obj);
        obj.Exploded += OnBombExploded;
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
