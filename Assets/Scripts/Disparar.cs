using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    private GameObject owner; // Propietario de la bala

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner; // Asigna el propietario de la bala
    }

    public GameObject GetOwner()
    {
        return owner; // Devuelve el propietario de la bala
    }

    public void DistruirDisparo()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignorar colisión si el objeto colisionado es el propietario de la bala
        if (collision.gameObject == owner)
        {
            return; // Salir sin hacer nada si colisiona con su propietario
        }

        JhonMuve john = collision.GetComponent<JhonMuve>();
        gruntScript grunt = collision.GetComponent<gruntScript>();

        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null)
        {
            grunt.Hit();
        }

        DistruirDisparo();
    }
}
