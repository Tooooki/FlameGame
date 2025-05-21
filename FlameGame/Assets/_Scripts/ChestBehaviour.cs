using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.Find("Sprite").gameObject.SetActive(false);
            gameObject.transform.Find("Particle System").gameObject.SetActive(true);
            GetComponent<ShadowCaster2D>().enabled = false;

            Destroy(gameObject, 0.4f);
        }
    }
}
