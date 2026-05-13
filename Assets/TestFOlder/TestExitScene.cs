using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class TestExitScene : MonoBehaviour
{
    public SceneFader scenefader;

    [Header("Next Scene")]
    public string nextScene;
    public void Awake()
    {
        
    }
    void LoadNextScene()
    {
        if(scenefader != null)
        {
            scenefader.FadeToScene(nextScene);
        } else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

}
