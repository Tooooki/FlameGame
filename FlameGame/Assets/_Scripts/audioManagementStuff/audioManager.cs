using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip step;
    public AudioClip doorShut;
    public AudioClip toTheNextRoom;
    public AudioClip enemyChargingBow;
    public AudioClip enemyShootingBow;
    public AudioClip mainCharShootingFire;
    public AudioClip fireballHitWall;
    public AudioClip cardsShowing;
    public AudioClip cardChoosed;
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
