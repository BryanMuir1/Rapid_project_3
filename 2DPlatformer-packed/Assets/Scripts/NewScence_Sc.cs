using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewScence_Sc : MonoBehaviour
{
    public int moveTo = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
            {
                SceneManager.LoadScene(moveTo);

            }
    }

}
