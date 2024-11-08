using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class iniciar : MonoBehaviour
{
    // Start is called before the first frame update
    public string Nivel1;

    private void Start()
    {
        // Obtén el componente Button y añade el evento al presionarlo
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // Carga la escena especificada cuando se toca la imagen
        SceneManager.LoadScene(Nivel1);
    }
}
