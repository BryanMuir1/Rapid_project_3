using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_Frog : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
        {
            Debug.Log("Hit");
            other.GetComponent<HealthComponent>().TakeDamage(20);
        }
    }

}
