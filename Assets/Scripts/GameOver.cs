using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Text Objects")]
    [SerializeField] Text roundsText;
    [SerializeField] Text killsText;
    [SerializeField] Text scoreText;
    [SerializeField] Text cashText;


    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
        killsText.text = PlayerStats.kills.ToString();
        scoreText.text = PlayerStats.score.ToString();
        cashText.text = PlayerStats.totalCash.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        // go to menu
    }
}
