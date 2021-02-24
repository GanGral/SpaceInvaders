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






    // Start is called before the first frame update
    void Start()
    {
        Enemy[] enemyArray = gameObject.GetComponentsInChildren<Enemy>();
        enemies.AddRange(enemyArray);
        level = FindObjectOfType<Level>();
        SetTheScreenBoundaries();
        InvokeRepeating("Move", 1f, moveSpeed);

        fleetCollider = gameObject.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {

        if ((int)fleetCollider.bounds.max.x == (int)xMax)
        {
            reverse = true;
        }

        if ((int)fleetCollider.bounds.max.x == (int)xMin)
        {
            reverse = false;
        }

        if ((int)fleetCollider.bounds.max.y == (int)bottomLimit)
        {
            CancelInvoke("Move");
        }

        if (reverse)
        {

            MoveBackward(fleetCollider.transform.position.x);
        }

       if (!reverse)
        {
            MoveForward(fleetCollider.transform.position.x);
        }
        
    }

    private void MoveDown(float pos)
    {
        transform.position = new Vector3(pos, transform.position.y - 1);
    }

    private void MoveForward(float pos)
    {
        transform.position = new Vector3(pos + moveDistance, transform.position.y);
    }
    private void MoveBackward(float pos)
    {
        transform.position = new Vector3(pos - moveDistance, transform.position.y);
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

