using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletDirection
{
    RIGHT,LEFT
}

public class Bullet : MonoBehaviour
{
   
    public float speed;   
    public BulletDirection direction;
    private Vector3 _direction;

    void Start()
    {
        if (direction == BulletDirection.LEFT)
        {
            _direction = Vector3.left;
        }
        else if (direction == BulletDirection.RIGHT)
        {
            _direction = Vector3.right;
        }

        Destroy(this.gameObject, 5f);
    }   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * Time.deltaTime * speed, Space.World);              
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Character>().TakeDamage(1);
        Destroy(this.gameObject);
    }
}
