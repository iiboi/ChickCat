using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform OrientationTransform;
    [SerializeField] private float MovementSpeed;

    private Rigidbody PlayerRigidbody;

    private float HorizontalInput, VerticalInput;
    private Vector3 MovementDirection;
    private void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerRigidbody.freezeRotation = true;

    }
    private void Update()
    {
        Setinputs();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void Setinputs()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SetPlayerMovement()
    {
        MovementDirection = OrientationTransform.forward * VerticalInput + OrientationTransform.right * HorizontalInput;
        PlayerRigidbody.AddForce(MovementDirection * MovementSpeed, ForceMode.Force);

    }

}
