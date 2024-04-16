using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] float mtvSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float attackRadius = 0f;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;
    [SerializeField] int amountOfDamage = 10;
    [SerializeField] int currentCoin;

    [SerializeField] AudioSource attackSfx, runningSfx, jumpingSfx,gettingHitSfx, deathSfx, coinSfx, doorTransitionSfx;

    private bool onGround;


    [SerializeField] Transform[] platform;
    [SerializeField] Transform attackPos;

    private Transform transitionPos;

    private float timeBetweenAttacks = 1.5f; 
    private float nextAttackTime = 0f;

    private bool canMove = true;
    private bool invinsible = false;
    public bool isDied = false;

    private bool moving = false;
    private bool sfxIsPlaying = false;

    Rigidbody2D rb;
    Animator playerAnimator;
    public Collider2D playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        rb=GetComponent<Rigidbody2D>();
        playerAnimator=GetComponent<Animator>();
        playerCollider=GetComponent<Collider2D>();

        transitionPos = transform;
        currentHP = maxHP;
        currentCoin = 0;
   
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.P)) { UIMenager.instance.PauseResume(); }

            PlayerMovement();
            PlayerJump();
            PlayerAttack();
            EnterDoor();
        }
       

       



    }


    private void EnterDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(transitionPos.position != transform.position)
            {
                doorTransitionSfx.Play();
            }
            transform.position = transitionPos.position;
           

        }
    }

    private void PlayerAttack()
    {
        if (!canMove) { return; }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                playerAnimator.SetTrigger("Attack");
                attackSfx.Play();

                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, LayerMask.GetMask("Enemy"));
                

                foreach (Collider2D enemy in hitEnemy)
                {
                    if (enemy.GetType() == typeof(CapsuleCollider2D)) 
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(amountOfDamage);
                    }
                    
                }

                nextAttackTime = Time.time + timeBetweenAttacks; 
            }
        }

    }



    private void PlayerJump()
    {
        if (!canMove) { return; }
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnimator.SetTrigger("Jump");
            jumpingSfx.Play() ;
            onGround = false;
        }
    }

    
    private void PlayerMovement()
    {
       
        float horizontalValue = Input.GetAxisRaw("Horizontal");
        if (!canMove )
        {
            horizontalValue = 0;

        }

        rb.velocity = new Vector2(horizontalValue * mtvSpeed, rb.velocity.y);


        if (Math.Abs(horizontalValue) < Mathf.Epsilon)
        {
            playerAnimator.SetBool("Run", false);
            moving = false;

        }
        else
        {

            playerAnimator.SetBool("Run", true);
            moving = true;

        }

        if (horizontalValue > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalValue < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
       
        

        if(moving && onGround && !sfxIsPlaying)
        {
            runningSfx.Play();
            sfxIsPlaying = true;
        }
        else if(Math.Abs(horizontalValue) == 0 || !onGround )
        {
            runningSfx.Stop();
            sfxIsPlaying = false;
        }
      


    }

    public void TakeDamage(int amout)
    {
        
        canMove = false;
        playerAnimator.SetTrigger("Hit");
       
        if (!invinsible)
        {
            currentHP -= amout;
            gettingHitSfx.Play();
            if (currentHP <= 0)
            {
                Die();
            }
            invinsible = true;
            UIMenager.instance.UpdateHPUI();
        }
       
        StartCoroutine(WaitToMove());
        StartCoroutine(Invinsiblity());

    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.7f);
        canMove = true;
        
    }
    IEnumerator Invinsiblity()
    {
        yield return new WaitForSeconds(1f);
        invinsible = false;
    }

    private void Die()
    {
        isDied = true;
        moving = false;
        playerAnimator.SetTrigger("Die");
        deathSfx.Play();
        UIMenager.instance.GameOver();

    }


    public void AddCoin(int amount)
    {
        currentCoin += amount;
        UIMenager.instance.UpdateCoinText();
        coinSfx.Play();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") || collision.CompareTag("Elevator"))
        {
            onGround = true;
        }

        if (collision.CompareTag("Elevator"))
        {
            int currentPlatformIndex = collision.GetComponent<ElevatorMvt>().Id();
            transform.parent = platform[currentPlatformIndex];
            
        }

        if (collision.CompareTag("OutOfWorld"))
        {
            Die();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Elevator"))
        {
            transform.parent = null;
        }
    }

    public void TatgetPos(Transform pos)
    {
        transitionPos = pos; 
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
        Gizmos.color = Color.green;
    }


    public int MaxHp() { return maxHP; }
    public int CurrentHp() { return currentHP; }
    public int CurrentCoin() { return currentCoin; }

    public void StropSFX()
    {
        runningSfx.Stop();
    }
}



