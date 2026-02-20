using UnityEngine;

public class Arrow : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] private float Speed = 6f;
    [SerializeField] private float Damage = 0.15f;
    [SerializeField] private float Lifetime = 4f;

    //--------- CLASS VARIABLES ---------//
    Vector2 _direction;

    //--------- UNITY METHODS ---------//
    void Update()
    {
        transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            c.GetComponent<Life>()?.OnHitReceived(Damage);
        }
        Destroy(gameObject);
    }

    //--------- PUBLIC METHODS ---------//
    public void Init(Vector2 dir)
    {
        _direction = dir.normalized;
        Destroy(gameObject, Lifetime);
    }
}
