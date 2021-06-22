using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField]
    private int idlePoolSize;
    [SerializeField]
    private int activePoolSize;
    private Queue<GameObject> idleEnemyPoolObjects;  //Objects which are ready to respawn
    private List<GameObject> activeEnemyPoolObjects; //Objects which are in the scene

    public static EnemyPoolManager instance;

    void Start()
    {
        instance = this;
        idleEnemyPoolObjects = new Queue<GameObject>(idlePoolSize);
        activeEnemyPoolObjects = new List<GameObject>(activePoolSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckEnemyInPool()
    {
      
        if (idleEnemyPoolObjects.Count > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddNewEnemyToActivePool(GameObject enemy)
    {
        if(activeEnemyPoolObjects.Count>activePoolSize)
        {
            Debug.Log("Active Pool size exceeded. Please incease size");
        }
        else
        {
            activeEnemyPoolObjects.Add(enemy);
        }
      
    }

    public void AddNewEnemyToIdlePool(GameObject enemy)
    {
        if (idleEnemyPoolObjects.Count > idlePoolSize)
        {
            Debug.Log("Idle Pool size exceeded. Please incease size");
        }
        else
        {
            idleEnemyPoolObjects.Enqueue(enemy);
        }
    }

    public void RemoveEnemyFromPool(GameObject enemy)
    {
        activeEnemyPoolObjects.Remove(enemy);
    }

    public GameObject GetEnemyFromPool()
    {
        return idleEnemyPoolObjects.Dequeue();
    }

    public List<GameObject> GetActiveEnemies()
    {
        return activeEnemyPoolObjects;
    }

    public int GetActiveEnemySize()
    {
       
        return activeEnemyPoolObjects.Count;
      
    }
}
