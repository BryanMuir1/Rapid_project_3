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
    public void Heal(float damage)
    {
        health += damage;
        if (health <= 0)
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
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20);

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Health_Item")
        {
            Heal(20);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "En_frog")
        {
            TakeDamage(20);

        }
    }
   

}
