using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class BaseCharacter : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] protected float LinearSpeed = 5f;

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
}
