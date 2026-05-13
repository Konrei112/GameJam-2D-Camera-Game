using UnityEngine;
using UnityEngine.SceneManagement;

public class KillFloor : MonoBehaviour
{
    public string Scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
