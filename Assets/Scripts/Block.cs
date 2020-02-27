using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    AudioClip breakSound;

    [SerializeField]
    GameObject blockSparklesVFX;

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
            DestroyBlock();
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
        return gameObject.tag == "Breakable";
    }

}
