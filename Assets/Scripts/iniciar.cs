using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Método para cargar la escena de nivel1
    public void IniciarJuego()
    {
        SceneManager.LoadScene("intro"); // Asegúrate de que el nombre de la escena sea correcto
    }

    // Método para salir del juego
    public void SalirJuego()
    {
        // Solo funciona en el build del juego
        Application.Quit();

        // Si estás en el editor de Unity, puedes usar esta línea para detener la ejecución:
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
