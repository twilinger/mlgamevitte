using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : Character
{
    private Animator anim;
    public Rigidbody2D rb;
    private int PlayerLives = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void PlayerDeath()
    {
        rb.bodyType = RigidbodyType2D.Static;
        EnemiesSlain = 0;
        anim.SetTrigger("Dead");

    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player went into the ENEMY");
            PlayerDeath();
        }
    }
   
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
