using System;
using System.Collections;
using System.IO.Compression;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private RectTransform PlayerWalkingTransform;
    [SerializeField] private RectTransform PlayerDashingTransform;
    [SerializeField] private RectTransform BoosterSpeedTransform;
    [SerializeField] private RectTransform BoosterJumpTransform;
    [SerializeField] private RectTransform BoosterSlowTransform;

    [Header("Images")]

    [SerializeField] private Image goldBoosterWheatImage;
    [SerializeField] private Image holyBoosterWheatImage;
    [SerializeField] private Image rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite PlayerWalkingActiveSprite;
    [SerializeField] private Sprite PlayerWalkingPassiveSprite;
    [SerializeField] private Sprite PlayerDashingActiveSprite;
    [SerializeField] private Sprite PlayerDashingPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;

    public RectTransform GetBoosterSpeedTransform => BoosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => BoosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => BoosterSlowTransform;
    public Image GetGoldBoosterWheatImage => goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => rottenBoosterWheatImage;

    private Image playerWalkingImage;
    private Image playerDashingImage;

    private void Awake()
    {
        playerWalkingImage = PlayerWalkingTransform.GetComponent<Image>();
        playerDashingImage = PlayerDashingTransform.GetComponent<Image>();
    }
    private void Start()
{
        PlayerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        SetStateUserInterfaces(PlayerWalkingActiveSprite, PlayerDashingPassiveSprite, PlayerWalkingTransform, PlayerDashingTransform);
}

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(PlayerWalkingActiveSprite, PlayerDashingPassiveSprite, PlayerWalkingTransform, PlayerDashingTransform);
                //Ustteki Kart Acılacak
                break;
            case PlayerState.DashIdle:
            case PlayerState.Dash:
            SetStateUserInterfaces(PlayerWalkingPassiveSprite, PlayerDashingActiveSprite, PlayerDashingTransform, PlayerWalkingTransform);
                //Alttaki Kart Acılacak
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerDashingSprite,
    RectTransform activeTransform, RectTransform passiveTransform)
    {
        playerWalkingImage.sprite = playerWalkingSprite;
        playerDashingImage.sprite = playerDashingSprite;

        activeTransform.DOAnchorPosX(80f, moveDuration).SetEase(moveEase);
        passiveTransform.DOAnchorPosX(50f, moveDuration).SetEase(moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
    Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(-40f, moveDuration).SetEase(moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(-15f, moveDuration).SetEase(moveEase);
    }
    
    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
    Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite,
        activeWheatSprite, passiveWheatSprite, duration));
    }
}