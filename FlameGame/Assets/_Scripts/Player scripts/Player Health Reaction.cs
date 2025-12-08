using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealthReaction : MonoBehaviour
{
    [SerializeField] Light2D playerLight;

    [SerializeField] GameObject flame;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject wallcollider;
    [SerializeField] GameObject hitbox;

    private GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        // Clamp health normalized (0 → 1)
        float healthNormalized = Mathf.Clamp(GAME.playerCurrentHealth / GAME.playerMaxHealth, 0f, 1f);

        // Apply safe scale
        sprite.transform.localScale = new Vector3(1f, healthNormalized, 1f);
        wallcollider.transform.localScale = new Vector3(1f, healthNormalized, 1f);
        hitbox.transform.localScale = new Vector3(1f, healthNormalized, 1f);

        // Adjust flame height safely
        float flameY = healthNormalized * 2.7f;
        flame.transform.localPosition = new Vector3(0f, flameY, 0f);
    }
}
