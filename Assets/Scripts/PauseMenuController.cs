using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Arrastra el canvas del menú de pausa aquí
    public GameObject controlesPanel;

    private bool isPaused = false;

    void Update()
    {
        // Detectar si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0; // Detener el tiempo
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1; // Reanudar el tiempo
        isPaused = false;
    }

    public void ShowControls()
    {
        // Ocultar el menú de pausa y mostrar los controles
        pauseMenuCanvas.SetActive(false);
        controlesPanel.SetActive(true);
    }

    public void HideControls()
    {
        // Ocultar los controles y regresar al menú de pausa
        controlesPanel.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo está en marcha
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar nivel actual
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo está en marcha
        SceneManager.LoadScene("pantalladeinicio"); // Cargar el menú principal
    }
}
