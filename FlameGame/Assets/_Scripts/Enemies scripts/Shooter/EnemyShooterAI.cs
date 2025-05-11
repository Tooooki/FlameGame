using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    public bool canMove, swich, runAway;



    EnemyShooterAttack attackScript;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        attackScript = GetComponent<EnemyShooterAttack>();

        InvokeRepeating("movement", 0f, 1f);

        runAway = false;
    }

    // Update is called once per frame
    void Update()
    {
        //NIE RUSZAC!!!
        Vector3 targetDirection = (GAME.Player.transform.position - transform.position).normalized * -1;

        if (canMove == true && runAway == false)
        {
            if (swich)
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(-targetDirection.y, targetDirection.x) * GAME.enemyShooterMoveVelocity;
            else
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.y, -targetDirection.x) * GAME.enemyShooterMoveVelocity;
        }
        else if (canMove == true && runAway == true)
        {
            GetComponent<Rigidbody2D>().linearVelocity = targetDirection * GAME.enemyShooterMoveVelocity;
        }
        else
        {

        }
        //ZAAWANSOWANA MATEMATYKA!!!
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackScript.canShoot = false;
            runAway = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackScript.canShoot = true;
            runAway = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "walls")
        {
            swich = !swich;
        }
    }

    private void movement()
    {
        if (Random.Range(0, 2) == 0)
        {
            swich = !swich;
        }
    }
}
