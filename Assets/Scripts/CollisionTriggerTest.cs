using UnityEngine;

public class CollisionTriggerTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
    }
}
