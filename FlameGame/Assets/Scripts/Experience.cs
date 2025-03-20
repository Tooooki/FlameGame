using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour
{
    public float expAmount;

    private float expNeeded;
    private int Level;

    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text textLevel;


    void Start()
    {
        expNeeded = 100f;
        Level = 1;
    }

    void Update()
    {
        expBar.transform.localScale = new Vector3(expAmount / 100, 1f);
        textLevel.SetText(Level.ToString());
        if(expAmount >= expNeeded)
        {
            Level++;
            expAmount = expAmount - expNeeded;
            expNeeded = 100 + (Level * 50);
        }
    }

    public void GetExp(float xpGottenAmount)
    {
        expAmount = expAmount + xpGottenAmount;
    }
}
