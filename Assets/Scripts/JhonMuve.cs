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
    private int vidasTotales = 3; // Total de vidas

    public GameObject pantallaGameOver;

    // Nombre del menú principal
    public string menuPrincipal;

    // Nombre del menú principal
    public string nivel1;

    // Referencia a los objetos de la barra de vida
    public Transform BarraDeVidaRelleno;

    // Nombre de la escena a la que quieres cambiar
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }

        if (PlayerPrefs.HasKey("VidasRestantes"))
        {
            vidasTotales = PlayerPrefs.GetInt("VidasRestantes");
        }
        else
        {
            vidasTotales = 3; // Valor inicial si no existe en PlayerPrefs
        }

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

        // Verificar si el personaje ha caído del mapa
        if (transform.position.y < -6.0f) // Ajusta el valor según el diseño de tu mapa
        {
            Health = 0; // La salud se reduce a 0
            UpdateHealthBar();
            PerderVida(); // Activa el sistema de pérdida de vida
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
        Health -= 1;
        UpdateHealthBar();

        if (Health <= 0) PerderVida();
    }

    private void PerderVida()
    {
        vidasTotales--;

        // Guardar las vidas restantes en PlayerPrefs
        PlayerPrefs.SetInt("VidasRestantes", vidasTotales);
        PlayerPrefs.Save();

        // Depuración: imprimir el número de vidas restantes
        Debug.Log("Vidas restantes: " + vidasTotales);

        if (vidasTotales <= 0)
        {
            // Mostrar pantalla de Game Over
            if (pantallaGameOver != null)
            {
                pantallaGameOver.SetActive(true);
                Time.timeScale = 0; // Pausa el juego
            }
        }
        else
        {
            // Reinicia el nivel actual si aún quedan vidas
            Health = 5; // Restablece la salud del jugador
            UpdateHealthBar();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    private void UpdateHealthBar()
    {
        if (BarraDeVidaRelleno != null)
        {
            // Escala el relleno de la barra de vida en función de la salud
            BarraDeVidaRelleno.localScale = new Vector3((float)Health / 5, 1, 1);
        }
    }

    public void GanarVida(int cantidad)
    {
        Health += cantidad; // Incrementa la vida
        if (Health > 5) Health = 5; // Opcional: Limita la vida máxima
        UpdateHealthBar(); // Actualiza la barra de vida
    }

    public int GetHealth()
    {
        return Health;
    }

    public void ReiniciarNivel()
    {
        // Reiniciar las vidas a 3 y guardar en PlayerPrefs
        vidasTotales = 4;
        PlayerPrefs.SetInt("VidasRestantes", vidasTotales);
        PlayerPrefs.Save();

        Time.timeScale = 1; // Reanuda el juego
        SceneManager.LoadScene(nivel1); // Cambia a Nivel 1
    }


    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1; // Reanuda el juego
        SceneManager.LoadScene(menuPrincipal);
    }


}