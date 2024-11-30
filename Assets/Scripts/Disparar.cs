using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;
    public int damage = 10; // Daño que inflige la bala
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
        if (collision.gameObject == owner)
        {
            return;
        }

        JhonMuve john = collision.GetComponent<JhonMuve>();
        gruntScript grunt = collision.GetComponent<gruntScript>();
        jefeScript jefe = collision.GetComponent<jefeScript>();

        if (john != null)
        {
            john.Hit(); // No necesita parámetro si no lo requiere.
        }
        if (grunt != null)
        {
            grunt.Hit();
        }
        if (jefe != null)
        {
            jefe.Hit(); // Pasa el daño como parámetro al método Hit.
        }

        DistruirDisparo();
    }
}
