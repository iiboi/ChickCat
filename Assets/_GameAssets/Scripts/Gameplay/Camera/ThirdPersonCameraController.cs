using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform OrientationTransform;
    [SerializeField] private Transform PlayerVisualTransform;

    [Header("Settings")]
    [SerializeField, Range(0f, 20f)] private float RotationSpeed; 

    private void Update()
    {
        Vector3 viewDirection = PlayerTransform.position - new Vector3(transform.position.x, PlayerTransform.position.y, transform.position.z);

        OrientationTransform.forward = viewDirection.normalized;

        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        Vector3 InputDirection = OrientationTransform.forward * VerticalInput + OrientationTransform.right * HorizontalInput;

        if (InputDirection != Vector3.zero)
        {
           PlayerVisualTransform.forward = Vector3.Slerp(PlayerVisualTransform.forward, InputDirection.normalized, Time.deltaTime * RotationSpeed); 
        }
    }

    
}
