using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [SerializeField] float speed = 5f;
    [SerializeField] float followRadius, attackRadius, meleeAttackRadius;

    [SerializeField] int amoutOfDamage = 20;
    public int currentHP = 0;
    [SerializeField] int maxHP = 50;

    [SerializeField] Transform pos1, pos2;
    [SerializeField] Transform attackPos;

    private int direction;

    private bool isDied = false;

    Rigidbody2D rb;
    Animator animator;
    Collider2D collider;
    Collider2D playerCollider; 

    [SerializeField] AudioSource deathSfx;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        playerCollider = PlayerController.Instance.playerCollider;

        currentHP = maxHP;
        direction = (int)transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDied || PlayerController.Instance.isDied) { return; }
        Vector3 playerPos = PlayerController.Instance.transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        EnemyMvt(playerPos, distance);

    }

    private void EnemyMvt(Vector3 playerPos, float distance)
    {

        //float distance = (playerPos - transform.position).magnitude;
        if (distance <= followRadius && distance > attackRadius)
        {


            if (Mathf.Abs(playerPos.x - transform.position.x) >= Mathf.Abs(attackPos.position.x - transform.position.x))
            {
   
                if (playerPos.x < transform.position.x)
                {
                    direction = -1;
                    ChangeDirection(direction);

                }
                else if (playerPos.x > transform.position.x)
                {
                    direction = 1;
                    ChangeDirection(direction);
                }
            }



            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
            animator.SetBool("Run", true);



        }

        else if (distance <= attackRadius)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Run", false);
            animator.SetTrigger("Attack");

        }
        else
        {
            animator.SetBool("Run", false);


        }

        if ((transform.position.x >= pos1.position.x && direction == 1) || (transform.position.x <= pos2.position.x && direction==-1))
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Run", false);
        }

    }

    public void Attack()
    {
        if (PlayerController.Instance.isDied) { return; }
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPos.position, meleeAttackRadius, LayerMask.GetMask("Player"));
        if(hitPlayer)
        {
            PlayerController.Instance.TakeDamage(amoutOfDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        animator.SetTrigger("Hit");
        if (currentHP <= 0)
        {
            Die();
        }
        UIMenager.instance.UpdateHpEnemy(currentHP);

    }

    private void Die()
    {
       
        isDied = true;
        animator.SetTrigger("Die");
        Physics2D.IgnoreCollision(collider, playerCollider, true);
        UIMenager.instance.ShowEnemyHpSlider(false, currentHP);
        deathSfx.Play();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void ChangeDirection(int dir)
    {
        
        transform.localScale = new Vector3(dir, 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIMenager.instance.ShowEnemyHpSlider(true, currentHP);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIMenager.instance.ShowEnemyHpSlider(false, currentHP);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, followRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRadius) ;
        Gizmos.color = Color.green;
    }
  
    public int MaxHp() { return maxHP; }
    



}
