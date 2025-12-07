using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip step;
    public AudioClip doorShut;
    public AudioClip toTheNextRoom;
    public AudioClip enemyChargingWater;
    public AudioClip enemyShootingWater;
    public AudioClip enemyShooterHittingPlayer;
    public AudioClip mainCharShootingFire;
    public AudioClip fireballHitWall;
    public AudioClip cardsShowing;
    public AudioClip cardChoosed;
    public AudioClip mcGettingHit;
    public AudioClip playerDash;
    public AudioClip slimeMoving;
    public AudioClip slimeDying;
    public AudioClip slimeAttack;
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
