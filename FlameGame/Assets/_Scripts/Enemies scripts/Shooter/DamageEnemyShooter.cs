using UnityEngine;

public class DamageEnemyShooter : MonoBehaviour
{
    public float shooterHP;

    GAMEGLOBALMANAGEMENT GAME;


    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        shooterHP = GAME.enemyShooterMaxHealth;
    }

    void Update()
    {
        if (shooterHP <= 0)
        {
            Die();
        }
    }

    public void LoseHP(float damage)
    {
        shooterHP -= damage;
        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);
    }

    public void Die()
    {
        GAME.Player.GetComponent<PlayerInRooms>().isCameraShaking = false;
        GAME.PlayerGetExperience(GAME.enemyShooterExperienceDrop);
        Destroy(this.gameObject);
    }

}
