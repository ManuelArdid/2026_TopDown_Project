using UnityEngine;

[RequireComponent(typeof(Sight2D))]
public class BaseSkeleton : BaseCharacter
{

    //--------- UNITY EDITOR ---------//

    //--------- CLASS VARIABLES ---------//

    Sight2D _sight;
    EnemySpawner spawner;

    //--------- UNITY METHODS ---------//
    public void Init(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }
    public void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnEnemyDied(transform.position);
        }

        Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        _sight = GetComponent<Sight2D>();
    }

    protected override void Update()
    {
        base.Update();
        Transform closestTarget = _sight.GetClosestTarget();
        if (closestTarget != null)
        {
            Move((closestTarget.position - transform.position).normalized);
        }
    }
}
