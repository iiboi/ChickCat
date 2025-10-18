using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO WheatDesignSO;
    [SerializeField] private PlayerController PlayerController;


    public void Collect()
    {
        PlayerController.SetJumpForce(WheatDesignSO.IncreaseDecreaseMultiplier, WheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
