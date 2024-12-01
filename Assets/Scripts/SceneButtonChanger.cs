using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtonChanger : MonoBehaviour
{
    // Referencias a los botones
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button cambionivelButton;
    public Button atras;

    void Start()
    {
        // Asigna los eventos de los botones
        if (level1Button != null)
            level1Button.onClick.AddListener(() => LoadScene("Nivel1"));
        
        if (level2Button != null)
            level2Button.onClick.AddListener(() => LoadScene("Nivel2"));
        
        if (level3Button != null)
            level3Button.onClick.AddListener(() => LoadScene("Nivel3"));
        
        if (cambionivelButton != null)
            cambionivelButton.onClick.AddListener(() => LoadScene("seleccionNiveles"));
        
        if (atras != null)
            atras.onClick.AddListener(() => LoadScene("pantalladeinicio"));
    }

    // Método que carga la escena correspondiente
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
