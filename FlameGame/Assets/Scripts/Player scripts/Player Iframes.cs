using System.Collections;
using UnityEngine;

public class PlayerIframes : MonoBehaviour
{
    [SerializeField] private GameObject HitBox;
    
    public float IframesDuration = 0.5f;

    private bool IsImmune = false;

    void Update()
    {
        if(IsImmune)
        {
            HitBox.SetActive(false);
        }
        else
        {
            HitBox.SetActive(true);
        }
    }

    public void Iframes()
    {
        //if (IsImmune = false)
        
        StopAllCoroutines();
        Debug.Log("dodge 3");
        StartCoroutine(IframesTiming());
        
    }

    private IEnumerator IframesTiming()
    {
        IsImmune = true;
        yield return new WaitForSeconds(IframesDuration);
        IsImmune = false;
    }
}
