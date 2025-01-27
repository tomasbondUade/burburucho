using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject player;
    private float score = 0;
    private BubbleControl bubbleControl;

    void Start()
    {
        bubbleControl = player.GetComponent<BubbleControl>();
    }

    void Update()
    {   
        // Solo incrementa el score si el jugador no está muerto
        if (!bubbleControl.isDead)
        {
            score += Time.deltaTime;
            scoreText.text = score.ToString("F0"); // F0 para mostrar el número sin decimales
        }
    }

    // Método para reiniciar la puntuación
    public void ResetScore()
    {
        score = 0; // Restablece el puntaje a 0
        scoreText.text = "Score: 0"; // Actualiza el texto de la UI
    }
}
