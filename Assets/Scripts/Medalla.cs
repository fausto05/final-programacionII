using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medalla : MonoBehaviour
{
    public int Puntos = 1; // Puntos que otorga la medallita al recolectarla

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJonh jugador = collision.GetComponent<MovimientoJonh>();
        if (jugador != null)
        {
            GameManager contador = FindObjectOfType<GameManager>();
            if (contador != null)
            {
                contador.AgregarMedalla();
            }
            Destroy(gameObject); 
        }
    }
}
