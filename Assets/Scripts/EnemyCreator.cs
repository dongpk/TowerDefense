using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] int amountToSpawn = 5;
    [SerializeField] GameObject enemyPrefab;
    private List<Transform> enemies;

    void Start()
    {
        CreateNewEnemies();
    }

    private void CreateNewEnemies()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            float randomX = Random.Range(-4, 4);
            float randomZ = Random.Range(-4, 4);
            Vector3 newPosition = new Vector3(randomX, 0.75f, randomZ);
            GameObject newEnemy = Instantiate(enemyPrefab, newPosition, Quaternion.identity);
            enemies.Add(newEnemy.transform);
        }
    }
    public List<Transform> EnemiesList() => enemies;
    
}
