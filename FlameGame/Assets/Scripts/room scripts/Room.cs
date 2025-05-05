using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor, bottomDoor, leftDoor, rightDoor, topWall, bottomWall, leftWall, rightWall;
    [SerializeField] GameObject enemyRunnerPrefab, enemyShooterPrefab;

    bool roomActive, didSpawnEnemy = false;

    private int enemyCount;//
    private bool up, down, left, right = false;

    public Vector2Int RoomIndex { get; set; }

    GAMEGLOBALMANAGEMENT gameManagementScript;

    private void Awake()
    {
        gameManagementScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            //topDoor.SetActive(true);
            topWall.SetActive(false);
            up = true;
        }
        if (direction == Vector2Int.down)
        {
            //bottomDoor.SetActive(true);
            bottomWall.SetActive(false);
            down = true;
        }
        if (direction == Vector2Int.left)
        {
            //leftDoor.SetActive(true);
            leftWall.SetActive(false);
            left = true;
        }
        if (direction == Vector2Int.right)
        {
            //rightDoor.SetActive(true);
            rightWall.SetActive(false);
            right = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(roomActive == false)
            {
                StartCoroutine(doorClose());

            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator doorClose()
    {
        yield return new WaitForSeconds(1f);
        
        if(didSpawnEnemy == false)
        {
            int RandomEnemyCount = Random.Range(1, 5);
            Vector3 enemySlotUR = new Vector3(transform.position.x + 25, transform.position.y + 12);
            Vector3 enemySlotUL = new Vector3(transform.position.x - 25, transform.position.y + 12);
            Vector3 enemySlotDR = new Vector3(transform.position.x + 25, transform.position.y - 12);
            Vector3 enemySlotDL = new Vector3(transform.position.x - 25, transform.position.y - 12);
            bool URtaken = false, ULtaken = false, DRtaken = false, DLtaken = false;

            if(RandomEnemyCount >= 1 && !URtaken)
            {
                if(Random.Range(0, 2) == 0)
                {
                    GameObject enemy = Instantiate(enemyRunnerPrefab, enemySlotUR, Quaternion.identity);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyShooterPrefab, enemySlotUR, Quaternion.identity);
                }
                URtaken = true;
            }

            if (RandomEnemyCount >= 2 && !ULtaken)
            {
                if (Random.Range(0, 2) == 0)
                {
                    GameObject enemy = Instantiate(enemyRunnerPrefab, enemySlotUL, Quaternion.identity);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyShooterPrefab, enemySlotUL, Quaternion.identity);
                }
                ULtaken = true;
            }

            if (RandomEnemyCount >= 3 && !DRtaken)
            {
                if (Random.Range(0, 2) == 0)
                {
                    GameObject enemy = Instantiate(enemyRunnerPrefab, enemySlotDR, Quaternion.identity);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyShooterPrefab, enemySlotDR, Quaternion.identity);
                }
                DRtaken = true;
            }

            if (RandomEnemyCount >= 4 && !DLtaken)
            {
                if (Random.Range(0, 2) == 0)
                {
                    GameObject enemy = Instantiate(enemyRunnerPrefab, enemySlotDL, Quaternion.identity);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyShooterPrefab, enemySlotDL, Quaternion.identity);
                }
                DLtaken = true;
            }
            didSpawnEnemy = true;
        }

        yield return new WaitForSeconds(0.5f);

        if(up)
        {
            topDoor.SetActive(true);
        }
        if (down)
        {
            bottomDoor.SetActive(true);
        }
        if (left)
        {
            leftDoor.SetActive(true);
        }
        if (right)
        {
            rightDoor.SetActive(true);
        }

    }

    private void Update()
    {
        if(gameManagementScript.existingEnemiesCount == 0)
        {
            topDoor.SetActive(false);
            bottomDoor.SetActive(false);
            leftDoor.SetActive(false);
            rightDoor.SetActive(false);
        }
    }
}
