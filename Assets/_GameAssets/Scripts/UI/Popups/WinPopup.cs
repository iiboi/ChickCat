using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using MaskTransitions;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI TimerUI;
    [SerializeField] private Button OneMoreButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TMP_Text TimerText;

    private void OnEnable() 
    {
        TimerText.text = TimerUI.GetFinalTime();

        OneMoreButton.onClick.AddListener(OneMoreButtonClicked);

        MainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });
    }

    private void OneMoreButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}
