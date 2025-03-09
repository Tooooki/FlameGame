using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBasicProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject self;
    [SerializeField] private CircleCollider2D collider;

    private GameObject projectile;
    private GameObject enemyShoot;
    
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
            OnWallHit?.Invoke();
            rb.linearVelocity = Vector3.zero;
            collider.enabled = false;
            Destroy(self, 0.2f);
        }
        
        
        if (collision.CompareTag("EnemyHitbox"))
        {
            OnEnemyHit?.Invoke();
            rb.linearVelocity = Vector3.zero;
            collider.enabled = false;
            enemyShoot = collision.gameObject.transform.parent.gameObject;
            projectile = rb.gameObject;
            enemyScript = enemyShoot.GetComponent<EnemyCrosshair>();
            enemyScript.Knockback(projectile);
            Destroy(self, 0.2f);
        }
    }
}
