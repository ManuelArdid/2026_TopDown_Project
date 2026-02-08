using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class BaseCharacter : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] protected float LinearSpeed = 5f;

    //-------- CLASS VARIABLES --------//
    Rigidbody2D _rb;
    Animator _animator;

    //--------- UNITY METHODS ---------//
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
    }

    //--------- PROTECTED METHODS ---------//
    protected virtual void Move(Vector2 direction)
    {
        _rb.position += LinearSpeed * Time.deltaTime * direction;
    }
}
