using UnityEngine;

public class DamageEnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject hpBar;
    
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

        hpBar.transform.localScale = new Vector3(shooterHP / GAME.enemyShooterMaxHealth, 1, 1);
    }

    public void LoseHP(float damage)
    {
        shooterHP -= damage;
        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);
    }

    public void Die()
    {
        GAME.PlayerGetExperience(GAME.enemyShooterExperienceDrop);
        Destroy(this.gameObject);
    }

}
