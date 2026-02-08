using UnityEngine;

public class Sight2D : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] float Radius = 5f;
    [Space]
    [SerializeField] IVisible2D.Side[] VisibleSides;

    //-------- CLASS VARIABLES --------//
    private float _lastCheckTime;
    private float _distanceToClosestTarget;
    private float _priorityOfClosestTarget = -1;

    private Collider2D[] _colliders;

    private Transform _closestTarget;

    //--------- UNITY METHODS ---------//
    void Update()
    {
        if ((Time.time - _lastCheckTime) > (1f / _lastCheckTime))
        {
            _lastCheckTime = Time.time;
        }

        _colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        _distanceToClosestTarget = Mathf.Infinity;
        _closestTarget = null;
        _priorityOfClosestTarget = -1;
        for (int i = 0; i < _colliders.Length; i++)
        {
            IVisible2D visible = _colliders[i].GetComponent<IVisible2D>();
            if (visible != null && CanSee(visible))
            {
                float distanceToTarget = Vector2.Distance(transform.position, _colliders[i].transform.position);
                if (
                    (visible.GetPriority() > _priorityOfClosestTarget) ||                                               // if the visible has higher priority than the current closest target,
                                                                                                                        // it becomes the new closest target, regardless of the distance to the player

                    ((visible.GetPriority() == _priorityOfClosestTarget) && (distanceToTarget <= _distanceToClosestTarget))  // if the visible has the same priority as the current closest target,
                                                                                                                             // it becomes the new closest target only if it's closer to the player than the
                                                                                                                             // current closest target
                    )
                {
                    _priorityOfClosestTarget = visible.GetPriority();
                    _distanceToClosestTarget = distanceToTarget;
                    _closestTarget = _colliders[i].transform;
                }

            }
            //Debug.Log($"El collider {i} se llama {_colliders[i].name}.", _colliders[i]);
        }
    }

    //--------- PUBLIC METHODS ---------//

    public Transform GetClosestTarget()
    {
        return _closestTarget;
    }

    public bool IsPlayerInSight()
    {
        bool isPlayerInSight = false;

        _colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        for (int i = 0; !isPlayerInSight && (i < _colliders.Length); i++)
        {
            if (_colliders[i].CompareTag("Player"))
                isPlayerInSight = true;
        }
        return isPlayerInSight;
    }

    //--------- PRIVATE METHODS ---------//
    private bool CanSee(IVisible2D visible)
    {
        bool canSee = false;

        for (int i = 0; !canSee && (i < VisibleSides.Length); i++)
        {
            canSee = visible.GetSide() == VisibleSides[i];
        }

        return canSee;
    }
}