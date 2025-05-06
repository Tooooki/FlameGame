using System.Collections;
using UnityEngine;

public class PlayerIframes : MonoBehaviour
{
    public float IframesDuration = 0.5f;

    public bool PlayerImmunity = false;

    void Update()
    {
        if (PlayerImmunity)
        {
            GetComponentInChildren<PlayerDeath>().enabled = false;
        }
        else
        {
            GetComponentInChildren<PlayerDeath>().enabled = true;
        }
    }
}
