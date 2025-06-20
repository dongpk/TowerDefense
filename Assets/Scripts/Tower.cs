using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemyCreator enemyCreator;
    private Transform enemy;
    [SerializeField] Transform towerHead; 
 

    [Header("Attack Details")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;

    [Header("Bullet Details")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 4f;
    void Awake()
    {
        enemyCreator = FindFirstObjectByType<EnemyCreator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemy == null)
        {
            enemy = FindCLosestEnemy();
            return; // Exit if no enemy is assigned
        }
        
        
        if ( Vector3.Distance(enemy.position, towerHead.position) < attackRange)
        {
            towerHead.LookAt(enemy);
            if (readyToAttack())
            {                
                CreateBullet();
            }
        }
    }
    private Transform FindCLosestEnemy()
    {
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;
           
        foreach (Transform enemy in enemyCreator.EnemiesList())
        {
           
                float distance = Vector3.Distance(enemy.position, transform.position);
            if (distance < closestDistance && distance < attackRange)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        if(closestEnemy != null)
        {
            enemyCreator.EnemiesList() .Remove(closestEnemy); // Remove the closest enemy from the list
        }

        return closestEnemy;
    }
    void FindRandomEnemy()
    {
        if(enemyCreator.EnemiesList().Count <= 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, enemyCreator.EnemiesList().Count);
        enemy = enemyCreator.EnemiesList()[randomIndex];
        enemyCreator.EnemiesList().RemoveAt(randomIndex); 
    }
    bool readyToAttack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time; 

            return true;
        }
    
        return false;
        
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, towerHead.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().linearVelocity = (enemy.position - towerHead.position).normalized * bulletSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
