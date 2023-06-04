using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    private Camera mainCam;
    private Vector3 mousePos;
    //public GameObject RotationPoint;
    // Update is called once per frame
    private void Start()
    {
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 rotation = mousePos - transform.position;
        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }

    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }



}
