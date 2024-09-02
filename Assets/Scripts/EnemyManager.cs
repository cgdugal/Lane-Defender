using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject mediumEnemy;
    [SerializeField] private GameObject slowEnemy;

    private float fastEnemyChance = .40f;
    private float mediumEnemyChance = .80f;

    private bool shouldSpawnEnemies = true;

    private float spawnDelayMax = 2.5f;
    private float spawnDelayMin = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawning()
    {
        while (shouldSpawnEnemies)
        {
            SpawnEnemy();

            float spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);
            
        }
    }

    void SpawnEnemy()
    {
        int currentSpawnPosition = Random.Range(0, spawnPositions.Length);

        float chooseEnemyType = Random.value;
        
        if(chooseEnemyType <= fastEnemyChance)
        {
            Instantiate(fastEnemy, spawnPositions[currentSpawnPosition].position, Quaternion.identity);
        }
        else if(chooseEnemyType <= mediumEnemyChance)
        {
            Instantiate(mediumEnemy, spawnPositions[currentSpawnPosition].position, Quaternion.identity);
        }
        else
        {
            Instantiate(slowEnemy, spawnPositions[currentSpawnPosition].position, Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        shouldSpawnEnemies = false;
    }
}
