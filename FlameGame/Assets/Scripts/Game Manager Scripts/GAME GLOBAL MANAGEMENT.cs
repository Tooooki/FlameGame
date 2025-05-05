using UnityEngine;
using UnityEngine.UI;

public class GAMEGLOBALMANAGEMENT : MonoBehaviour
{
    //---------------------- Game Management --------------------------------------------------------------------------------
    public int existingEnemiesCount;

    //---------------------- Data Storage -----------------------------------------------------------------------------------
    public GameObject Player;

    //---------------------- Player Stats -----------------------------------------------------------------------------------
    public int playerLevel = 1;                         //Level        
    public float playerCurrentExperience;               //Experience   
    public float playerExperienceToNextLevel = 100f;    //Needed Exp   
    public float playerCurrentHealth;                   //Health       
    public float playerMoveVelocity = 25f;              //Move         Velocity
    public float playerBasicAttackDamage = 25f;         //Basic Attack Damage
    public float playerBasicAttackVelocity;             //Basic Attack Velocity
    public float playerBasicAttackCooldown;             //Basic Attack Cooldown
    public float playerDashVelocity;                    //Dash         Velocity
    public float playerDashCooldown;                    //Dash         Cooldown

    //---------------------- Enemy Stats ------------------------------------------------------------------------------------
    public float enemyRunnerMoveVelocity = 12f;         //Runner  Move   Velocity
    public float enemyRunnerDamage = 20f;               //Runner  Attack Damage
    public float enemyRunnerDashVelocity = 20f;         //Runner  Dash   Velocity
    public float enemyRunnerExperienceDrop = 0f;        //Runner  Drop   Experience
    public float enemyShooterProjectileDamage = 20f;    //Shooter Attack Damage
    public float enemyShooterProjectileVelocity = 20f;  //Shooter Attack Velocity
    public float enemyShooterMoveVelocity = 5f;         //Shooter Move   Velocity
    public float enemyShooterExperienceDrop = 0f;       //Shooter Drop   Experience


    //---------------------- Player Abilities -------------------------------------------------------------------------------






    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        existingEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void PlayerGetExperience(float amount)
    {
        playerCurrentExperience += amount;
    }
}
