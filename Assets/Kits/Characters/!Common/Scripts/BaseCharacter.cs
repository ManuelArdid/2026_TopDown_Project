using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public abstract class BaseCharacter : MonoBehaviour, IVisible2D
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] protected float LinearSpeed = 5f;

    [Header("Visibility")]
    [SerializeField] protected int Priority = 0;
    [SerializeField] protected IVisible2D.Side Side = IVisible2D.Side.Neutral;

    //-------- CLASS VARIABLES --------//
    private Rigidbody2D _rb;
    private Vector2 _lastMoveDirection;

    //--------- PROTECTED VARIABLES ---------//
    protected Animator animator;

    //--------- UNITY METHODS ---------//
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("HorizontalVelocity", _lastMoveDirection.x);
        animator.SetFloat("VerticalVelocity", _lastMoveDirection.y);
    }

    //--------- PROTECTED METHODS ---------//
    protected virtual void Move(Vector2 direction)
    {
        _rb.position += LinearSpeed * Time.deltaTime * direction;
        _lastMoveDirection = direction;
    }

    protected internal virtual void NotifyPunch()
    {
        Debug.Log($"{gameObject.name} has been punched!", gameObject);
        Destroy(gameObject);
    }

    public int GetPriority()
    {
        return Priority;
    }

    public IVisible2D.Side GetSide()
    {
        return Side;
    }
}
