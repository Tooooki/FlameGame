using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapBehaviour : MonoBehaviour
{
    [SerializeField] BoxCollider2D trigger, hitbox, push;

    private HashSet<GameObject> actorsOnHitbox;

    public bool trapActive;

    Animator animator;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        animator = GetComponent<Animator>();

        actorsOnHitbox = new HashSet<GameObject>();

        trapActive = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox") || collision.CompareTag("EnemyHitbox"))
        {
            actorsOnHitbox.Add(collision.transform.parent.gameObject);
        }

        if (!trapActive) return;

        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            StartCoroutine(ActivateTrap());
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
        trapActive = false;
        
        yield return new WaitForSeconds(0.3f);

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

        GetComponent<Animator>().SetTrigger("Show");
        push.enabled = true;
        actorsOnHitbox.Clear();

        yield return new WaitForSeconds(2f);

        GetComponent<Animator>().SetTrigger("Hide");
        push.enabled = false;
        trapActive = true;
    }


}
