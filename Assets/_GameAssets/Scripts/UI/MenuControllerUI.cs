using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    private void Awake() 
    {
        PlayButton.onClick.AddListener(() =>
        {
        AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
        });

        QuitButton.onClick.AddListener(() =>
        {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
            Debug.Log("Quitting The Game!");
            Application.Quit();
        });
    }
}
