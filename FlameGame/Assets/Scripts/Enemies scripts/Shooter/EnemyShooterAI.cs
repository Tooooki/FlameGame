using System.Collections;
using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    public bool canMove = false;
    public float moveX, moveY = 5;

    GameObject target;
    private Vector3 targetDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        InvokeRepeating("movement", 0f, 2f);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            targetDirection = (target.transform.position - transform.position).normalized * -1;
            if(targetDirection.x > 0 && targetDirection.y > 0)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.x - 1f, targetDirection.y - 1f).normalized * 4f;

            }
            else if(targetDirection.x < 0 && targetDirection.y > 0)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.x + 1, targetDirection.y - 1f).normalized * 4f;

            }
            else if (targetDirection.x > 0 && targetDirection.y < 0)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.x - 1, targetDirection.y + 1f).normalized * 4f;

            }
            else if (targetDirection.x < 0 && targetDirection.y < 0)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector3(targetDirection.x + 1, targetDirection.y + 1f).normalized * 4f;

            }



        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0f, 0f);
        }
    }

    private void movement()
    {
        StartCoroutine(movePerSecound());
        moveX = moveX * -1;
        moveY = 0;
    }

    private IEnumerator movePerSecound()
    {
        
        yield return new WaitForSeconds(1f);
    }
}
