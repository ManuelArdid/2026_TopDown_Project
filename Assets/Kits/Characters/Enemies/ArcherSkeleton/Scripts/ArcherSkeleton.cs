using UnityEngine;

public class ArcherSkeleton : BaseSkeleton
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] private float PreferredDistance = 5f;
    [SerializeField] private float FireRate = 2f;
    [SerializeField] private GameObject ProjectilePrefab;
    
    //--------- CLASS VARIABLES ---------//
    float _nextFireTime;
    
    //--------- UNITY METHODS ---------//
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PreferredDistance);
    }
    
    protected override void Update()
    {
        base.Update();
        
        Transform target = sight.GetClosestTarget();
        if (target == null) return;

        var dist = Vector2.Distance(transform.position, target.position);
        Vector2 dirToTarget = (target.position - transform.position).normalized;

        if      (dist > PreferredDistance + 0.5f) Move(dirToTarget);
        else if (dist < PreferredDistance - 0.5f) Move(-dirToTarget);

        if (Time.time >= _nextFireTime)
        {
            Shoot(dirToTarget);
            _nextFireTime = Time.time + FireRate;
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
