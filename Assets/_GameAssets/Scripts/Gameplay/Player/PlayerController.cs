using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform OrientationTransform;

    [Header("Movement Settings")]

    [SerializeField] private KeyCode MovementKey;
    [SerializeField, Range(0f, 100f)] private float MovementSpeed;

    [Header("Jump Settings")]

    [SerializeField] private KeyCode Jumpkey;
    [SerializeField, Range(0f, 20f)] private float JumpForce;

    [SerializeField] private float JumpCoolDown;

    [SerializeField] private bool Canjump;

    [Header("Dash Settings")]

    [SerializeField] private KeyCode DashKey;
    [SerializeField] private float DashMultiplier;

    [SerializeField] private float DashDrag;

    [Header("Ground Check Settings")]

    [SerializeField] private float PlayerHeight;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float GroundDrag;

    private Rigidbody PlayerRigidbody;

    private float HorizontalInput, VerticalInput;
    private Vector3 MovementDirection;

    private bool IsDashing;
    private void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerRigidbody.freezeRotation = true;

    }
    private void Update()
    {
        Setinputs();
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
            Debug.Log("Player Dashing");
        }
        else if (Input.GetKeyDown(MovementKey))
        {
            IsDashing = false;
            Debug.Log("Player Moving");
        }

        else if (Input.GetKey(Jumpkey) && Canjump && IsGrounded())
        {
            Canjump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), JumpCoolDown);
        }

    }

    private void SetPlayerMovement()
    {
        MovementDirection = OrientationTransform.forward * VerticalInput + OrientationTransform.right * HorizontalInput;

        if (IsDashing)
        {
            PlayerRigidbody.AddForce(MovementDirection.normalized * MovementSpeed * DashMultiplier, ForceMode.Force);
        }
        else
        {
            PlayerRigidbody.AddForce(MovementDirection.normalized * MovementSpeed, ForceMode.Force);
        }
        

    }

    private void SetPlayerDrag()
    {
        if (IsDashing)
        {
            PlayerRigidbody.linearDamping = DashDrag;
        }
        else
        {
            PlayerRigidbody.linearDamping = GroundDrag;
        }
        
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
}
