using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioClip mainMenuTheme;

    public void StartGame()
    {
        SceneManager.LoadScene("1st Level");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");//settings
    }    

    public void QuitGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
        musicSource.PlayOneShot(mainMenuTheme);
    }
    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.PlayOneShot(mainMenuTheme);
        }
    }
}