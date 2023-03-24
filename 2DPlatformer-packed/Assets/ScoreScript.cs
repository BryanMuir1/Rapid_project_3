using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scoreText;
    public int scoreCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        scoreText.text = "Score = " + scoreCount;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            scoreCount +=1;
            scoreText.text = "Score: " + scoreCount;
        }
    }
}
