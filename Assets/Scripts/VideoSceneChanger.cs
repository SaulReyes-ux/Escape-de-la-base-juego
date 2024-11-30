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

    void Update()
    {
        // Detectar la tecla de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Cambiar de escena inmediatamente cuando se presiona la tecla espacio
            ChangeScene();
        }
    }

    // Método que se llama cuando el video termina
    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambia a la siguiente escena
        ChangeScene();
    }

    // Método para cambiar la escena
    void ChangeScene()
    {
        // Cambia a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}

