using System.Collections;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitbox"))
        {
            StopAllCoroutines();
            StartCoroutine(getDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
            StopAllCoroutines();
    }

    private IEnumerator getDamage()
    {
        GAME.PlayerGetDamage(GAME.spikesDamage);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(getDamage());
    }
}
