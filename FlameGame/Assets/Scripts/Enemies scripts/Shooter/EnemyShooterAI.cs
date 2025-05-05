using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    public bool canMove = false, swich = false, runAway = false;

    GameObject target;
    private Vector3 targetDirection;

    EnemyShooterAttack attackScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        attackScript = GetComponent<EnemyShooterAttack>();

        InvokeRepeating("movement", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //NIE RUSZAC!!!
        targetDirection = (target.transform.position - transform.position).normalized * -1;

        if (canMove == true && runAway == false)
        {
            if (swich)
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(-targetDirection.y, targetDirection.x) * 5;
            else
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.y, -targetDirection.x) * 5;
        }
        else if (canMove == true && runAway == true)
        {
            GetComponent<Rigidbody2D>().linearVelocity = targetDirection * 5;
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
