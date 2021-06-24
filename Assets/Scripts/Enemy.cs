using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public Player player;
    private bool _shootOnce = false;

    void Start()
    {
        //InvokeRepeating("Shoot", 1, 3);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, -45f, 0);
    }

    public override void Shoot()
    {
        if(!player.playerState.Equals(PlayerState.PLAYER_DEAD))
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 15f && !_shootOnce)
            {
                base.Shoot();
                _shootOnce = true;
            }
        }
       
       
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

    private void OnDisable()
    {
        _shootOnce = false;
    }

}
