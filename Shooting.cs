using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float damage = 3f;
    private float shootingDelay;
    public LayerMask enemyLayer = 8;
    public LayerMask bulletLayer = 7;
    // Start is called before the first frame update
        void Start()
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        // Update is called once per frame
        void Update()
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer > shootingDelay)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && canFire)
            {
                canFire = false;
                Debug.Log("Player attempting Ranged attack");
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
        }
    }
