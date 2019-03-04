using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Money")]
    public static int cash;
    [SerializeField] int startMoney = 400;
    [SerializeField] Text vBucksText;

    [Header("Lives")]
    public static int lives;
    [SerializeField] int startLives = 20;
    [SerializeField] Text livesText;

    [Header("Stats")]
    public static int rounds;
    public static int kills;
    public static int score;
    public static int totalCash;

    private void Start()
    {
        cash = startMoney;
        lives = startLives;
        rounds = 0;
        kills = 0;
        score = 0;
        totalCash = startMoney;
    }

    private void Update()
    {
        vBucksText.text = "$" + PlayerStats.cash.ToString();
        livesText.text = PlayerStats.lives.ToString() + " Lives...";

        lives = Mathf.Clamp(lives, 0, startLives);
    }

}
