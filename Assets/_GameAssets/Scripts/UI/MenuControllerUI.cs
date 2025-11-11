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
            SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
        });

        QuitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quitting The Game!");
            Application.Quit();
        });
    }
}
