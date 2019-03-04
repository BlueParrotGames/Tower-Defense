using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [Tooltip("Movement Speed")]
    public float startSpeed = 10f;
    [HideInInspector] public float speed;
    [SerializeField] float startHealth = 100;
    private float health = 100;
    [SerializeField] int cashValue = 10;
    [SerializeField] int scoreValue = 5;

    [Header("Editor")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] Image healthBar;
    Renderer _renderer;
    Material shader;
    string _2C608 = "Vector1_EDE2C608";
    float dissolveAmount;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        shader = _renderer.material;

    }

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    void UpdateDissolveAmount()
    {
        dissolveAmount = shader.GetFloat(_2C608);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = (health / startHealth);

        UpdateDissolveAmount();
        shader.SetFloat(_2C608, 1 - (health / startHealth));

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.kills += 1;
        PlayerStats.cash += cashValue;
        PlayerStats.totalCash += cashValue;
        PlayerStats.score += scoreValue;

        WaveSpawner.enemyCount--;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
