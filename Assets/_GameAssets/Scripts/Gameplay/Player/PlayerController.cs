using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;


    [Header("References")]
    [SerializeField] private Transform OrientationTransform;

    [Header("Movement Settings")]

    [SerializeField] private KeyCode MovementKey;
    [SerializeField, Range(0f, 100f)] private float MovementSpeed;

    [Header("Jump Settings")]

    [SerializeField] private KeyCode Jumpkey;
    [SerializeField, Range(0f, 20f)] private float JumpForce;
    [SerializeField] private float JumpCoolDown;
    [SerializeField] private float AirMultiplier;
    [SerializeField] private float AirDrag;
    [SerializeField] private bool Canjump;

    [Header("Dash Settings")]

    [SerializeField] private KeyCode DashKey;
    [SerializeField] private float DashMultiplier;
    [SerializeField] private float DashDrag;

    [Header("Ground Check Settings")]

    [SerializeField] private float PlayerHeight;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float GroundDrag;

    private StateController StateController;

    private Rigidbody PlayerRigidbody;

    private float HorizontalInput, VerticalInput;
    private Vector3 MovementDirection;

    private bool IsDashing;
    private void Awake()
    {
        StateController = GetComponent<StateController>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerRigidbody.freezeRotation = true;

    }
    private void Update()
    {
        Setinputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void Setinputs()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(DashKey))
        {
            IsDashing = true;
        }
        else if (Input.GetKeyDown(MovementKey))
        {
            IsDashing = false;
        }

        else if (Input.GetKey(Jumpkey) && Canjump && IsGrounded())
        {
            Canjump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), JumpCoolDown);
        }

    }

    private void SetStates()
    {
        var MovementDirection = GetMovementDirection();
        var Isgrounded = IsGrounded();
        var CurrentState = StateController.GetCurrentState();
        var Isdashing = isdashing();

        var NewState = CurrentState switch
        {
            _ when MovementDirection == Vector3.zero && Isgrounded && !IsDashing => PlayerState.Idle,
            _ when MovementDirection != Vector3.zero && Isgrounded && !IsDashing => PlayerState.Move,
            _ when MovementDirection != Vector3.zero && Isgrounded && IsDashing => PlayerState.Dash,
            _ when MovementDirection == Vector3.zero && Isgrounded && IsDashing => PlayerState.DashIdle,
            _ when !Canjump && !Isgrounded => PlayerState.Jump,
            _ => CurrentState,
        };

        if (NewState != CurrentState)
        {
            StateController.ChangeState(NewState);
        }
    }


    private void SetPlayerMovement()
    {
        MovementDirection = OrientationTransform.forward * VerticalInput + OrientationTransform.right * HorizontalInput;

        float ForceMultiplier = StateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Dash => DashMultiplier,
            PlayerState.Jump => AirMultiplier,
            _ => 1f
        };

        PlayerRigidbody.AddForce(MovementDirection.normalized * MovementSpeed * ForceMultiplier, ForceMode.Force);

    }

    private void SetPlayerDrag()
    {
        PlayerRigidbody.linearDamping = StateController.GetCurrentState() switch
        {
            PlayerState.Move => GroundDrag,
            PlayerState.Dash => DashDrag,
            PlayerState.Jump => AirDrag,
            _ => PlayerRigidbody.linearDamping
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatvelocity = new Vector3(PlayerRigidbody.linearVelocity.x, 0f, PlayerRigidbody.linearVelocity.z);
        if (flatvelocity.magnitude > MovementSpeed)
        {
            Vector3 Limitedvelocity = flatvelocity.normalized * MovementSpeed;
            PlayerRigidbody.linearVelocity = new Vector3(Limitedvelocity.x, PlayerRigidbody.linearVelocity.y, Limitedvelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();
        PlayerRigidbody.linearVelocity = new Vector3(PlayerRigidbody.linearVelocity.x, 0f, PlayerRigidbody.linearVelocity.z);
        PlayerRigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        Canjump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, GroundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return MovementDirection.normalized;
    }

    private bool isdashing()
    {
        return IsDashing;
    }
}
