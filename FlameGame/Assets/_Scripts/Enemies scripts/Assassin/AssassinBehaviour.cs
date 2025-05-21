using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBehaviour : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    public bool canMove, canAttack, goingInvisible, invisible, visible;

    private Coroutine ChangeAlpha;

    public HashSet<GameObject> objectsInsideTrigger;

    CircleCollider2D attackDetection, visibleDetection;
    BoxCollider2D boxC;
    Rigidbody2D rb;
    SpriteRenderer sr;





    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        attackDetection = GetComponentInChildren<CircleCollider2D>();
        visibleDetection = GetComponent<CircleCollider2D>();
        boxC = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        canMove = true;
        canAttack = true;
        objectsInsideTrigger = new HashSet<GameObject>();
    }

    void Update()
    {
        if (canMove)
        {
            rb.linearVelocity = (GAME.Player.transform.position - transform.position).normalized * GAME.enemyAssassinMoveVelocity;
        }

        if (visible && canAttack)
            boxC.enabled = true;
        else
            boxC.enabled = false;


        if (!objectsInsideTrigger.Contains(GAME.Player) && !goingInvisible)
        {
            if (ChangeAlpha != null)
                StopCoroutine(ChangeAlpha);

            goingInvisible = true;
            ChangeAlpha = StartCoroutine(Invisibility(1f));
        }
        else if (objectsInsideTrigger.Contains(GAME.Player))
        {
            if (ChangeAlpha != null)
            {
                StopCoroutine(ChangeAlpha);
                goingInvisible = false;
            }

            ChangeAlpha = StartCoroutine(Visibility(2f));
        }

        visibleDetection.radius = GAME.enemyAssassinDetectionDistance;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectsInsideTrigger.Add(collision.gameObject);
        }

        if (collision.CompareTag("PlayerHitbox"))
        {
            if (canAttack)
            {
                canMove = false;

                StartCoroutine(Attack(collision.transform.parent.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectsInsideTrigger.Remove(collision.gameObject);
        }
    }








    private IEnumerator Invisibility(float duration)
    {
        visible = false;

        float timer = 0f;

        float startAlpha = sr.color.a;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, timer / duration);
            sr.color = new Color(1, 1, 1, newAlpha);
            yield return null;
        }
        invisible = true;
        goingInvisible = false;
    }

    private IEnumerator Visibility(float duration)
    {
        invisible = false;

        float timer = 0f;

        float startAlpha = sr.color.a;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1, timer / duration);
            sr.color = new Color(1, 1, 1, newAlpha);
            yield return null;
        }

        visible = true;
    }

    private IEnumerator Attack(GameObject target)
    {
        canAttack = false; //make sure that there is only one attack active at the time

        Vector3 direction = (target.transform.position - transform.position).normalized; //set attack direction

        yield return new WaitForSeconds(GAME.enemyAssassinAttackDelay); //wait before attacking

        float timer = 0f; 

        while (timer < 0.1f) //for 0.1 s dashes in previously set direction
        {
            timer += Time.deltaTime;

            rb.linearVelocity = direction * 120f;

            yield return null;
        }

        rb.linearVelocity = Vector3.zero; //stops after the dash

        timer = 0f;

        while (timer < 0.2f) //for 0.2 s dashes in opposite direction
        {
            timer += Time.deltaTime;

            rb.linearVelocity = -direction * 120f;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero; //stops after the dash
        canMove = true; //resumes enemy movement
        canAttack = true; //allow for the next attack
    }
}
