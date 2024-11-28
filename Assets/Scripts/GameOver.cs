using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Método para cargar la escena de nivel1
    public void Reinicio()
    {
        SceneManager.LoadScene("nivel1"); // Asegúrate de que el nombre de la escena sea correcto
    }

    public void Menu()
    {
        SceneManager.LoadScene("pantalladeinicio"); // Asegúrate de que el nombre de la escena sea correcto
    }
}
