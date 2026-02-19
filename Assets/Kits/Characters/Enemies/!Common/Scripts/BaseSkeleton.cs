using UnityEngine;

[RequireComponent(typeof(Sight2D))]
public class BaseSkeleton : BaseCharacter
{

    //--------- UNITY EDITOR ---------//
    [SerializeField] private float ContactDamage = 0.2f;
    [SerializeField] private float DamageCooldown = 1f;

    //--------- CLASS VARIABLES ---------//
    Sight2D _sight;
    float _nextDamageTime;

    //--------- UNITY METHODS ---------//
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

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player") && Time.time >= _nextDamageTime)
        {
            c.gameObject.GetComponent<Life>()?.OnHitReceived(ContactDamage);
            _nextDamageTime = Time.time + DamageCooldown;
        }
    }
}
