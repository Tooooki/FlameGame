using UnityEngine;

public class AssassinDamage : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    public float hp;

    public bool canDie;

    [SerializeField] GameObject hpBar;

    void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        canDie = false;
        hp = GAME.enemyAssassinMaxHealth;
    }
    void Update()
    {
        if (hp <= 0 && canDie)
        {
            Die();
        }

        hpBar.transform.localScale = new Vector3(hp / GAME.enemyAssassinMaxHealth, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            GAME.PlayerGetDamage(GAME.enemyAssassinDamage);
        }
    }

    public void GetDamage(float damage)
    {
        canDie = true;
        hp -= damage;
        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);
        if (transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().color.a < 0.5f)
            transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().color.a + 0.5f);
        else
            transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void Die()
    {
        GAME.PlayerGetExperience(GAME.enemyAssassinExperienceDrop);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
