using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;     // Przypisz Audio Mixer
    public Slider volumeSlider;       // Przypisz Slider z UI

    void Start()
    {
        // Odczyt zapisanej głośności lub domyślnie 0 dB
        float savedVolume = PlayerPrefs.GetFloat("SavedVolume", 0f);

        // Ustaw głośność w AudioMixer
        audioMixer.SetFloat("volume", savedVolume);

        // Ustaw pozycję suwaka bez wywoływania onValueChanged
        volumeSlider.SetValueWithoutNotify(savedVolume);

        // Dodaj nasłuchiwanie zmian suwaka
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volumeInDb)
    {
        // Ustaw głośność w mikserze
        audioMixer.SetFloat("volume", volumeInDb);

        // Zapisz wartość
        PlayerPrefs.SetFloat("SavedVolume", volumeInDb);
        PlayerPrefs.Save();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
