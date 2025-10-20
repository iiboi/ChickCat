using TMPro;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private PlayerController PlayerController;
    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
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
        if(other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.boost(PlayerController);
        }
    }
}
