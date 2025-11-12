using UnityEngine;
using UnityEngine.UI;

public class GoldWheat : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO WheatDesignSO;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private PlayerStateUI PlayerStateUI;

    private RectTransform PlayerBoosterTransform;
    private Image PlayerBoosterImage;

    void Awake()
    {
        PlayerBoosterTransform = PlayerStateUI.GetBoosterSpeedTransform;
        PlayerBoosterImage = PlayerBoosterTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        PlayerController.SetMovementSpeed(WheatDesignSO.IncreaseDecreaseMultiplier, WheatDesignSO.ResetBoostDuration);

        PlayerStateUI.PlayBoosterUIAnimations(PlayerBoosterTransform, PlayerBoosterImage, PlayerStateUI.GetGoldBoosterWheatImage,
          WheatDesignSO.ActiveSprite, WheatDesignSO.PassiveSprite, WheatDesignSO.ActiveWheatSprite, WheatDesignSO.PassiveWheatSprite, WheatDesignSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        Destroy(gameObject);
    }
}