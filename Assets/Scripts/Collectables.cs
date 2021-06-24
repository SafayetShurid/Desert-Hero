using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void Start()
    {
        GameEvent.instance.GameSpeedIncrease += SpeedIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 1f, 0f,Space.Self);
        transform.Translate(Vector3.left * Time.deltaTime* _speed,Space.World);
    }

    public void SpeedIncrease()
    {       
        _speed += 1;
    }
}
