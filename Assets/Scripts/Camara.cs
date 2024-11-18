using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform John;

    // Variables para definir los límites de la cámara
    public float minX;
    public float maxX;

    void Update()
    {
        if (John != null)
        {
            Vector3 position = transform.position;
            
            // Seguir al personaje solo si está en la zona entre los límites
            if (John.position.x > minX && John.position.x < maxX)
            {
                position.x = John.position.x;
            }
            else
            {
                // Si el personaje está en el borde, fija la cámara en el límite
                position.x = Mathf.Clamp(John.position.x, minX, maxX);
            }

            transform.position = position;
        }
    }
}
