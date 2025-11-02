using UnityEngine;

public class RunnerDealDamage : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    EnemyCrosshair enemyAttackScript;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        enemyAttackScript = GetComponentInParent<EnemyCrosshair>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            if (enemyAttackScript.canDamage)
                GAME.audioManager.PlaySFX(GAME.audioManager.mcGettingHit);
            {
                enemyAttackScript.canDamage = false;

                GAME.audioManager.PlaySFX(GAME.audioManager.slimeAttack);

                GAME.PlayerGetDamage(GAME.enemyRunnerDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Clutter") && !enemyAttackScript.isAttacking)
        {
            enemyAttackScript.Attack();
        }

        if (collision.CompareTag("Clutter") && enemyAttackScript.canDamage)
        {
            Destroy(collision.gameObject);
        }
    }
}
