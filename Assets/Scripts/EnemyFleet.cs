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
    Collider2D fleetCollider;



    //aux variables
    bool reverse = false;
    public bool allowFire = true;
    float distCentBorder;






    // Start is called before the first frame update
    void Start()
    {
        Enemy[] enemyArray = gameObject.GetComponentsInChildren<Enemy>();
        fleetCollider = gameObject.GetComponent<Collider2D>();
        enemies.AddRange(enemyArray);
        level = FindObjectOfType<Level>();
        SetTheScreenBoundaries();
        InvokeRepeating("Move", 1f, moveSpeed);
        distCentBorder = Math.Abs(fleetCollider.transform.position.x - transform.position.x);

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        
        if ((int)fleetCollider.bounds.max.y == (int)bottomLimit)
        {
            print("Hitting bottom limit");
            CancelInvoke("Move");
            return;
        }

        if (reverse)
        {
            print("Moving backwards");

            MoveBackward(fleetCollider.transform.position.x);
            //return;
        }

        if (!reverse)
        {
            print("Moving forward");

            MoveForward(fleetCollider.transform.position.x);
           // return;
        }

        CheckBoundariesAndMoveDown();
        




        
    }

    private void CheckBoundariesAndMoveDown()
    {
        if (fleetCollider.bounds.max.x+padding >= xMax)
        {
            print("hit right border");

            reverse = true;
            MoveDown(fleetCollider.transform.position.x);

        }
        else if (fleetCollider.bounds.min.x+padding <= xMin)
        {
            print("hit left border");

            reverse = false;
            MoveDown(fleetCollider.transform.position.x);
        }
    }

    private void MoveDown(float pos)
    {
        
        print("Moving down");
        float targetPosY = Mathf.Clamp(transform.position.y - 1, bottomLimit, 999);
        transform.position = new Vector3(pos, targetPosY);
    }

    private void MoveForward(float pos)
    {
        //we need to check here not to go beyong the limit
        float targetPosX = Mathf.Clamp(pos+ distCentBorder + moveDistance, xMin, xMax);
        transform.position = new Vector3(targetPosX, transform.position.y);
    }
    private void MoveBackward(float pos)
    {
        float targetPosX = Mathf.Clamp(pos- distCentBorder - moveDistance, xMin, xMax);
        transform.position = new Vector3(targetPosX, transform.position.y);
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

