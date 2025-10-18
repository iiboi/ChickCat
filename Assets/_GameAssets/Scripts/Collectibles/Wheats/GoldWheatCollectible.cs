using UnityEngine;

public class GoldWheat : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO WheatDesignSO; 
    [SerializeField] private PlayerController PlayerController;


    public void Collect()
    {
        PlayerController.SetMovementSpeed(WheatDesignSO.IncreaseDecreaseMultiplier, WheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}