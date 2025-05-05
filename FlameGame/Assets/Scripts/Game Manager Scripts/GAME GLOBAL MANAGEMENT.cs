using UnityEngine;
using UnityEngine.UI;

public class GAMEGLOBALMANAGEMENT : MonoBehaviour
{
    public int existingEnemies;
    public int playerLevel;
    public GameObject Player;



    Experience expScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        expScript = GetComponent<Experience>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        existingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        playerLevel = expScript.Level;
    }
}
