using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerObject;
    GameManager gameManager;
    PlayerStats playerStats;

    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce = 5.0f;
    private bool resetJump = false;
    private bool grounded = false;
    [SerializeField]
    private LayerMask groundLayer;
    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;

    Vector2 moveInput;
    public bool IsMoving { get; private set; }
    Rigidbody2D playerRb;

    public GameObject tempInventory;
    private TempInventory temporaryInventory;

    private void Awake()
    {
        gameManager = gameManagerObject.gameObject.GetComponent<GameManager>();
        playerStats = GetComponent<PlayerStats>();
        playerRb= GetComponent<Rigidbody2D>();
        currentHealth = playerStats.baseHP;
        walkSpeed = playerStats.baseSpeed;
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        temporaryInventory = tempInventory.GetComponent<TempInventory>();
    }
    private void Update()
    {
        if (gameManager.currentState == GameState.Playing)
        {
            Movement();
            Attack();
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        if (hitInfo.collider != null)
        {
            if (resetJump == false)
            {
                playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
            playerAnim.Jump(true);
        }

        playerRb.velocity = new Vector2(move * walkSpeed, playerRb.velocity.y);

        playerAnim.Move(move);
    }

    void Attack()
    {
        grounded = IsGrounded();

        if (IsGrounded() == true)
        {
            Debug.Log("Attack");
            playerAnim.Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        playerAnim.Death();
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            playerSprite.flipX = false;
            //_swordArcSprites.flipX = false;
            //_swordArcSprites.flipY = false;
            //
            //Vector3 newPos = _swordArcSprites.transform.localPosition;
            //newPos.x = 1.01f;
            //_swordArcSprites.transform.localPosition = newPos;

        }
        else if (faceRight == false)
        {
            playerSprite.flipX = true;
            //_swordArcSprites.flipX = true;
            //_swordArcSprites.flipY = true;

            //Vector3 newPos = _swordArcSprites.transform.localPosition;
            //newPos.x = -1.01f;
            //_swordArcSprites.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collecting Item and send them inventory
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);

            temporaryInventory.AddItem(collision.gameObject);
        }
    }



}
