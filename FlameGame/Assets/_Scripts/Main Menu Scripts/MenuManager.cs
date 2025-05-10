using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1st Level"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}