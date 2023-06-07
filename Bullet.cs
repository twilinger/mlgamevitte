using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    private Vector3 mousePos;
    private Camera mainCam;
    public int Bullet_damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        //rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        FlyingEnemy flenemy = hitInfo.GetComponent<FlyingEnemy>();
        if (enemy != null)
        {
            //Debug.Log(Bullet_damage, hitInfo);
            enemy.ApplyDamage(Bullet_damage);
        }
        if (flenemy != null)
        {
            //Debug.Log(Bullet_damage, hitInfo);
            flenemy.ApplyDamage(Bullet_damage);
        }
        if (hitInfo.transform.gameObject.CompareTag("Enemy") || hitInfo.name == "FlyingEnemy" || hitInfo.name == "Tilemap")
        {
            Destroy(gameObject);
        }
          
    }


}
