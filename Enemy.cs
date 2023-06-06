using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character, IDamageable
{
    public bool EnemyRespawnable = false;
    public Text enemiesText;
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
