using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private float ForceIncrease;
    [SerializeField] private float ResetBoostDuration;

    public void Collect()
    {
        PlayerController.SetJumpForce(ForceIncrease, ResetBoostDuration);
        Destroy(gameObject);
    }
}
