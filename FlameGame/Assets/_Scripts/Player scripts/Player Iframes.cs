using System.Collections;
using UnityEngine;

public class PlayerIframes : MonoBehaviour
{
    public bool PlayerImmunity = false;

    private GameObject hitbox;

    private void Awake()
    {
        hitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
        PlayerImmunity = false;
    }

    void Update()
    {
        if (PlayerImmunity)
        {
            hitbox.SetActive(false);
        }
        else
        {
            hitbox.SetActive(true);
        }
    }
}
