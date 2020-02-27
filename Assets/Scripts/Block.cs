﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits = 1;
    [SerializeField] Sprite[] hitSprites;

    private int timeHits = 0;

    Level level;
    GameSessions gameSession;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSessions>();
        CountBreakableBlock();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsBreakable())
        {
            timeHits++;
            if(timeHits >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        TriggerSparklesVFX();
        gameSession.AddToScore();
        level.BlockDestroyed();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

    private void CountBreakableBlock()
    {
        if (IsBreakable())
        {
            level.CountBlock();
        }
    }

    private bool IsBreakable()
    {
        return tag == "Breakable";
    }

    private void ShowNextHitSprite()
    {
        int index = timeHits - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[index];
    }

}
