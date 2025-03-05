using UnityEngine;

public class EnemyCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject crosshair;

    private Vector2 crosshairPosition = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            crosshairPosition = collision.transform.position;
            Debug.Log("attack incoming");
        }
    }
    private void FixedUpdate()
    {
        crosshair.transform.position = crosshairPosition;
    }
}
