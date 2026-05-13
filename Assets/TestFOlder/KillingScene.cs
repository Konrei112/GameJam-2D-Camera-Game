using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class KillingScene : MonoBehaviour
{
    public SceneFader scenefader;

    [Header("Next Scene")]
    public string nextScene;
    public void Awake()
    {
        
        scenefader = FindFirstObjectByType<SceneFader>();
    }
    void ReloadNextScene()
    {
        if(scenefader != null)
        {
            scenefader.FadeToScene(nextScene);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("STEP 20000000|||||||Touching Player");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Touching Player");
            ReloadNextScene();
        }
    }

}
