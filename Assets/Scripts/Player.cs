using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{

    private Rigidbody rb;

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
    int counter = 0;

    [SerializeField]
    private int _totalCactus = 0;   

    [Header("UI")]
    [SerializeField]
    private Text cactusCounterText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0 && !_isGrounded)
        {
            // rb.velocity += Vector3.up * Physics.gravity.y * (_jumpFallMultiplier*Time.fixedDeltaTime);
            //rb.velocity += (Vector3.up * (_jumpFallMultiplier));
            //rb.AddForce(Physics.gravity * _jumpFallMultiplier, ForceMode.Acceleration);
            _animator.SetTrigger("Falling");
        }
        //Debug.Log(rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0 && !_isGrounded)
        {
            // rb.velocity += Vector3.up * Physics.gravity.y * (_jumpFallMultiplier*Time.fixedDeltaTime);
            //rb.velocity += (Vector3.up * (_jumpFallMultiplier));
            //rb.AddForce(Physics.gravity * _jumpFallMultiplier, ForceMode.Acceleration);
            _animator.SetBool("isFalling", true);
            _animator.SetBool("isJumping", false);
            Debug.Log(rb.velocity.y);
        }
       
        //  Debug.Log(Time.fixedDeltaTime);

        if (Time.time<1f)
        {
            counter++;
           
        }

        

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

    }

    public void Jump()
    {
      
        try
        {           
            if (_isGrounded && !_isFirstJump)
            {
                 rb.AddForce(Vector3.up * _jumpForce);               
                _isGrounded = false;
                _isFirstJump = true;
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isFalling", false);
            }

            else if (!_isGrounded && _isFirstJump)
            {                

                rb.AddForce(Vector3.up * (_jumpForce/1.5f));
                _isFirstJump = false;
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isFalling", false);
            }
        }

        catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log("Most Probably Jump button called after Player is Destroyed");
        }
       

    }

    public void Duck()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = 0.5f;
        transform.localScale = newScale;

        Vector3 newPosition = transform.localPosition;
        newPosition.y = -0.5f;
        transform.localPosition = newPosition;
    }

    public void ResetDuck()
    {
        transform.localScale = Vector3.one *0.7f;
        transform.localPosition = _initialPosition;
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
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _isFirstJump = false;
            PlayerButtonHandlers.instance._jumpButtonCounter = 0;
            _animator.SetTrigger("FellDown");
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
