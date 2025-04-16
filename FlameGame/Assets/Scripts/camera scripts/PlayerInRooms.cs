using UnityEngine;

public class PlayerInRooms : MonoBehaviour
{
    public int gridPosX;
    public int gridPosY;

    [SerializeField] private GameObject cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gridPosX = Mathf.RoundToInt(transform.position.x / 80);
        gridPosY = Mathf.RoundToInt(transform.position.y / 48);
        cam.transform.position = new Vector3(gridPosX * 80, gridPosY * 48, -10);
    }
}
