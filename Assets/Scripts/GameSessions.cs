using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessions : MonoBehaviour
{
    [Range(0.1f,10f)]
    [SerializeField]
    float gameSpeed = 1f;

    [SerializeField]
    int currentScore = 0;

    [SerializeField]
    int point = 10;

    [SerializeField]
    Text scoreText;

    private void Awake()
    {
        int numberOfThing = FindObjectsOfType<GameSessions>().Length;
        if(numberOfThing > 1)
        {
            DestroySelf();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += point;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = currentScore.ToString();
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
