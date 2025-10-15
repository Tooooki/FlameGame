using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class PlayerPhoenixJump : MonoBehaviour
{
    public bool canPhoenixJump = true;

    private float durationCounter, cooldownCounter;

    private Vector3 startPos;

    [SerializeField] GameObject playerCollider, playerHitbox, playerSprite, ghost, playerFlame;
    [SerializeField] Light2D playerLight;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(canPhoenixJump)
            StartCoroutine(PlayDash(GAME.MouseWorldPos));
        }
    }

    private IEnumerator PlayDash(Vector3 Pos)
    {
        canPhoenixJump = false;

        GAME.Player.GetComponent<PlayerMovement>().enabled = false;
        GAME.Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        startPos = GAME.Player.transform.position;

        playerCollider.SetActive(false);
        playerHitbox.SetActive(false);
        playerFlame.SetActive(false);
        ghost.SetActive(true);
        ghost.transform.position = playerSprite.transform.position;

        durationCounter = GAME.playerPhoenixJumpDuration;


        while (durationCounter > GAME.playerPhoenixJumpDuration / 2)
        {
            durationCounter -= Time.deltaTime;
            ghost.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * 150f;
            playerSprite.transform.position = ghost.transform.position;
            playerLight.intensity = durationCounter - 0.4f;
            yield return null;
        }

        GAME.Player.transform.position = Pos;

        while (durationCounter > 0f)
        {
            durationCounter -= Time.deltaTime;
            ghost.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 150f;
            playerSprite.transform.position = ghost.transform.position;
            playerLight.intensity = 0.4f - durationCounter;
            yield return null;
        }

        playerFlame.SetActive(true);
        GAME.Player.transform.position = Pos;
        playerSprite.transform.position = Pos;
        ghost.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GAME.Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GAME.Player.GetComponent<PlayerMovement>().enabled = true;
        playerCollider.SetActive(true);
        playerHitbox.SetActive(true);
        ghost.SetActive(false);

        StartCoroutine(PlayPhoenixJumpCooldown());
    }

    private IEnumerator PlayPhoenixJumpCooldown()
    {
        cooldownCounter = GAME.playerPhoenixJumpCooldown;

        while (cooldownCounter > 0f)
        {
            cooldownCounter -= Time.deltaTime;

            yield return null;
        }

        canPhoenixJump = true;
    }
}
