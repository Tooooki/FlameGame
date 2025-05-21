using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapBehaviour : MonoBehaviour
{
    [SerializeField] BoxCollider2D trigger, hitbox, push;
    [SerializeField] GameObject hiddenSprite, activeSprite;

    public List<GameObject> actorsOnHitbox;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        actorsOnHitbox = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            trigger.enabled = false;
            StartCoroutine(ActivateTrap());
        }

        if(collision.CompareTag("PlayerHitbox") || collision.CompareTag("EnemyHitbox"))
        {
            actorsOnHitbox.Add(collision.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox") || collision.CompareTag("EnemyHitbox"))
        {
            actorsOnHitbox.Remove(collision.transform.parent.gameObject);
        }
    }

    private IEnumerator ActivateTrap()
    {
        yield return new WaitForSeconds(1f);

        foreach(GameObject target in actorsOnHitbox)
        {
            if (target.CompareTag("Player"))
                GAME.PlayerGetDamage(GAME.spikesDamage); //Apply damage to Player
            else if (target.GetComponent<EnemyCrosshair>() != null)
                target.GetComponent<EnemyCrosshair>().GetDamage(GAME.spikesDamage); // Apply damage to Runner
            else if (target.GetComponent<DamageEnemyShooter>() != null)
                target.GetComponent<DamageEnemyShooter>().LoseHP(GAME.spikesDamage); // Apply damage to Shooter
            else if (target.GetComponent<AssassinDamage>() != null)
                target.GetComponent<AssassinDamage>().GetDamage(GAME.spikesDamage); //Apply damage to Assassin
        }

        push.enabled = true;
        hiddenSprite.SetActive(false);
        activeSprite.SetActive(true);

        yield return new WaitForSeconds(2f);

        push.enabled = false;
        hiddenSprite.SetActive(true);
        activeSprite.SetActive(false);

        yield return new WaitForSeconds(1f);

        trigger.enabled = true;
    }

    
}
