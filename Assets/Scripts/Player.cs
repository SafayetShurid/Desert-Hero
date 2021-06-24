using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PlayerState
{
    PLAYER_DEAD,
    PLAYER_JUMP,
    PLAYER_DOUBLE_JUMP,
    PLAYER_RUNNING,
    PLAYER_DUCK,
    PLAYER_SHOOT

}

public class Player : Character
{

    private Rigidbody _rb;

    public PlayerState playerState = PlayerState.PLAYER_RUNNING;

    [Header("Jumping")]
    [SerializeField]
    [Range(0, 1000)]
    private float _jumpForce;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isFirstJump;
    [SerializeField]
    [Range(0.1f,10)]
    private float _jumpFallMultiplier;

    private Vector3 _initialPosition;
    private Vector3 _jumpVelocity = Vector3.zero;
    private Animator _animator;
    
    [Header("Stats")]
    [SerializeField]
    private int _totalCactus = 0;
   

    [Header("UI")]
    [SerializeField]
    private Text cactusCounterText;
    [SerializeField]
    private Text healthText;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.y < 0 && !_isGrounded)
        {
            // _rb.velocity += Vector3.up * Physics.gravity.y * (_jumpFallMultiplier*Time.fixedDeltaTime);
            //_rb.velocity += (Vector3.up * (_jumpFallMultiplier));
            //_rb.AddForce(Physics.gravity * _jumpFallMultiplier, ForceMode.Acceleration);
          
        }
        //Debug.Log(_rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.y < 0 && !_isGrounded)
        {
            // _rb.velocity += Vector3.up * Physics.gravity.y * (_jumpFallMultiplier*Time.fixedDeltaTime);
            //_rb.velocity += (Vector3.up * (_jumpFallMultiplier));
            //_rb.AddForce(Physics.gravity * _jumpFallMultiplier, ForceMode.Acceleration);
            _animator.SetBool("isFalling", true);
            _animator.SetBool("isJumping", false);
           
        }
       
       

        

        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);

    }

    public void Jump()
    {
      
        try
        {           
            if (_isGrounded && !_isFirstJump)
            {
                playerState = PlayerState.PLAYER_JUMP;
                 _rb.AddForce(Vector3.up * _jumpForce);               
                _isGrounded = false;
                _isFirstJump = true;
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isFalling", false);
            }

            else if (!_isGrounded && _isFirstJump)
            {
                playerState = PlayerState.PLAYER_DOUBLE_JUMP;
                _rb.AddForce(Vector3.up * (_jumpForce/1.5f));
                _isFirstJump = false;
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isFalling", false);
            }

            PlayerButtonHandlers.instance.duckButton.interactable = false;
        }

        catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log("Most Probably Jump button called after Player is Destroyed");
        }
       

    }

    public void Duck()
    {        
        if(playerState.Equals(PlayerState.PLAYER_RUNNING))
        {
            Vector3 newScale = transform.localScale;
            newScale.y = 0.35f;
            transform.localScale = newScale;

            Vector3 newPosition = transform.localPosition;
            newPosition.y = -0.35f;
            transform.localPosition = newPosition;
            playerState = PlayerState.PLAYER_DUCK;
        }
        
    }

    public void ResetDuck()
    {
        if (playerState.Equals(PlayerState.PLAYER_DUCK))
        {
            transform.localScale = Vector3.one * 0.7f;
            transform.localPosition = _initialPosition;
            playerState = PlayerState.PLAYER_RUNNING;
        }
            
    }

    public override void Shoot()
    {
        if(!(_totalCactus<=0))
        {
            base.Shoot();
            DecreaseCactus(1);
        }
      
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthText.text = health.ToString();
        if (health <= 0)
        {
            playerState = PlayerState.PLAYER_DEAD;
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            collision.transform.GetComponentInParent<PoolManager>().AddNewEnemyToIdlePool(collision.gameObject);
            collision.transform.GetComponentInParent<PoolManager>().RemoveObjectFromPool(collision.gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            playerState =  playerState.Equals(PlayerState.PLAYER_DUCK) ? PlayerState.PLAYER_DUCK : PlayerState.PLAYER_RUNNING;
            _isGrounded = true;
            _isFirstJump = false;
            PlayerButtonHandlers.instance.jumpButtonCounter = 0;
            PlayerButtonHandlers.instance.duckButton.interactable = true;          
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }      
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            IncreaseCactus(1);           
            collision.transform.GetComponentInParent<PoolManager>().AddNewEnemyToIdlePool(collision.gameObject);
            collision.transform.GetComponentInParent<PoolManager>().RemoveObjectFromPool(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    private void IncreaseCactus(int n)
    {
        _totalCactus += n;
        cactusCounterText.text = _totalCactus.ToString();
    }

    private void DecreaseCactus(int n)
    {
        _totalCactus -= n;
        cactusCounterText.text = _totalCactus.ToString();
    }


}
