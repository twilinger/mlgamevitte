using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character, IDamageable
{
    public bool chase = false;
    private GameObject player;
    public bool EnemyRespawnable = false;
    public Text enemiesText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (chase == true)
        {
            Chase();
        }
        Flip();
    }
        private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            //Debug.Log("enemies before" + EnemiesSlain);
            EnemiesSlain++;
            enemiesText.text = "Enemies Slain: " + EnemiesSlain;
            Die(EnemyRespawnable);
        }
    }

}
