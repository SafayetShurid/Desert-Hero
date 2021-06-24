using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent instance;

    public event Action GameSpeedIncrease;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    public IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            GameSpeedIncrease.Invoke();
        }
    }
}
