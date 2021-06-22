using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;
    public GameObject enemyPrefab;
    public float minimumTimebetweenEnemies; //waits for the next enemy to spawn. 
    
    public Transform[] spawnPoints;   //enemy spawn point
    public int enemyLimit;     //total enemy allowed in the scene
    [SerializeField]
    private float decreaseRate;  //enemy spawn time decreases with this value. the more the faster the enemy will spawn
    [SerializeField]
    private float decreaseWaitTime;   //the amount of time to be waited to increase the difficulty by decreasing spawn time
    private float initialdecreaseWaitTime;

    void Start()
    {
        initialdecreaseWaitTime = decreaseWaitTime;
        instance = this;
        StartCoroutine(StartSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        decreaseWaitTime -= Time.deltaTime;
       if(decreaseWaitTime<=0)
        {
            IncreaseSpawnRate();
            decreaseWaitTime = initialdecreaseWaitTime;
        }
        
    }

    private void IncreaseSpawnRate()
    {
        minimumTimebetweenEnemies -= decreaseRate;
        minimumTimebetweenEnemies = Mathf.Clamp(minimumTimebetweenEnemies, 0.5f, 3.5f);
    }

    void SpawnEnemy()
    {
        if(EnemyPoolManager.instance.GetActiveEnemySize()<enemyLimit)
        {
            if (EnemyPoolManager.instance.CheckEnemyInPool())  //checks if any enemy is avaible which is destroyed once and ready to be reassign
            {
                GameObject enemy = EnemyPoolManager.instance.GetEnemyFromPool();
                enemy.transform.position = spawnPoints[Random.Range(0, 3)].position;
                enemy.transform.rotation = Quaternion.identity;             
                enemy.gameObject.SetActive(true);                           // if enemy available in pool. Uses it instead of creating a new gameObject
                EnemyPoolManager.instance.AddNewEnemyToActivePool(enemy);
            }
            else   
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, 3)].position, Quaternion.identity); // if no enemies avaialbe in pool, creates a new enemyObject
                EnemyPoolManager.instance.AddNewEnemyToActivePool(enemy);
            }
        }
        else
        {
            Debug.Log("Max Enemy Limit Reached");
        }
       
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(1f);
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(minimumTimebetweenEnemies);
        }
       
    }

    

 
}
