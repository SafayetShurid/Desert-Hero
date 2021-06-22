using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    private int idlePoolSize;
    [SerializeField]
    private int activePoolSize;
    private Queue<GameObject> idleObjectPoolObjects;  //Objects which are ready to respawn
    private List<GameObject> activePoolObjects; //Objects which are in the scene

   
    void Start()
    {
        
        idleObjectPoolObjects = new Queue<GameObject>(idlePoolSize);
        activePoolObjects = new List<GameObject>(activePoolSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckEnemyInPool()
    {
      
        if (idleObjectPoolObjects.Count > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddNewObjectToActivePool(GameObject obj)
    {
        if(activePoolObjects.Count>activePoolSize)
        {
            Debug.Log("Active Pool size exceeded. Please incease size");
        }
        else
        {
            activePoolObjects.Add(obj);
        }
      
    }

    public void AddNewEnemyToIdlePool(GameObject obj)
    {
        if (idleObjectPoolObjects.Count > idlePoolSize)
        {
            Debug.Log("Idle Pool size exceeded. Please incease size");
        }
        else
        {
            idleObjectPoolObjects.Enqueue(obj);
        }
    }

    public void RemoveObjectFromPool(GameObject obj)
    {
        activePoolObjects.Remove(obj);
    }

    public GameObject GetObjectFromPool()
    {
        return idleObjectPoolObjects.Dequeue();
    }

    public List<GameObject> GetActiveObjects()
    {
        return activePoolObjects;
    }

    public int GetActiveObjectSize()
    {
       
        return activePoolObjects.Count;
      
    }
}
