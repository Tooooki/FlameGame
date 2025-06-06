using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealthReaction : MonoBehaviour
{
    [SerializeField] Light2D playerLight;

    [SerializeField] GameObject flame, sprite, wallcollider, hitbox;

    GAMEGLOBALMANAGEMENT GAME;



    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        sprite.transform.localScale = new Vector3(1, GAME.playerCurrentHealth / GAME.playerMaxHealth);
        wallcollider.transform.localScale = new Vector3(1, GAME.playerCurrentHealth / GAME.playerMaxHealth);
        hitbox.transform.localScale = new Vector3(1, GAME.playerCurrentHealth / GAME.playerMaxHealth);
        flame.transform.localPosition = new Vector3(0f, GAME.playerCurrentHealth * 2.7f / GAME.playerMaxHealth);
    }
}
