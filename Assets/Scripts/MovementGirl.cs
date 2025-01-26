using UnityEngine;

public class MovementGirl : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Velocidad del movimiento

    // Update es llamado una vez por frame
    void Update()
    {
        // Mueve el objeto hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
