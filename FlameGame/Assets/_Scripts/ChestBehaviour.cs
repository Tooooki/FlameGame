using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public List<GameObject> AvalibleLoot;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack"))
        {
            int drop = Random.Range(0, AvalibleLoot.Count);
            Instantiate(AvalibleLoot[drop], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
