using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    private Vector3 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 mouseGamePos;
    private bool haveAmmo = true;


    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
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

        //mouseGamePos = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
    }

    private void PlayerBasicShoot()
    {
        if (haveAmmo)
        {
            haveAmmo = false;

            GameObject clone;
            clone = Instantiate(projectile, transform.position, Quaternion.identity);
            clone.SetActive(true);

            //Vector3 direction = (mouseGamePos - clone.transform.position);
            Vector3 direction = (mouseWorldPosition - clone.transform.position);
            clone.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * GAME.playerBasicAttackVelocity;

            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(GAME.playerBasicAttackCooldown);

        haveAmmo = true;
    }
}
