using UnityEngine;

public class LandControl : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidad inicial del desplazamiento
    private SpriteRenderer sr;
    private Material material;
    private bool superBubbleWasActive; // Estado previo de superBubble

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        material = sr.material; // Obtiene el material asociado
        superBubbleWasActive = BubbleControl.superBubble; // Estado inicial de superBubble
    }

    void Update()
    {
        // Actualiza el desplazamiento del material con la velocidad actual
        Vector2 offset = material.mainTextureOffset;
        offset.x += speed * Time.deltaTime;
        material.mainTextureOffset = offset;

        // Verifica si el modo superBubble cambió de activo a inactivo
        if (superBubbleWasActive && !BubbleControl.superBubble)
        {
            ReduceSpeedBy50Percent();
        }

        // Actualiza el estado anterior de superBubble
        superBubbleWasActive = BubbleControl.superBubble;
    }

    private void ReduceSpeedBy50Percent()
    {
        // Reducir la velocidad en un 50%
        speed *= 0.5f;
        Debug.Log($"Velocidad reducida al 50%. Nueva velocidad: {speed}");
    }
}
