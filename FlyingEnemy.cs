using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : Character, IDamageable
{

    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    public float RangedAttackDamage = 3f;
    public bool FlEnemyRespawnable = false;
    public Text enemiesText;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
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
        //else
        //{
        //он вернется на стартовую позицию(я передумал, он не будет возвращаться)
        //    ReturnStartPoint();
        //}
        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //private void ReturnStartPoint()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, startingPoint.transform.position, speed * Time.deltaTime);

    //}

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
            if (CurrentHealth <= 0)
            {
                //Debug.Log("enemies before" + EnemiesSlain);
                EnemiesSlain++;
                enemiesText.text = "Enemies Slain: " + EnemiesSlain;
                Die(FlEnemyRespawnable);
            }
        }
    }
}
