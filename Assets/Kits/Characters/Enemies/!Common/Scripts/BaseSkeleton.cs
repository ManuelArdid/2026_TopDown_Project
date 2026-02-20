using UnityEngine;

[RequireComponent(typeof(Sight2D))]
public class BaseSkeleton : BaseCharacter
{

    //--------- UNITY EDITOR ---------//
    [SerializeField] protected float ContactDamage = 0.2f;
    [SerializeField] protected float DamageCooldown = 1f;

    //--------- CLASS VARIABLES ---------//
    float _nextDamageTime;

    EnemySpawner spawner;

    //--------- PROTECTED VARIABLES ---------//
    protected Sight2D sight;

    //--------- UNITY METHODS ---------//
    protected override void Awake()
    {
        base.Awake();
        sight = GetComponent<Sight2D>();
    }

    protected override void Update()
    {
        base.Update();
        Transform closestTarget = sight.GetClosestTarget();
        if (closestTarget != null)
        {
            Move((closestTarget.position - transform.position).normalized);
        }
    }

    //--------- PUBLIC METHODS ---------//
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

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player") && Time.time >= _nextDamageTime)
        {
            c.gameObject.GetComponent<Life>()?.OnHitReceived(ContactDamage);
            _nextDamageTime = Time.time + DamageCooldown;
        }
    }
}
