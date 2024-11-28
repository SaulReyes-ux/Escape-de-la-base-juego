using UnityEngine;

public class vidaItem : MonoBehaviour
{
    public int vidaExtra = 1; // Vida que otorga al jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene el script del jugador
        JhonMuve jugador = collision.GetComponent<JhonMuve>();
        if (jugador != null)
        {
            jugador.GanarVida(vidaExtra); // Llama al método de ganar vida
            Destroy(gameObject); // Destruye el objeto de vida
        }
    }
}

