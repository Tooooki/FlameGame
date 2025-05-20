using System.Collections;
using UnityEngine;

public class AssassinBehaviour : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    public bool canMove;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        canMove = true;
        StartCoroutine(Invisibility());
    }
    void Update()
    {
        if(canMove)
        {
            GetComponent<Rigidbody2D>().linearVelocity = (GAME.Player.transform.position - transform.position).normalized * 1f;
        }
    }

    private IEnumerator Invisibility()
    {
        float timer = 20f;

        var alpha = GetComponentInChildren<SpriteRenderer>().color.a;

        while (timer > 0)
        {
            alpha = Mathf.Lerp(alpha, 0, timer * Time.deltaTime);
        }
        
        yield return null;
    }
}
