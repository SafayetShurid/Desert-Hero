using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Collectables")
            || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            collision.transform.GetComponentInParent<PoolManager>().AddNewEnemyToIdlePool(collision.gameObject);
            collision.transform.GetComponentInParent<PoolManager>().RemoveObjectFromPool(collision.gameObject);
        }
        

    }


}
