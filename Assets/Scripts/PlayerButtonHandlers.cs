using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonHandlers : MonoBehaviour
{
    [SerializeField]
    private Player player;    
  
    public Button jumpButton;
    public Button duckButton;
    public Button shootButton;
    public int jumpButtonCounter = 0;

    public static PlayerButtonHandlers instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Duck(true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            player.ResetDuck();
        }
    }

    public void Jump()
    {
        jumpButtonCounter++;
        if (jumpButtonCounter < 3)
        {
            player.Jump();
        }

    }

    public void Duck(bool pointerDown)
    {
        if (pointerDown)
        {
            player.Duck();
        }
        else
        {
            player.ResetDuck();
        }

    }

    public void Shoot()
    {
        player.Shoot();
    }



}
