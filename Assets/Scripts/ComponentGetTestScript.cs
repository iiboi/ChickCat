using UnityEngine;

public class ComponentGetTestScript : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidbody.useGravity = false;

    }
}
