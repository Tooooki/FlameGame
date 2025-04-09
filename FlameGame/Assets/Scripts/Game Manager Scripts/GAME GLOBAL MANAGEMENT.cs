using UnityEngine;

public class GAMEGLOBALMANAGEMENT : MonoBehaviour
{
    public int existingEnemies;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        existingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
