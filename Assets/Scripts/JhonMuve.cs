using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar de escena
using UnityEngine.UI; // Importante para trabajar con UI

public class JhonMuve : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float JumpForce;
    public float Speed;
    private bool Grounded;
    private Animator Animator;
    public GameObject DisparoPrefab;
    private float LastShoot;
    private int Health = 5;

    // Referencia a los objetos de la barra de vida
    public Transform BarraDeVidaRelleno;

    // Nombre de la escena a la que quieres cambiar
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        // Inicializar barra de vida (opcional)
        UpdateHealthBar();
    }

    // Update es llamado una vez por frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        //voltear, izquierda, derecha
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //correr
        Animator.SetBool("running", Horizontal != 0.0f);

        // Detectar Suelo
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Disparar
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

        // Verificar si la posición X supera el valor de 5
        if (transform.position.x > 5.0f)
        {
            // Cambiar a la siguiente escena
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject disparo = Instantiate(DisparoPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        Disparar disparoScript = disparo.GetComponent<Disparar>();
        disparoScript.SetDirection(direction);
        disparoScript.SetOwner(gameObject); // Asigna al jugador como propietario de la bala
    }

    public void Hit()
    {
        Health = Health - 1;
        UpdateHealthBar(); // Actualizar la barra de vida cuando recibe daño
        if (Health == 0) Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (BarraDeVidaRelleno != null)
        {
            // Escala el relleno de la barra de vida en función de la salud
            BarraDeVidaRelleno.localScale = new Vector3((float)Health / 5, 1, 1);
        }
    }
}
