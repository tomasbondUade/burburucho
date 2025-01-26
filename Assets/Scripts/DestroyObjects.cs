using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);
        Debug.Log("Collision");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        Debug.Log("Trigger");
    }
}
