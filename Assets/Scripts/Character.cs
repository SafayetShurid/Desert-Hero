using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Basic Attributes")]
    public int health;
    public string characterName;

    [Header("Movement")]
    [SerializeField]
    protected float moveSpeed;


    [Header("Shooting")]
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected Transform shootingPosition;
    [SerializeField]
    protected BulletDirection bulletDirection;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected float bulletDamage;
  

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPosition.position, bulletPrefab.transform.rotation);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.GetComponent<Bullet>().direction = bulletDirection;

    }

    public virtual void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime,Space.World);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
