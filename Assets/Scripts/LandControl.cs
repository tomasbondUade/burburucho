using UnityEngine;

public class LandControl : MonoBehaviour
{
    [SerializeField] private float baseSpeed; // Velocidad inicial
    private float currentSpeed; // Velocidad actual
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentSpeed = baseSpeed; // Inicializa la velocidad actual con la velocidad base
    }

    void Update()
    {
        // Incrementa la velocidad con TimerController.time
        currentSpeed = baseSpeed * TimerController.time;

        // Aplica el desplazamiento de la textura
        sr.material.mainTextureOffset += new Vector2(currentSpeed * Time.deltaTime, 0);
    }

    // Método para restablecer la velocidad a la original (baseSpeed)
    public void ResetSpeed()
    {
        TimerController.time = 1f; // Opcional: restablece el tiempo a su estado inicial
        Debug.Log("Velocidad restablecida a la base.");
    }
}
