using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float strength = 40f;
    [SerializeField] private float delay = 0.1f;

    public bool canDash = true;
    public float cooldownValue = 1.5f;

    public UnityEvent OnBegin;
    public UnityEvent OnDone;

    PlayerIframes IframesScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayDash()
    {
        if(canDash)
        {
            canDash = false;
            OnBegin?.Invoke();
            IframesScript = GetComponent<PlayerIframes>();
            Debug.Log("dodge 2");
            IframesScript.Iframes();
            rb.AddForce(rb.linearVelocity.normalized * strength, ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }
        
    }
    
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        OnDone?.Invoke();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownValue);
        canDash = true;
    }
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            PlayDash();
        }
    }
}
