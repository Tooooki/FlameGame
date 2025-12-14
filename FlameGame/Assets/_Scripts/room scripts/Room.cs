using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("Room Doors & Walls")]
    [SerializeField] GameObject topDoor, bottomDoor, leftDoor, rightDoor, topWall, bottomWall, leftWall, rightWall, boss;

    [Header("Room Content")]
    public List<GameObject> clutter;
    public List<GameObject> activeClutter;
    public List<GameObject> avalibleEnemies;
    public List<Transform> enemiesSpawnpoints;

    public int maxClutter, minClutter;
    private int startClutter;

    public int minEnemies, maxEnemies;

    bool roomActive, didSpawnEnemy = false;
    private bool up, down, left, right = false;

    public bool clutterReady = true, BossRoom = false;

    public Vector2Int RoomIndex { get; set; }
    public Vector3 bossRoomPos;

    GAMEGLOBALMANAGEMENT GAME;

    [Header("Boss UI Settings")]
    [SerializeField] private GameObject bossSliderPrefab; // assign BossHealthSliderPrefab from Assets/Prefabs/Enemies

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        if (!BossRoom)
        {
            startClutter = Random.Range(minClutter, maxClutter + 1);
            clutterReady = true;
            InvokeRepeating("SetClutter", 0f, 0.01f);
        }
    }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up) { topWall.SetActive(false); up = true; }
        if (direction == Vector2Int.down) { bottomWall.SetActive(false); down = true; }
        if (direction == Vector2Int.left) { leftWall.SetActive(false); left = true; }
        if (direction == Vector2Int.right) { rightWall.SetActive(false); right = true; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !roomActive)
        {
            StartCoroutine(doorClose());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            roomActive = true;
    }

    private IEnumerator doorClose()
    {
        yield return null;

        if (!didSpawnEnemy)
        {
            StartCoroutine(SpawnEnemy());
            didSpawnEnemy = true;
        }

        if (up) topDoor.SetActive(true);
        if (down) bottomDoor.SetActive(true);
        if (left) leftDoor.SetActive(true);
        if (right) rightDoor.SetActive(true);

        GAME.audioManager.PlaySFX(GAME.audioManager.doorShut);
    }

    private IEnumerator SpawnEnemy()
    {
        if (BossRoom)
        {
            // Spawn boss
            GameObject TheBoss = Instantiate(boss, bossRoomPos, Quaternion.identity);
            TheBoss.SetActive(true);

            BossHealth bossHealth = TheBoss.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.bossHP = bossHealth.bossMaxHP;

                // Find Canvas dynamically
                Canvas canvas = Object.FindFirstObjectByType<Canvas>();
                if (canvas == null)
                    Debug.LogWarning("No Canvas found in the scene!");

                // Spawn slider prefab assigned in Inspector
                if (canvas != null && bossSliderPrefab != null)
                {
                    GameObject sliderInstance = Instantiate(bossSliderPrefab, canvas.transform);
                    bossHealth.bossSlider = sliderInstance.GetComponent<Slider>();
                    sliderInstance.gameObject.SetActive(true);
                    bossHealth.bossSlider.maxValue = bossHealth.bossMaxHP;
                    bossHealth.bossSlider.value = bossHealth.bossHP;
                }
                else
                {
                    Debug.LogWarning("Boss slider prefab not assigned or canvas not found!");
                }
            }

            yield return null;
        }
        else
        {
            // Normal enemies
            int enemyCount = Random.Range(minEnemies, maxEnemies + 1);
            while (enemyCount > 0)
            {
                int randomIndex = Random.Range(0, enemiesSpawnpoints.Count);
                Vector3 pos = enemiesSpawnpoints[randomIndex].position;
                enemiesSpawnpoints.RemoveAt(randomIndex);

                int enemyIndex = Random.Range(0, avalibleEnemies.Count);
                Instantiate(avalibleEnemies[enemyIndex], pos, Quaternion.identity);

                enemyCount--;
                yield return null;
            }
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

        if (activeClutter.Count >= startClutter || !clutterReady)
        {
            CancelInvoke("SetClutter");
            clutterReady = false;
        }
    }

    public void SetClutter()
    {
        if (activeClutter.Count < startClutter && clutterReady)
        {
            int randomClutterIndex = Random.Range(0, clutter.Count);
            GameObject clutterItem = Instantiate(
                clutter[randomClutterIndex],
                new Vector3(transform.position.x + Random.Range(-28f, 28f), transform.position.y + Random.Range(-12f, 12f)),
                Quaternion.identity,
                this.transform
            );
            activeClutter.Add(clutterItem);
        }
    }
}
