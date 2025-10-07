using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBehaviour : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    public bool canMove, canAttack, goingInvisible, invisible, visible;

    private bool isFading = false;
    private bool playerInside = false;

    private Coroutine changeAlphaCoroutine;

    public HashSet<GameObject> objectsInsideTrigger;

    private SpriteRenderer[] spriteRenderers;

    CircleCollider2D attackDetection, visibleDetection;
    BoxCollider2D boxC;
    Rigidbody2D rb;




    private void Start()
    {
        visible = false;
        invisible = true;
        isFading = false;

        // Force re-evaluate the player inside logic
        playerInside = objectsInsideTrigger.Contains(GAME.Player);

        if (playerInside)
        {
            changeAlphaCoroutine = StartCoroutine(FadeToAlpha(1f, 0.5f, () =>
            {
                invisible = false;
                visible = true;
                isFading = false;
            }));
        }
        else
        {
            changeAlphaCoroutine = StartCoroutine(FadeToAlpha(0f, 0.5f, () =>
            {
                invisible = true;
                visible = false;
                isFading = false;
            }));
        }
    }
    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        attackDetection = GetComponentInChildren<CircleCollider2D>();
        visibleDetection = GetComponent<CircleCollider2D>();
        boxC = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        canMove = true;
        canAttack = true;
        objectsInsideTrigger = new HashSet<GameObject>();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove)
        {
            Vector2 direction = (GAME.Player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * GAME.enemyAssassinMoveVelocity;
        }

        boxC.enabled = visible && canAttack;
        visibleDetection.radius = GAME.enemyAssassinDetectionDistance;

        bool playerNowInside = objectsInsideTrigger.Contains(GAME.Player);

        if (!isFading)
        {
            if (playerNowInside && !visible)
            {
                if (changeAlphaCoroutine != null)
                    StopCoroutine(changeAlphaCoroutine);

                changeAlphaCoroutine = StartCoroutine(FadeToAlpha(1f, 0.5f, () =>
                {
                    invisible = false;
                    visible = true;
                    isFading = false;
                }));
            }
            else if (!playerNowInside && !invisible)
            {
                if (changeAlphaCoroutine != null)
                    StopCoroutine(changeAlphaCoroutine);

                changeAlphaCoroutine = StartCoroutine(FadeToAlpha(0f, 0.5f, () =>
                {
                    invisible = true;
                    visible = false;
                    isFading = false;
                }));
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
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
            Debug.Log("Player exited trigger");
            objectsInsideTrigger.Remove(collision.gameObject);
        }
    }



    private IEnumerator FadeToAlpha(float targetAlpha, float duration, System.Action onComplete)
    {
        isFading = true;

        float timer = 0f;
        float startAlpha = spriteRenderers[0].color.a;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);

            foreach (var sr in spriteRenderers)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            yield return null;
        }

        foreach (var sr in spriteRenderers)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, targetAlpha);

        onComplete?.Invoke();
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
