using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Movement")]
    public float speed;    

    void Start()
    {
        InvokeRepeating("Shoot", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            this.transform.GetComponentInParent<PoolManager>().AddNewEnemyToIdlePool(this.gameObject);
            this.transform.GetComponentInParent<PoolManager>().RemoveObjectFromPool(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    
}
