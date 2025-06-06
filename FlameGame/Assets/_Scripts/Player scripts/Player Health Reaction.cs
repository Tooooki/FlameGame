using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealthReaction : MonoBehaviour
{
    [SerializeField] Light2D playerLight;

    [SerializeField] GameObject flame, sprite;

    GAMEGLOBALMANAGEMENT GAME;



    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        sprite.transform.localScale = new Vector3(1, GAME.playerCurrentHealth / GAME.playerMaxHealth);
        flame.transform.localPosition = new Vector3(-0.07f, (GAME.playerCurrentHealth * 2.3f / GAME.playerMaxHealth) - 1.15f);
        playerLight.transform.localPosition = new Vector3(0, (GAME.playerCurrentHealth * 2.3f / GAME.playerMaxHealth) - 1.15f);
    }
}
