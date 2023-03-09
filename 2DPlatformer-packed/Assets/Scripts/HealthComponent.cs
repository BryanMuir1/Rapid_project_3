using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{

    public float health;
    public Image healthBar;
    public bool hasHealthBar;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


    void Update()
    {
        if (hasHealthBar)
        {
            healthBar.fillAmount = health / 100;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(20);
        }
    }
}
