using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            GetComponent<Rigidbody2D>().linearVelocity = InputManager.Movement * GAME.playerMoveVelocity;
        }
    }
}
