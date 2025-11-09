using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float Force = 10f;
    public void GiveDamage(Rigidbody pLayerRigidbody, Transform pLayerVisualTransform)
    {
        pLayerRigidbody.AddForce(-pLayerVisualTransform.forward * Force, ForceMode.Impulse);
        HealthManager.Instance.Damage(1);
        Destroy(gameObject);
    }
}
