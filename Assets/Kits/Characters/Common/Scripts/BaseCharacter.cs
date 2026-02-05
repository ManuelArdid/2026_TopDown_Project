using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseCharacter : MonoBehaviour
{

    //--------- UNITY EDITOR ---------//

    //-------- CLASS VARIABLES --------//

    Rigidbody2D _rb;

    //--------- UNITY METHODS ---------//
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //--------- PROTECTED METHODS ---------//
    protected void Move(Vector2 movement)
    {
        _rb.position += movement;
    }
}
