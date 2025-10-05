using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public bool canDash = true;

    private float durationCounter, cooldownCounter;

    [SerializeField] Image DashIcon, DashBar;


    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

    }

    private IEnumerator PlayDash(Vector2 dir)
    {
        canDash = false;

        GAME.Player.GetComponent<PlayerMovement>().enabled = false;

        durationCounter = GAME.playerDashDuration;

        while (durationCounter > 0f)
        {
            durationCounter -= Time.deltaTime;

            GAME.Player.GetComponent<Rigidbody2D>().linearVelocity = dir * GAME.playerDashVelocity;

            yield return null;
        }

        GAME.Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GAME.Player.GetComponent<PlayerMovement>().enabled = true;

        StartCoroutine(PlayDashCooldown());
    }

    private IEnumerator PlayDashCooldown()
    {
        cooldownCounter = GAME.playerDashCooldown;

        while (cooldownCounter > 0f)
        {
            cooldownCounter -= Time.deltaTime;


            yield return null;
        }

        canDash = true;
    }

    void Update()
    {
        if (Input.GetKeyUp("space") && canDash && GAME.dashAbility)
        {
            GAME.audioManager.PlaySFX(GAME.audioManager.mcGettingHit);
            Vector2 direction = new Vector2(0f, 0f);

            if (Input.GetKey(KeyCode.W))
            {
                direction.y = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.y = -1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1f;
            }

            StartCoroutine(PlayDash(direction));
        }

        DashBar.transform.localScale = new Vector3(cooldownCounter / GAME.playerDashCooldown, DashBar.transform.localScale.y);
    }
}
