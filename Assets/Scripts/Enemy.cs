using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = -10f;




    Level level;
    EnemyFleet row;
    



    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountActiveEnemies();
        row = FindObjectOfType<EnemyFleet>();
        int rand = UnityEngine.Random.Range(5, 15);
        Invoke("Fire", rand);



    }

    // Update is called once per frame
    void Update()
    {

        
        //StartCoroutine(Fire());


    }

    private void Fire()
    {

        {
            int rand = UnityEngine.Random.Range(5, 15);
            //row.allowFire = false;
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            Invoke("Fire", rand);
            //row.allowFire = true;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision);

    }

    private void HandleHit(Collision2D collision)
    {

        Destroy(gameObject);
        row.RemoveOnDestruction(this);
        level.DecrementActiveEnemies();
    }
}

   

