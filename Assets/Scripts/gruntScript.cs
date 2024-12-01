using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gruntScript : MonoBehaviour
{
    public GameObject John;
    public GameObject DisparoPrefab;
    private int Health = 1;

    private float LastShoot;
    private bool isShooting = false; // Controla si ya est� en proceso de disparo con delay

    private void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        // Si la distancia es menor a 1.0f y no est� en proceso de disparar, inicia la corrutina
        if (distance < 1.0f && !isShooting)
        {
            StartCoroutine(ShootWithDelay(3.0f)); // Llama a la corrutina para disparar con delay entre disparos
        }
    }

    // Corrutina que a�ade un delay entre cada disparo
    private IEnumerator ShootWithDelay(float delay)
    {
        isShooting = true; // Marca que est� en proceso de disparo
        Shoot(); // Dispara
        yield return new WaitForSeconds(delay); // Espera 3 segundos antes de permitir el siguiente disparo
        isShooting = false; // Marca que ya termin� el proceso de disparo, permitiendo otro ciclo
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject disparo = Instantiate(DisparoPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        disparo.GetComponent<Disparar>().SetDirection(direction);
    }

  public void Hit()
{
    Health = Health - 1;
    if (Health == 0)
    {
        // Añadir puntuación al destruir el enemigo
        ScoreManager.Instance.AddScore(10); // Añade 10 puntos por enemigo
        Destroy(gameObject);
    }
}

}
