using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAMEGLOBALMANAGEMENT : MonoBehaviour
{
    //---------------------- Game Management --------------------------------------------------------------------------------
    public int existingEnemiesCount;
    public float globalDifficultyMultiplyer;

    //---------------------- Data Storage -----------------------------------------------------------------------------------
    public GameObject Player;
    public audioManager audioManager;
    public List<GameObject> enemyPrefabs;

    //---------------------- Player Stats -----------------------------------------------------------------------------------
    public int playerLevel;                             //Level         Current
    public float playerCurrentExperience;               //Experience    Current
    public float playerExperienceToNextLevel;           //Experience    Max  

    public float playerCurrentHealth;                   //Health        Current
    public float playerMaxHealth;                       //Health        Max

    public float playerMoveVelocity;                    //Move          Velocity

    public float playerBasicAttackDamage;               //Basic Attack  Damage
    public float playerBasicAttackVelocity;             //Basic Attack  Velocity
    public float playerBasicAttackCooldown;             //Basic Attack  Cooldown

    public float playerDashVelocity;                    //Dash          Velocity
    public float playerDashCooldown;                    //Dash          Cooldown
    public float playerDashDuration;                    //Dash          Duration

    //---------------------- Enemy Stats ------------------------------------------------------------------------------------
    public float enemyRunnerMoveVelocity;               //Runner  Move   Velocity
    public float enemyRunnerDamage;                     //Runner  Attack Damage
    public float enemyRunnerDashVelocity;               //Runner  Dash   Velocity
    public float enemyRunnerDashDuration;               //Runner  Dash   Duration
    public float enemyRunnerExperienceDrop;             //Runner  Drop   Experience
    public float enemyRunnerMaxHealth;                  //Runner  Health Max

    public float enemyShooterProjectileDamage;          //Shooter Attack Damage
    public float enemyShooterProjectileVelocity;        //Shooter Attack Velocity
    public float enemyShooterMoveVelocity;              //Shooter Move   Velocity
    public float enemyShooterExperienceDrop;            //Shooter Drop   Experience
    public float enemyShooterMaxHealth;                 //Shooter Health Max

    public float enemyAssassinMaxHealth;
    public float enemyAssassinDamage;
    public float enemyAssassinExperienceDrop;
    public float enemyAssassinMoveVelocity;
    public float enemyAssassinDetectionDistance;
    public float enemyAssassinAttackDelay;

    //---------------------- Other Stats ------------------------------------------------------------------------------------
    public float spikesDamage;

    public float waxPotionHealingValue;
    public float expCrystalExperienceValue;



    //---------------------- Player Abilities -------------------------------------------------------------------------------
    public bool dashAbility = true;                     //Dash






    private void Start()
    {
        //Reset values of stats
        globalDifficultyMultiplyer = 1f;
        playerLevel = 1;
        playerCurrentExperience = 0f;
        playerExperienceToNextLevel = 100f;

        playerCurrentHealth = 0f;
        playerMaxHealth = 100f;

        playerMoveVelocity = 10f;

        playerBasicAttackDamage = 25f;
        playerBasicAttackVelocity = 30f;
        playerBasicAttackCooldown = 0.8f;

        playerDashVelocity = 50f;
        playerDashCooldown = 6f;
        playerDashDuration = 0.1f;

        enemyRunnerMoveVelocity = 12f;
        enemyRunnerDamage = 20f;
        enemyRunnerDashVelocity = 40f;
        enemyRunnerExperienceDrop = 20f;
        enemyRunnerDashDuration = 0.3f;

        enemyShooterProjectileDamage = 20f;
        enemyShooterProjectileVelocity = 20f;
        enemyShooterMoveVelocity = 5f;
        enemyShooterExperienceDrop = 30f;
        enemyShooterMaxHealth = 75f;
        enemyRunnerMaxHealth = 100f;

        enemyAssassinMaxHealth = 60f;
        enemyAssassinDamage = 40f;
        enemyAssassinExperienceDrop = 50f;
        enemyAssassinMoveVelocity = 4f;
        enemyAssassinDetectionDistance = 10f;
        enemyAssassinAttackDelay = 1f;

        spikesDamage = 50f;

        waxPotionHealingValue = 30f;
        expCrystalExperienceValue = 50f;
}
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    void Update()
    {
        existingEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void PlayerGetExperience(float amount)
    {
        playerCurrentExperience += amount;
    }

    public void PlayerGetDamage(float damage)
    {
        playerCurrentHealth -= damage;

        Player.GetComponentInChildren<PlayerDeath>().DamageResult();
    }
}
