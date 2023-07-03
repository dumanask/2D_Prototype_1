using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    Collider2D damageCollider;

    private int playerDamage;
    private int enemyDamage;

    private void Awake()
    {
        damageCollider = GetComponent<Collider2D>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyManager enemyManager = collision.GetComponent<EnemyManager>();
            EnemyStats enemyStats = collision.GetComponentInChildren<EnemyStats>();
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            
            if (enemyManager != null)
            {
                
                if (enemyStats != null)
                {
                
                    if (playerStats != null)
                    {
                        playerDamage = playerStats.baseStrength - enemyStats.baseDefence;
                        Debug.Log("Player" + playerDamage);
                        enemyManager.TakeDamage(playerDamage);

                    }
                }
            }

        }

        if (collision.tag == "Player")
        {            
            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            PlayerStats playerStats = collision.GetComponentInChildren<PlayerStats>();
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

            if (playerManager != null)
            {

                if (playerStats != null)
                {

                    if (enemyStats != null)
                    {
                        enemyDamage = enemyStats.baseStrength - playerStats.baseDefence;
                        Debug.Log("Enemy" + enemyDamage);
                        playerManager.TakeDamage(enemyDamage);
                    }
                }
            }
        }
    }
}
