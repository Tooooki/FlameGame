using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealthReaction : MonoBehaviour
{
    [SerializeField] GameObject HitBox, fullHPimage, highHPimage, medHPimage, lowHPimage, noHPimage;
    [SerializeField] Light2D playerLight;

    PlayerDeath healthScript;
    
    
    void Start()
    {
        
    }

    private void Awake()
    {
        healthScript = HitBox.GetComponent<PlayerDeath>();
    }

    void Update()
    {
        

        if(healthScript.playerHealth >= healthScript.playerMaxHealth / 5 * 4)
        {
            fullHPimage.SetActive(true);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(healthScript.playerMaxHealth / 5 * 4 >= healthScript.playerHealth && healthScript.playerHealth >= healthScript.playerMaxHealth / 5 * 3)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(true);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(healthScript.playerMaxHealth / 5 * 3 >= healthScript.playerHealth && healthScript.playerHealth >= healthScript.playerMaxHealth / 5 * 2)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(true);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(false);
        }
        else if(healthScript.playerMaxHealth / 5 * 2 >= healthScript.playerHealth && healthScript.playerHealth >= healthScript.playerMaxHealth / 5 * 1)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(true);
            noHPimage.SetActive(false);
        }
        else if(healthScript.playerMaxHealth / 5 * 1 >= healthScript.playerHealth && healthScript.playerHealth >= 0)
        {
            fullHPimage.SetActive(false);
            highHPimage.SetActive(false);
            medHPimage.SetActive(false);
            lowHPimage.SetActive(false);
            noHPimage.SetActive(true);
        }
    }
}
