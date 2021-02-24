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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        //print(enemies.Count);

        float xEnemyPos;
        float xEnemyRowPos = transform.position.x;
        float yEnemyRowPos = transform.position.y;

        //Store reference to last and first enemy in a row
        Enemy firstEnemy = enemies[0];
        Enemy lastEnemy = enemies[enemies.Count - 1];

        if (yEnemyRowPos == bottomLimit || level.gameFinished)
        {
           
            CancelInvoke("Move");
        }

        //move row based and the current direction
        if (!reverse)
        {
           xEnemyPos = lastEnemy.transform.position.x;
           MoveForward(xEnemyRowPos);
        
        }
        else
        {
            xEnemyPos = firstEnemy.transform.position.x;
            MoveBackward(xEnemyRowPos);
        }

        //make decision if we need to go down and reverse.
        if ((int)xEnemyPos == (int)xMax)
        {

            MoveDown(xEnemyRowPos);
            
            reverse = true;
        }else if ((int)xEnemyPos == (int)xMin)
        { 
            MoveDown(xEnemyRowPos);
            reverse = false;
        }




       
        
        //float movePosX = transform.position.x + moveSpeed;
        //float xPos = Mathf.Clamp(movePosX, xMin, xMax);


        
        
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

