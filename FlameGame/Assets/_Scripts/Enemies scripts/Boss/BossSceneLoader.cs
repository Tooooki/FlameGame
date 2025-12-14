using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossSceneLoader : MonoBehaviour
{
    public static BossSceneLoader Instance;

    [Header("Fade Settings")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 3f;

    private void Awake()
    {
        transform.SetParent(null);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenuWithFade()
    {
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        SceneManager.LoadScene("Main Menu");
    }
}