using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;

    private Vector3 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 direction;
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
        mouseWorldPosition.z = 0f;

        direction = (mouseWorldPosition - (GAME.Player.transform.position + new Vector3(0, GAME.playerCurrentHealth * 2.7f / GAME.playerMaxHealth))).normalized;
    }

    private void PlayerBasicShoot()
    {
        if (haveAmmo)
        {
            haveAmmo = false;

            GameObject clone = Instantiate(projectile, transform.position + new Vector3(0, (GAME.playerCurrentHealth / GAME.playerMaxHealth) * 2.7f), Quaternion.identity);

            clone.SetActive(true);
            clone.GetComponent<Rigidbody2D>().linearVelocity = direction * GAME.playerBasicAttackVelocity;

            GAME.audioManager.PlaySFX(GAME.audioManager.mainCharShootingFire);

            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(GAME.playerBasicAttackCooldown);
        haveAmmo = true;
    }
}