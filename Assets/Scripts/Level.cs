using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
 [SerializeField] int breakableBlocks;
 [SerializeField] int activeEnemies;

    public bool gameFinished = false;
    private void Update()
    {

    }

    public void EndGame()
    {
        gameFinished = true;
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void DecrementBreakableBlocks()
    {
        breakableBlocks--;
    }


    public void CountActiveEnemies()
    {
        activeEnemies++;
    }

    public void DecrementActiveEnemies()
    {
        activeEnemies--;
    }

    
}
