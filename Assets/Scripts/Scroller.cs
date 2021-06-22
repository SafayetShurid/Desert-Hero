using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*_speed*Time.deltaTime);
        if(transform.position.x<=-39.9f)
        {
            transform.position = new Vector3(40f, transform.position.y, transform.position.z);
        }
    }
}
