using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PoolManager))]
public class SpawnManager : MonoBehaviour
{    
    public GameObject objectPrefab;
    public float minimumTimebetweenObjects; //waits for the next object to spawn. 
    
    public Transform[] spawnPoints;   //enemy spawn point
    public int objectLimit;     //total enemy allowed in the scene
    [SerializeField]
    private float decreaseRate;  //enemy spawn time decreases with this value. the more the faster the object will spawn
    [SerializeField]
    private float decreaseWaitTime;   //the amount of time to be waited to increase the difficulty by decreasing spawn time
    private float initialdecreaseWaitTime;
    private PoolManager poolManager;

    [Header("Random")]
    [SerializeField]
    private bool spawnRandomly;

    [SerializeField]
    private float randWaitMinimum;

    [SerializeField]
    private float randWaitMaximum;


    

    void Start()
    {
        poolManager = GetComponent<PoolManager>();
        initialdecreaseWaitTime = decreaseWaitTime;       
        StartCoroutine(StartSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnRandomly)
        {
            decreaseWaitTime -= Time.deltaTime;
            if (decreaseWaitTime <= 0)
            {
                IncreaseSpawnRate();
                decreaseWaitTime = initialdecreaseWaitTime;
            }
        }
       
       
        
    }

    private void IncreaseSpawnRate()
    {
        minimumTimebetweenObjects -= decreaseRate;
        minimumTimebetweenObjects = Mathf.Clamp(minimumTimebetweenObjects, 3.5f, 10f);
    }

    void SpawnEnemy()
    {
       
        if(poolManager.GetActiveObjectSize()<objectLimit)
        {
            GameObject gO;
            if (poolManager.CheckEnemyInPool())  //checks if any enemy is avaible which is destroyed once and ready to be reassign
            {
                gO = poolManager.GetObjectFromPool();
                gO.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                gO.transform.rotation = Quaternion.identity;
                gO.gameObject.SetActive(true);                           // if enemy available in pool. Uses it instead of creating a new gameObject
                poolManager.AddNewObjectToActivePool(gO);
            }
            else   
            {
                gO = Instantiate(objectPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity); // if no enemies avaialbe in pool, creates a new enemyObject
                
                poolManager.AddNewObjectToActivePool(gO);
            }

            gO.transform.SetParent(this.transform);
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
            if(!spawnRandomly)
            {
                yield return new WaitForSeconds(minimumTimebetweenObjects);              

            }
            else
            {
                yield return new WaitForSeconds(Random.Range(randWaitMinimum,randWaitMinimum));
              
            }
           
        }
       
    }

    

 
}
