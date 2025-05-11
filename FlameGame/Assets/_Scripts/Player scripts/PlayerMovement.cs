using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;

    audioManager audioManager;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        InvokeRepeating("PlayStepSFX", 0f, 0.4f);
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            GetComponent<Rigidbody2D>().linearVelocity = InputManager.Movement * GAME.playerMoveVelocity;
        }
    }
    private void PlayStepSFX()
    {
        if (InputManager.Movement.x != 0 || InputManager.Movement.y != 0)
        {
            audioManager.PlaySFX(audioManager.step);
        }
    }
}
