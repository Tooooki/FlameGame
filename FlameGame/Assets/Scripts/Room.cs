using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor, bottomDoor, leftDoor, rightDoor, topWall, bottomWall, leftWall, rightWall;
    [SerializeField] GameObject enemyPrefab;

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
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(transform.position.x + 25, transform.position.y + 12), Quaternion.identity);
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
        if(gameManagementScript.existingEnemies == 0)
        {
            topDoor.SetActive(false);
            bottomDoor.SetActive(false);
            leftDoor.SetActive(false);
            rightDoor.SetActive(false);
        }
    }
}
