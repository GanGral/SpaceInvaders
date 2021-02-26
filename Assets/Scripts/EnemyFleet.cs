using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleet : MonoBehaviour
{
    //config items
    [SerializeField] List<Enemy> enemies;
    [SerializeField] float padding;
    [SerializeField] float moveDistance;
    [SerializeField] float xMin, xMax;
    [SerializeField] float moveSpeed;
    [SerializeField] float bottomLimit;

    Level level;
    Vector3 fleetSize;



    //aux variables
    bool reverse = false;
    public bool allowFire = true;
   // float distCentBorder;






    // Start is called before the first frame update
    void Start()
    {
        //caching
        Enemy[] enemyArray = gameObject.GetComponentsInChildren<Enemy>();
        fleetSize = gameObject.GetComponent<Collider2D>().bounds.size;
        level = FindObjectOfType<Level>();

        //setting up
        enemies.AddRange(enemyArray);
        SetTheScreenBoundaries();

        //start moving
        InvokeRepeating("Move", 1f, moveSpeed);



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        
        if (transform.position.y <= bottomLimit)
        {
            print("Hitting bottom limit");
            CancelInvoke("Move");
            return;
        }

        if (reverse)
        {
            print("Moving backwards");

            MoveBackward();
            //return;
        }

        if (!reverse)
        {
            print("Moving forward");

            MoveForward();
           // return;
        }

        CheckBoundariesAndMoveDown();
        




        
    }

    private void CheckBoundariesAndMoveDown()
    {
        if (transform.position.x >= xMax-fleetSize.x)
        {
            print("hit right border");

            reverse = true;
            MoveDown();

        }
        else if (transform.position.x <= xMin+fleetSize.x)
        {
            print("hit left border");

            reverse = false;
            MoveDown();
        }
    }

    private void MoveDown()
    {
        
        print("Moving down");
        float newPos = Mathf.Clamp(transform.position.y - moveDistance, bottomLimit+fleetSize.y,999);
        transform.position = new Vector3(transform.position.x, newPos);

    }

    private void MoveForward()
    {
        //we need to check here not to go beyong the limit
        float newPos = Mathf.Clamp(transform.position.x+moveDistance, xMin, xMax - fleetSize.x);
        transform.position = new Vector3(newPos, transform.position.y);

    }
    private void MoveBackward()
    {
        float newPos = Mathf.Clamp(transform.position.x - moveDistance, xMin + fleetSize.x, xMax);
        transform.position = new Vector3(newPos, transform.position.y);
    }

    private void SetTheScreenBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    }


    public void RemoveOnDestruction(Enemy enemy)
    {
        
        try
        {
            
            enemies.Remove(enemy);
            print("are?");
           
        }
        catch (Exception e)
        {
            print("are we still going?");
            print(e.Message);
        }
        
    }
}

