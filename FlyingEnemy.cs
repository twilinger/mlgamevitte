using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy: Character, IDamageable
{

    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    public float RangedAttackDamage = 3f;


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
        if (chase==true)
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
        //это говно надо подправить чтобы он после атаки двигался назад
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
    private void OnCollisionEnter(Collision collision)
    {

            if (CurrentHealth <= 0)
            {
                Die();
            }
    }

}
