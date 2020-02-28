using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    private int maxHits = 0;
    private int timeHits = 0;

    Level level;
    GameSessions gameSession;

    private void Start()
    {
        maxHits = hitSprites.Length + 1;
        level = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSessions>();
        CountBreakableBlock();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakable())
        {
            timeHits++;
            if (timeHits >= maxHits)
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
        if (hitSprites[index] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[index];
        }
    }

}
