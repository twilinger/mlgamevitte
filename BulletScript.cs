using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : Character, IDamageable
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public LayerMask enemyLayer = 8;
    public float force;
    public float damage = 3f;
    public float delaySeconds = 5f;
    // Start is called before the first frame update
    private WaitForSeconds cullDelay = null;
    void Start()
    {
        cullDelay = new WaitForSeconds(delaySeconds);
        StartCoroutine(DelayedCull());
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, enemyLayer);
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            IDamageable enemyAttributes = overlappedColliders[i].GetComponent<IDamageable>();
            if (enemyAttributes != null)
            {
                enemyAttributes.ApplyDamage(damage);
            }
        }
    }
    private IEnumerator DelayedCull()
    {
        yield return cullDelay;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
