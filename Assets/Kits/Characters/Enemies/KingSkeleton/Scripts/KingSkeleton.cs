using UnityEngine;

public class KingSkeleton : BaseSkeleton
{
    //--------- UNITY EDITOR ---------//
    [Header("Melee")]
    [SerializeField] private float MeleeRange = 1.5f;
    [SerializeField] private float MeleeCooldown = 1f;
    
    [Header("Ranged")]
    [SerializeField] private float FireRate = 1.5f;
    [SerializeField] private GameObject ProjectilePrefab;

    //--------- CLASS VARIABLES ---------//
    float _nextFireTime;
    float _nextMeleeTime;

    //--------- UNITY METHODS ---------//
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeRange);
    }
    
    protected override void Update()
    {
        base.Update();

        Transform target = sight.GetClosestTarget();
        if (target == null) return;

        var dist = Vector2.Distance(transform.position, target.position);
        Vector2 dirToTarget = (target.position - transform.position).normalized;

        if (dist <= MeleeRange)
        {
            if (Time.time >= _nextMeleeTime)
            {
                target.GetComponent<Life>()?.OnHitReceived(ContactDamage);
                _nextMeleeTime = Time.time + MeleeCooldown;
            }
        }
        else
        {
            Move(dirToTarget * 0.5f);
            if (Time.time >= _nextFireTime)
            {
                Shoot(dirToTarget);
                _nextFireTime = Time.time + FireRate;
            }
        }
    }

    //--------- PRIVATE METHODS ---------//
    void Shoot(Vector2 dir)
    {
        if (ProjectilePrefab == null) return;
        GameObject proj = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Arrow>()?.Init(dir);
    }
}
