using UnityEngine;
using UnityEngine.InputSystem;

public class Loadingprocess : MonoBehaviour
{
    RoomManager roomScript;

    [SerializeField] private GameObject loadingScreen, Player, enemyRunner, enemyShooter, enemyTank, enemyAssassin, Camera;

    
    void Start()
    {
        roomScript = GetComponent<RoomManager>();
    }

    void Update()
    {
        if(roomScript.generationComplete == true)
        {
            loadingScreen.SetActive(false);
        }else
        {
            loadingScreen.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject spawnedEnemy;
            Vector3 enemySpawnPos = new Vector3(0, 0, 0);
            float enemySpawnPosX = 0, enemySpawnPosY = 0;
            int randomSideOfTheRoom = Random.Range(1, 5);
            if(randomSideOfTheRoom == 1)
            {
                enemySpawnPosX = 0;
                enemySpawnPosY = 13;
            }
            else if(randomSideOfTheRoom == 2)
            {
                enemySpawnPosX = 0;
                enemySpawnPosY = -13;
            }
            else if(randomSideOfTheRoom == 3)
            {
                enemySpawnPosX = 30;
                enemySpawnPosY = 0;
            }
            else if(randomSideOfTheRoom == 4)
            {
                enemySpawnPosX = -30;
                enemySpawnPosY = 0;
            }
            enemySpawnPos = new Vector3(enemySpawnPosX, enemySpawnPosY);
            spawnedEnemy = Instantiate(enemyRunner, enemySpawnPos, Quaternion.identity);
            spawnedEnemy.SetActive(true);
        }
    }

    public void OnGameLoaded()
    {
        Player.SetActive(true);
    }
}
