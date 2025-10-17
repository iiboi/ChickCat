using UnityEngine;

public class GoldWheat : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private float MovementIncreaseSpeed;
    [SerializeField] private float ResetBoostDuration;

    public void Collect()
    {
        PlayerController.SetMovementSpeed(MovementIncreaseSpeed, ResetBoostDuration);
        Destroy(gameObject);
    }
}