using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    [SerializeField] GameObject gameOverUI;

    public static GameManager instance = null;

    private void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if(GameIsOver)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
