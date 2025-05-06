using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealthReaction : MonoBehaviour
{
    [SerializeField] GameObject fullHPimage, highHPimage, medHPimage, lowHPimage, noHPimage;
    [SerializeField] Light2D playerLight;

    GAMEGLOBALMANAGEMENT GAME;



    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        if (GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 4)
        {
            fullHPimage.SetActive(true);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(GAME.playerMaxHealth / 5 * 4 >= GAME.playerCurrentHealth && GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 3)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(true);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 3 && GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 2)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(true);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 2 && GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 1)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(true);
            noHPimage.SetActive(false);
        }
        else if(GAME.playerCurrentHealth >= GAME.playerMaxHealth / 5 * 1 && GAME.playerCurrentHealth >= 0)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(true);
        }
    }
}
