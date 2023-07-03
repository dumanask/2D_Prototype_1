using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerObject;
    GameManager gameManager;
    EnemyStats enemyStats;
    EnemyAnimation enemyAnimation;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Rigidbody2D enemyRb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int currentHP;
    [SerializeField] private int xpToPlayer;

    public Transform target;

    private void Awake()
    {
        gameManager = gameManagerObject.gameObject.GetComponent<GameManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyRb= GetComponent<Rigidbody2D>();
        currentHP = enemyStats.baseHP;
        moveSpeed = enemyStats.speed;
    }
    private void Start()
    {
        Direction();
    }

    private void Update()
    {
        if (gameManager.currentState == GameState.Playing)
        {
            Debug.Log(PlayerInSight());
            cooldownTimer += Time.deltaTime;

            if (PlayerInSight())
            {
                Debug.Log("Enemyyyyy2");
                if (cooldownTimer > enemyStats.attackCooldown)
                {
                    cooldownTimer = 0;
                    Debug.Log("Enemyyyyy");
                    Attack();
                }
            }
            else
            {
                Movement();
            }
        }
        else
        {
            enemyRb.velocity = Vector2.zero;
            enemyAnimation.Idle();
        }
        

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                                             new Vector3 (boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    
    private void Movement()
    {
        // Calculate the direction to the player
        Vector2 direction = (target.position - transform.position).normalized;

        // Move towards the player if they are far away
        if (!PlayerInSight())
        {
            enemyRb.velocity = direction * moveSpeed;
            enemyAnimation.Move();
        }
        // Stop moving if the player is close enough
        else
        {
            enemyRb.velocity = Vector2.zero;
        }
    }

    private void Direction()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        // Flip the enemy sprite to face the direction it is moving
        if (direction.x > 0)
        {
            Debug.Log("1");
            //transform.localScale = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            Debug.Log("0");
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Attack()
    {
        enemyAnimation.Attack();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHP = currentHP - damage;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Death();
        }
    }

    void Death()
    {
        GetComponent<ItemBag>().InstatiateItem(transform.position);
        GiveXP();
        gameObject.SetActive(false);
        enemyAnimation.Death();        
    }

    void GiveXP()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        xpToPlayer = enemyStats.deathXP;
        playerStats.GetXP(xpToPlayer);
    }
}
