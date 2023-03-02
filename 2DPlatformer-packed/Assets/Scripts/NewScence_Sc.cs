using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScence_Sc : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
            {
                SceneManager.LoadScene(1);

            }
    }
}
