using UnityEngine.UI;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.white;

    [Range(0, 10)]
    public float speed = 1;

    SpriteRenderer cherry;

    void Awake()
    {
        cherry = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {
        cherry.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
