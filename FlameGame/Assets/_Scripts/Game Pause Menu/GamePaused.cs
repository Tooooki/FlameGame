using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePaused : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;


    void Start()
    {
        pauseMenu.SetActive(false);
    
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    { 
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
