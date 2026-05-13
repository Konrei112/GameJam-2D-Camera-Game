using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [Header("Image")]
    public GameObject fadeImageObject;
    public Image fadeImage;

    public float fadeDuration = 1f;

    private void Awake()
    {
        fadeImageObject = GameObject.FindGameObjectWithTag("FadeImage");
        fadeImage = fadeImageObject.GetComponent<Image>();
    }

    void Start()
    {
        fadeImageObject.SetActive(true);
        StartCoroutine(FadeIn());

    }
    public void FadeToScene(string nextScene)
    {
        Debug.Log("LoadingNextScene");
        StartCoroutine(FadeOut(nextScene));
    }
    IEnumerator FadeIn()
    {
        float time = fadeDuration;
        while (time > 0)
        {
            time -= Time.unscaledDeltaTime;
            SetAlpha(time / fadeDuration);
            yield return null;
        }
        SetAlpha(0);
    }
    IEnumerator FadeOut(string nextScene)
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            SetAlpha(time / fadeDuration);
            yield return null;
        }
        SetAlpha(1);
        SceneManager.LoadScene(nextScene);
    }
    void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
