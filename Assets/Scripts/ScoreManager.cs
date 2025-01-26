using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject player;
    private float score = 0;
    private BubbleControl bubbleControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleControl = player.GetComponent<BubbleControl>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!bubbleControl.isDead){
            score += Time.deltaTime;
            scoreText.text = $"Score: {score.ToString("F0")}"; // F0 para mostrar el número sin decimales
        }
        
    }
}
