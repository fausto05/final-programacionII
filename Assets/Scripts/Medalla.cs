using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medalla : MonoBehaviour
{
    public int Puntos = 10; // Puntos que otorga la medallita al recolectarla

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJonh jugador = collision.GetComponent<MovimientoJonh>();
        if (jugador != null)
        {
            // Aquí podrías sumar puntos o activar algún efecto
            Debug.Log("Medallita recolectada! +" + Puntos + " puntos");
            Destroy(gameObject); // Destruye la medallita al recolectarla
        }
    }
}
