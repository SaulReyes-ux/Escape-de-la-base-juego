using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jefeScript : MonoBehaviour
{
    public GameObject John; // Referencia al personaje principal
    public GameObject DisparoPrefab; // Prefab del disparo
    private int Health = 10; // Salud del enemigo
    public float Speed = 0.1f; // Velocidad de movimiento del enemigo
    public float jumpForce = 5.0f; // Fuerza del salto
    private float LastShoot; // Último disparo realizado
    private bool isShooting = false; // Controla si está disparando
    private Rigidbody2D rb; // Referencia al Rigidbody2D del enemigo

    public float raycastDistance = 1.0f; // Distancia del raycast para detectar obstáculos
    public LayerMask groundLayer; // Capa del suelo para verificar si está tocando el suelo

    // Referencia a la barra de vida del enemigo
    public Transform BarraDeVidaRelleno;

    // Nueva variable para controlar la distancia de acercamiento al jugador
    public float stopDistance = 2.0f; // Distancia a la que el enemigo dejará de moverse

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del enemigo
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("PlayerLayer")); // Ignorar colisión entre enemigo y jugador
        UpdateHealthBar(); // Inicializar la barra de vida
    }

    void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        float distance = Mathf.Abs(direction.x);

        // Determinar si el enemigo está demasiado cerca del jugador
        if (distance < stopDistance)
        {
            // No se mueve, pero aún puede disparar
            // Si está demasiado cerca, no se mueve
            direction = Vector3.zero; 
        }
        else
        {
            // Mover al jugador si está fuera del rango de detención
            if (direction.x >= 0.0f)
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            else
                transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);

            // Movimiento del enemigo
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, John.transform.position, step);
        }

        // Si el enemigo está suficientemente cerca del jugador y no está disparando, disparar
        if (distance < 1.0f && !isShooting)
        {
            StartCoroutine(ShootWithDelay(3.0f));
        }

        Jump();
    }

    private IEnumerator ShootWithDelay(float delay)
    {
        isShooting = true;
        Shoot();
        yield return new WaitForSeconds(delay);
        isShooting = false;
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x > 0.0f)
            direction = Vector3.right;
        else
            direction = Vector3.left;

        GameObject disparo = Instantiate(DisparoPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        Disparar disparoScript = disparo.GetComponent<Disparar>();
        disparoScript.SetDirection(direction);
        disparoScript.SetOwner(gameObject);
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void Hit()
    {
        Health = Health - 1;
        UpdateHealthBar();
        if (Health <= 0) Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (BarraDeVidaRelleno != null)
        {
            BarraDeVidaRelleno.localScale = new Vector3((float)Health / 5, 1, 1);
        }
    }
}
