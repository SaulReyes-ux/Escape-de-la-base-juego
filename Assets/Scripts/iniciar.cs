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

    public void selecScene()
    {
        SceneManager.LoadScene("nivelesLista");
    }

    public void nivel1()
    {
        SceneManager.LoadScene("nivel1");
    }

    public void nivel2()
    {
        SceneManager.LoadScene("nivel2");
    }

    public void nivel3()
    {
        SceneManager.LoadScene("nivel3");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("pantalladeinicio");
    }
}
