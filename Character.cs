using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Attributes")]
    public float healthPool = 10f;
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundLeeway = 0.1f;

    private Rigidbody2D rb2D = null;
    private Animator animator = null;
    private float currentHealth = 10f;
    public GameObject EnemyClone;
    
    
    public bool respawnable = false;
    public static int EnemiesSlain = 0;
    public static int NeedEnemiesSlain;



    public Rigidbody2D Rb2D
    {
        get { return rb2D; }
        protected set { rb2D = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        protected set { currentHealth = value; }
    }

    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Rigidbody2D>())
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

        currentHealth = healthPool;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    protected bool CheckGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, groundLeeway);
    }
    protected virtual void Die(bool respawnable)
    {
        gameObject.SetActive(false);
        if (respawnable)
        {
            Invoke("Respawn", 3);
        }
    }
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die(respawnable);
        }
    }
    void Respawn()
    {
        CurrentHealth = healthPool;
        gameObject.SetActive(true);
    }
    


}
