using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    //[SerializeField] float checkFrecuency = 5f;


    private float lastCheckTime;
    private Transform _closestPlayer;
    private float _distanceToClosestPlayer;

    Collider2D[] colliders;
    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastCheckTime) > (1f / lastCheckTime))
        {
            lastCheckTime = Time.time;
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        _distanceToClosestPlayer = Mathf.Infinity;
        _closestPlayer = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                float distanceToPlayer = Vector2.Distance(transform.position, colliders[i].transform.position);
                if (distanceToPlayer < _distanceToClosestPlayer)
                {
                    _distanceToClosestPlayer = distanceToPlayer;
                    _closestPlayer = colliders[i].transform;
                }
            }
            Debug.Log($"El collider {i} se llama {colliders[i].name}.", colliders[i]);
        }
    }

    public Transform GetClosestTarget()
    {
        return _closestPlayer;
    }

    public bool IsPlayerInSight()
    {
        bool isPlayerInSight = false;

        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; !isPlayerInSight && (i < colliders.Length); i++)
        {
            if (colliders[i].CompareTag("Player"))
            { isPlayerInSight = true; }
        }
        return isPlayerInSight;
    }
}