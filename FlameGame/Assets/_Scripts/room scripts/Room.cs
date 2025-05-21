using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor, bottomDoor, leftDoor, rightDoor, topWall, bottomWall, leftWall, rightWall;

    public List<GameObject> clutter;
    public List<GameObject> activeClutter;
    public List<GameObject> avalibleEnemies;
    public List<Transform> enemiesSpawnpoints;
    
    public int maxClutter, minClutter;
    private int startClutter;

    public int minEnemies, maxEnemies;

    bool roomActive, didSpawnEnemy = false;

    private bool up, down, left, right = false;

    public bool clutterReady = true;

    public Vector2Int RoomIndex { get; set; }

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        startClutter = Random.Range(minClutter, maxClutter + 1);
        clutterReady = true;
        InvokeRepeating("SetClutter", 0f, 0.01f);
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
            if (roomActive == false)
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

        if(Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator doorClose()
    {
        yield return new WaitForSeconds(0f);

        if (didSpawnEnemy == false)
        {
            StartCoroutine(SpawnEnemy());
            didSpawnEnemy = true;
        }


        if (up)
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

        GAME.audioManager.PlaySFX(GAME.audioManager.doorShut);
    }

    private IEnumerator SpawnEnemy()
    {
        //there should be more spawnpoints than maxEnemy value
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        while(enemyCount > 0)
        {
            int randomItemFromList = Random.Range(0, enemiesSpawnpoints.Count);
            Vector3 pos = enemiesSpawnpoints[randomItemFromList].position;
            enemiesSpawnpoints.Remove(enemiesSpawnpoints[randomItemFromList]);

            randomItemFromList = Random.Range(0, avalibleEnemies.Count);

            GameObject enemy = Instantiate(avalibleEnemies[randomItemFromList], pos, Quaternion.identity);

            enemyCount -= 1;
            yield return null;
        }
    }

    private void Update()
    {
        if (GAME.existingEnemiesCount == 0)
        {
            topDoor.SetActive(false);
            bottomDoor.SetActive(false);
            leftDoor.SetActive(false);
            rightDoor.SetActive(false);
        }

        if(activeClutter.Count >= startClutter || !clutterReady)
        {
            CancelInvoke("SetClutter");
            clutterReady = false;
        }
    }

    public void SetClutter()
    {
        if (activeClutter.Count < startClutter && clutterReady)
        {
            GameObject clutterItem;
            int randomClutterIndex = Random.Range(0, clutter.Count);
            clutterItem = Instantiate(clutter[randomClutterIndex], new Vector3(transform.position.x + Random.Range(-28f, 28f), transform.position.y + Random.Range(-12f, 12f)), Quaternion.identity, this.transform);
            activeClutter.Add(clutterItem);
        }
    }
}
