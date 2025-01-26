using UnityEngine;
using System.Collections;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] bubbleList; // Lista de burbujas (incluyendo Power-Ups)
    [SerializeField] private Transform spawnPoint; // Punto de spawn
    [SerializeField] private float PositionVariable; // Variación en la posición de spawn
    [SerializeField] private float minRandomTime; // Tiempo mínimo entre spawns
    [SerializeField] private float maxRandomTime; // Tiempo máximo entre spawns

    void Start()
    {
        StartCoroutine(SpawnBubble());
    }

    IEnumerator SpawnBubble()
    {
        while (true)
        {
            // Espera un tiempo aleatorio entre spawns
            yield return new WaitForSeconds(Random.Range(minRandomTime / TimerController.time, maxRandomTime / TimerController.time));

            // Selecciona una burbuja al azar
            GameObject obj = bubbleList[Random.Range(0, bubbleList.Length)];

            // Si es un Power-Up y superBubble está activo, lo omite
            if (obj.CompareTag("PowerUpWater") && BubbleControl.superBubble)
            {
                continue; // Omitir el ciclo actual
            }

            // Instancia la burbuja seleccionada
            Instantiate(
                obj,
                new Vector2(
                    spawnPoint.transform.position.x,
                    Random.Range(spawnPoint.transform.position.y - PositionVariable, spawnPoint.transform.position.y + PositionVariable)
                ),
                Quaternion.identity
            );
        }
    }
}
