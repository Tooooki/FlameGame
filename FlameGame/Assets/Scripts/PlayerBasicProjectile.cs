using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBasicProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject self;
    [SerializeField] private CircleCollider2D collider;

    private GameObject whatHitU;
    
    public UnityEvent OnWallHit;
    public UnityEvent OnEnemyHit;

    EnemyCrosshair enemyScript;

    void Start()
    {
       
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            Debug.Log("Miss");
            OnWallHit?.Invoke();
            rb.linearVelocity = Vector3.zero;
            collider.enabled = false;
            Destroy(self, 0.2f);
        }
        
        
        if (collision.CompareTag("EnemyHitbox"))
        {
            Debug.Log("Hit");
            OnEnemyHit?.Invoke();
            rb.linearVelocity = Vector3.zero;
            collider.enabled = false;
            whatHitU = collision.gameObject.transform.parent.gameObject;
            enemyScript = whatHitU.GetComponent<EnemyCrosshair>();
            enemyScript.Knockback(whatHitU);
            Destroy(self, 0.2f);
        }
    }
}
