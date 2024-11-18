using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneChanger : MonoBehaviour
{
    // Campo público para el VideoPlayer
    public VideoPlayer videoPlayer;
    // Campo público para el nombre de la siguiente escena
    public string nextSceneName;

    void Start()
    {
        // Verifica si el VideoPlayer está asignado
        if (videoPlayer == null)
        {
            // Intenta encontrar automáticamente el componente VideoPlayer en el mismo objeto
            videoPlayer = GetComponent<VideoPlayer>();
        }
        
        // Asegura que se asigne el evento solo si el VideoPlayer está configurado
        if (videoPlayer != null)
        {
            // Asigna el método OnVideoEnd al evento loopPointReached
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer no está asignado en el Inspector ni en el objeto.");
        }
    }

    // Método que se llama cuando el video termina
    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambia a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
