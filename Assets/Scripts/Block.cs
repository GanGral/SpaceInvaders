using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] int maxHits = 3;
    [SerializeField] int hitsTaken;
    [SerializeField] Sprite[] hitSprites;

    Level level;
    // Start is called before the first frame update
    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision);

    }

    private void HandleHit(Collision2D collision)
    {
        hitsTaken++;
        if (hitsTaken >= maxHits)
        {
            Destroy(gameObject);
            level.DecrementBreakableBlocks();
        }
        else
        {
            ShowNextSprite();
        }
        Destroy(collision.gameObject);
    }

    private void ShowNextSprite()
    {
        int spriteIndex = hitsTaken - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }
}
