using UnityEngine;

public class Wolf : Entity
{
    private float speed = 3f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    [Header("Wall check")]
    [SerializeField] private float checkDistance = 0.7f;
    [SerializeField] private float checkRadius = 0.1f;

    [Header("Ground check (edge detection)")]
    [SerializeField] private float groundCheckDistance = 0.7f;   // на сколько вперёд смотрим
    [SerializeField] private float groundCheckOffsetY = 0.6f;    // на сколько вниз от центра
    [SerializeField] private float groundCheckRadius = 0.1f;

    [SerializeField] private float flipCooldown = 0.25f;

    [SerializeField] private float bounceForce = 10f;
    /*[SerializeField] private float stompThereshold = 0.5f;*/
 
    private float flipTimer = 0f;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        dir = Vector3.right;
        sprite.flipX = dir.x < 0f;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (flipTimer > 0f) flipTimer -= Time.deltaTime;

        if (flipTimer <= 0f)
        {
            bool needFlip = false;

            // 1. Стена впереди
            Vector2 wallCheckPos = (Vector2)transform.position
                                   + Vector2.up * 0.1f
                                   + new Vector2(dir.x, 0f) * checkDistance;

            Collider2D[] wallHits = Physics2D.OverlapCircleAll(wallCheckPos, checkRadius);
            foreach (var h in wallHits)
            {
                if (h.transform.root == transform.root) continue;
                if (h.isTrigger) continue;
                needFlip = true;
                break;
            }

            
            if (!needFlip)
            {
                Vector2 groundCheckPos = (Vector2)transform.position
                                         + Vector2.up * groundCheckOffsetY
                                         + new Vector2(dir.x, 0f) * groundCheckDistance;

                Collider2D groundHit = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius);
                bool groundBelow = false;

                if (groundHit != null && groundHit.transform.root != transform.root && !groundHit.isTrigger)
                    groundBelow = true;

               
                if (!groundBelow) needFlip = true;
            }

            if (needFlip)
            {
                dir.x *= -1f;
                sprite.flipX = dir.x < 0f;   
                flipTimer = flipCooldown;
            }
        }

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Hero.Instance.gameObject) return;

        Rigidbody2D heroRb = Hero.Instance.GetComponent<Rigidbody2D>();
        if (heroRb == null) return;

        float heroY = Hero.Instance.GetComponent<Collider2D>().bounds.center.y;
        float wolfY = GetComponent<Collider2D>().bounds.center.y;


        bool fromAbove = heroY > wolfY + 0.1f;
        bool falling = heroRb.velocity.y <= 0.1f;

        if (fromAbove && falling)
        {
            heroRb.velocity = new Vector2(heroRb.velocity.x, 0f);
            heroRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            Die();
            return;
        }
        Hero.Instance.GetDamage();
    }
}