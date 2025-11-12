using TMPro;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform PlayerVisualTransform;
    private PlayerController PlayerController;
    private Rigidbody PlayerRigidBody;
    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerRigidBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.boost(PlayerController);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(PlayerRigidBody, PlayerVisualTransform);
            CameraShake.Instance.ShakeCamera(0.7f, 0.5f);
        }
    }
}
