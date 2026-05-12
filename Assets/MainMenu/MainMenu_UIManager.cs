using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UIManager : MonoBehaviour
{
    public GameObject settingsPanel;

    [Header("Next Scene")] // Handles the Scene Transition
    public SceneFader scenefader;
    public string nextScene;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartGame()
    {
        LoadNextScene();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void LoadNextScene()
    {
        if (scenefader != null)
        {
            scenefader.FadeToScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
