using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
