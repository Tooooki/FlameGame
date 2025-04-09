using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor, bottomDoor, leftDoor, rightDoor, topWall, bottomWall, leftWall, rightWall;

    private bool up, down, left, right = false;

    public Vector2Int RoomIndex { get; set; }

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
        StartCoroutine(doorClose());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    private IEnumerator doorClose()
    {
        yield return new WaitForSeconds(2f);
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
        if(Input.GetKeyDown(KeyCode.O))
        {
            topDoor.SetActive(false);
            bottomDoor.SetActive(false);
            leftDoor.SetActive(false);
            rightDoor.SetActive(false);
        }
    }
}
