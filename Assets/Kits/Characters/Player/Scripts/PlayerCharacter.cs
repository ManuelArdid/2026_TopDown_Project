using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BaseCharacter
{

    //--------- UNITY EDITOR ---------//
    [SerializeField] InputActionReference MoveInputAction;

    //-------- CLASS VARIABLES --------//
    private Vector2 _rawMove;

    //--------- UNITY METHODS ---------//
    protected override void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        MoveInputAction.action.Enable();

        // Input Callbacks
        MoveInputAction.action.started += OnMove;
        MoveInputAction.action.performed += OnMove;
        MoveInputAction.action.canceled += OnMove;
    }

    protected override void Update()
    {
        base.Update();

        //Movement
        Move(_rawMove);
    }

    void OnDisable()
    {
        MoveInputAction.action.Disable();

        // Input Callbacks
        MoveInputAction.action.started -= OnMove;
        MoveInputAction.action.performed -= OnMove;
        MoveInputAction.action.canceled -= OnMove;
    }

    //--------- PROTECTED METHODS ---------//

    protected void OnMove(InputAction.CallbackContext context)
    {
        _rawMove = context.action.ReadValue<Vector2>();
    }
}
