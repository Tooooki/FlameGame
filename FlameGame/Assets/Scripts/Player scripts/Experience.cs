using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    public float expAmount;

    private float expNeeded;
    public int Level;

    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject HitBox;

    PlayerAttack attackScript;
    PlayerDeath healthScript;

    void Start()
    {
        expNeeded = 100f;
        Level = 1;
        attackScript = Player.GetComponent<PlayerAttack>();
        healthScript = HitBox.GetComponent<PlayerDeath>();
    }

    void Update()
    {
        textLevel.SetText(Level.ToString());
        if(expAmount >= expNeeded)
        {
            Level++;
            expAmount = expAmount - expNeeded;
            expNeeded = 100 + (Level * 50);
            healthScript.playerMaxHealth = healthScript.playerMaxHealth + 20;
            healthScript.playerHealth = healthScript.playerMaxHealth;
        }
        else
        {
            expBar.transform.localScale = new Vector3(expAmount / expNeeded, 1f);
        }

        attackScript.projectileSpeed = 10 + Level * 10;
    }

    public void GetExp(float xpGottenAmount)
    {
        expAmount = expAmount + xpGottenAmount;
    }
}
