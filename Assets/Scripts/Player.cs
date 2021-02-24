using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float xMin, xMax;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float padding;
    [SerializeField] float projectileSpeed =10f;
    [SerializeField] float shootingDelay;


    //state variables

    private bool allowFire = true;

    // Start is called before the first frame update
    void Start()
    {
        SetTheScreenBoundaries();
    }


 

    // Update is called once per frame
    void Update()
    {
       
        Move();
        if (Input.GetButtonDown("Fire1") && allowFire)
        {
            StartCoroutine(Fire());
        }
       

    }

   
    IEnumerator Fire()
    {
        
        {
            allowFire = false;
            GameObject laser = Instantiate(laserPrefab,transform.position,Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(shootingDelay);
            allowFire = true;
        }
    }

    private void Move()
    {
        var movePosX = transform.position.x + Input.GetAxis("Horizontal")*Time.deltaTime*speed;
        

        //compute Clamp value
        float xPos = Mathf.Clamp(movePosX, xMin, xMax);
        //Debug.Log(movePosX);
        //move to the desired position
        transform.position = new Vector2(xPos, transform.position.y);
    }

    private void SetTheScreenBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-padding;

    }
}
