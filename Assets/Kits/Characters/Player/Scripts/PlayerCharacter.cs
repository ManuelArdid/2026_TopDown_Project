using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Life))]
public class PlayerCharacter : BaseCharacter
{

    //--------- UNITY EDITOR ---------//

    [Header("Input")]
    [SerializeField] InputActionReference MoveInputAction;
    [SerializeField] InputActionReference PunchInputAction;

    [Header("Combat")]
    [SerializeField] float PunchRadius = 0.3f;
    [SerializeField] float PunchRange = 1f;


    //-------- CLASS VARIABLES --------//
    private Vector2 _rawMove;
    private Vector2 _punchDirection = Vector2.down;

    private Life _life;

    private bool _mustPunch;

    //--------- UNITY METHODS ---------//
    protected override void Awake()
    {
        base.Awake();
        _life = GetComponent<Life>();
    }

    void OnEnable()
    {
        MoveInputAction.action.Enable();
        PunchInputAction.action.Enable();

        // Input Callbacks
        MoveInputAction.action.started += OnMove;
        MoveInputAction.action.performed += OnMove;
        MoveInputAction.action.canceled += OnMove;

        PunchInputAction.action.performed += OnPunch;
    }

    protected override void Update()
    {
        base.Update();

        //Movement
        Move(_rawMove);

        //Combat
        if (_mustPunch)
        {
            PerformPunch();
            _mustPunch = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Drop drop = collision.GetComponent<Drop>();
        if (drop)
        {
            _life.RecoverHealth(drop.DropDefinition.HealthRecovery);
            drop.NotifyPickUp();
        }
    }

    void OnDisable()
    {
        MoveInputAction.action.Disable();
        PunchInputAction.action.Disable();

        // Input Callbacks
        MoveInputAction.action.started -= OnMove;
        MoveInputAction.action.performed -= OnMove;
        MoveInputAction.action.canceled -= OnMove;

        PunchInputAction.action.performed -= OnPunch;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _punchDirection * PunchRange);
    }

    //--------- PUBLIC METHODS ---------//

    public void NotifyInventoryItemUsed(InventoryItemDefinition itemDefinition)
    {
        _life.RecoverHealth(itemDefinition.HealthRecovery);
    }

    //--------- PROTECTED METHODS ---------//

    protected void OnMove(InputAction.CallbackContext context)
    {
        _rawMove = context.action.ReadValue<Vector2>();
        if (_rawMove != Vector2.zero)
        {
            _punchDirection = _rawMove.normalized;
        }
    }

    protected void OnPunch(InputAction.CallbackContext context)
    {
        _mustPunch = true;
    }

    protected void PerformPunch()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, PunchRadius, _punchDirection * PunchRange);

        foreach (RaycastHit2D hit in hits)
        {
            BaseCharacter otherCharacter = hit.collider.GetComponent<BaseCharacter>();
            if (otherCharacter != this)
                otherCharacter.NotifyPunch();
        }
    }
}
