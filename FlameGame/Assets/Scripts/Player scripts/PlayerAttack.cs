using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject cursor;
    private Rigidbody2D rb;
    private Rigidbody2D cloneRb;
    public GameObject projectile;
    private Vector3 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 mouseGamePos;
    private bool haveAmmo = true;
    public float shootCooldown = 0.75f;

    public float projectileSpeed = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 direction = (rb.linearVelocity.normalized * 3);
        crosshair.transform.localPosition = direction;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PlayerBasicShoot();
        }

        mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseScreenPosition.z = 0f;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseGamePos = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        cursor.transform.position = mouseGamePos;
    }

    private void PlayerBasicShoot()
    {
        if(haveAmmo)
        {
            GameObject clone;
            clone = Instantiate(projectile, transform.position, Quaternion.identity);
            clone.SetActive(true);
            cloneRb = clone.GetComponent<Rigidbody2D>();
            Vector3 direction = (mouseGamePos - clone.transform.position);
            cloneRb.linearVelocity = direction.normalized * projectileSpeed;
            haveAmmo = false;
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        haveAmmo = true;
    }
}
