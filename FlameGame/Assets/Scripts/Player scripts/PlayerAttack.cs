using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 direction = (rb.linearVelocity.normalized * 3);
        crosshair.transform.localPosition = direction;
    }
}
