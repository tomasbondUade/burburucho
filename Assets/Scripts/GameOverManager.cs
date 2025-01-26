using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Reinicia la escena actual
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sale del juego (solo funciona en la versión compilada)
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado."); // Útil para pruebas en el editor
    }
}
