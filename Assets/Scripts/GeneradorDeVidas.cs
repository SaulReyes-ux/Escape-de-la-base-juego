using UnityEngine;

public class GeneradorDeVidas : MonoBehaviour
{
    public GameObject vidaPrefab; // Prefab del objeto de vida
    public float intervaloGeneracion = 5.0f; // Tiempo entre generaciones
    public float rangoHorizontal = 3.0f; // Distancia horizontal alrededor del jugador donde caen las vidas
    public float alturaY = 6.0f; // Altura desde donde caen las vidas
    public JhonMuve jugador; // Referencia al jugador

    private void Start()
    {
        // Llama a la función repetidamente
        InvokeRepeating("GenerarVida", 2.0f, intervaloGeneracion);
    }

    private void GenerarVida()
    {
        // Verifica que el jugador tiene menos de 5 puntos de vida
        if (jugador != null && jugador.GetHealth() < 5)
        {
            // Genera una posición aleatoria cerca del jugador
            float posicionX = Random.Range(jugador.transform.position.x - rangoHorizontal, jugador.transform.position.x + rangoHorizontal);
            Vector3 posicion = new Vector3(posicionX, alturaY, 0);

            // Instancia el objeto de vida
            Instantiate(vidaPrefab, posicion, Quaternion.identity);
        }
    }
}
