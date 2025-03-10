using System.Collections;
using UnityEngine;

public class PlayerIframes : MonoBehaviour
{
    [SerializeField] private GameObject HitBox;
    
    public float IframesDuration = 0.5f;

    private bool IsImmune = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
