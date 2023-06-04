using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageable
{
    [Header("Input")]
    public KeyCode MeleeAttackKey = KeyCode.Mouse0;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode RangedAttackKey = KeyCode.Mouse1;
    public string xMoveAxis = "Horizontal";

    [Header("Combat")]
    public Transform MeleeAttackOrigin = null;
    //public Transform RangedAttackOrigin = null;
    //public GameObject projectile = null;
    public float MeleeAttackRadius = 0.6f;
    public float MeleeDamage = 2f;
    public float MeleeAttackDelay = 1.1f;
    //public float RangedAttackDamage = 3f;
    //public float RangedAttackDelay = 0.5f;
    public LayerMask enemyLayer = 8;

    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float damage = 3f;
    private float shootingDelay;



    private float moveIntentionX = 0;
    private bool tryJump = false;
    private bool tryMeleeAttack = false;
    private float timeUntilMeleeReady = 0;
    private bool isMeleeAttacking = false;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        GetInput();

        HandleJump();
        HandleMeleeAttack();
        HandleAnimations();

    }

    void FixedUpdate()
    {
        HandleRun();
    }

    void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, -Vector2.up * groundLeeway, Color.red);
        if (MeleeAttackOrigin != null)
        {
            Gizmos.DrawWireSphere(MeleeAttackOrigin.position, MeleeAttackRadius);
        }
    }
    
    private void GetInput()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        moveIntentionX = Input.GetAxis(xMoveAxis);
        tryMeleeAttack = Input.GetKeyDown(MeleeAttackKey);
        //tryRangedAttack = Input.GetKeyDown(RangedAttackKey);
        tryJump = Input.GetKeyDown(jumpKey);
    }
    private void HandleRun()
    {
        
        if (moveIntentionX > 0 && transform.rotation.y == 0 && !isMeleeAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (moveIntentionX < 0 && transform.rotation.y != 0 && !isMeleeAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        Rb2D.velocity = new Vector2(moveIntentionX * speed, Rb2D.velocity.y);
    }

    private void HandleJump()
    {
        if (tryJump && CheckGrounded())
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
        }
    }

    private void HandleMeleeAttack()
    {
        if (tryMeleeAttack && timeUntilMeleeReady <= 0)
        {
            Debug.Log("Player attempting melee attack");
            Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(MeleeAttackOrigin.position, MeleeAttackRadius, enemyLayer);
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                IDamageable enemyAttributes = overlappedColliders[i].GetComponent<IDamageable>();
                if (enemyAttributes != null)
                {
                    enemyAttributes.ApplyDamage(MeleeDamage);
                }
            }
            timeUntilMeleeReady = MeleeAttackDelay;
        }
        else
        {
            timeUntilMeleeReady -= Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.Mouse1) && canFire)
        //{
        //    canFire = false;
        //    Debug.Log("Player attempting Ranged attack");
        //    Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        //}

    }

    private void HandleAnimations()
    {
        Animator.SetBool("IsGrounded", CheckGrounded());
        if (tryMeleeAttack)
        {
            if(!isMeleeAttacking)
            {
                StartCoroutine(MeleeAttackAnimDelay());
            }
        }


        if (tryJump && CheckGrounded() || Rb2D.velocity.y > 1f)
        {
            if(!isMeleeAttacking)
            {
                Animator.SetTrigger("IsJumping");
            }
            
        }

        if (Mathf.Abs(moveIntentionX) > 0.1f && CheckGrounded())
        {
            Animator.SetInteger("AnimState", 1);
        }
        else
        {
            Animator.SetInteger("AnimState", 0);
        }



    }
    private IEnumerator MeleeAttackAnimDelay()
    {
        Animator.SetTrigger("IsMeleeAttacking");
        isMeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackDelay);
        isMeleeAttacking = false;
    }





}

