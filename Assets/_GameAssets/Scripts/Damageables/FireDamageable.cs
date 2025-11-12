using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float Force = 10f;
    public void GiveDamage(Rigidbody pLayerRigidbody, Transform pLayerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        pLayerRigidbody.AddForce(-pLayerVisualTransform.forward * Force, ForceMode.Impulse);
        AudioManager.Instance.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
