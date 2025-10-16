using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private float MovementDecreaseSpeed;
    [SerializeField] private float ResetBoostDuration;

    public void Collect()
    {
        PlayerController.SetMovementSpeed(MovementDecreaseSpeed, ResetBoostDuration);
        Destroy(gameObject);
    }
}
