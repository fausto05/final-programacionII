using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoJefe : MonoBehaviour
{
    public float Speed = 2.0f; // Velocidad de movimiento
    public float RangoDisparo = 3.0f; // Rango para detenerse y disparar
    public GameObject BalaPrefab; // Prefab de la bala
    public GameObject Jonh; // Referencia al jugador
    public float CadenciaDisparo = 1.0f; // Tiempo entre disparos
    public float DistanciaPatrullaje = 5.0f; // Distancia total que recorrerá
    public int Health = 3;
    public GameObject MedallitaPrefab; // Prefab de la medallita

    private Vector3 puntoInicial; // Posición inicial del enemigo
    private Vector3 destinoActual; // Destino actual del enemigo
    private bool disparando = false; // Si está disparando
    private float ultimoDisparo; // Última vez que disparó

    private void Start()
    {
        // Guardar la posición inicial y calcular el primer destino
        puntoInicial = transform.position;
        destinoActual = puntoInicial + Vector3.right * DistanciaPatrullaje;
        StartCoroutine(MoverEntrePuntos());
    }

    private void Update()
    {
        if (Jonh == null) return;

        // Detectar si el jugador está dentro del rango de disparo
        float distanciaJugador = Vector3.Distance(transform.position, Jonh.transform.position);

        if (distanciaJugador <= RangoDisparo)
        {
            if (!disparando)
            {
                disparando = true;
                StopCoroutine(MoverEntrePuntos());
            }

            Disparar();
        }
        else
        {
            if (disparando)
            {
                disparando = false;
                StartCoroutine(MoverEntrePuntos());
            }
        }

        // Ajustar dirección visual
        Vector3 direccion = (destinoActual - transform.position).normalized;
        if (direccion.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    private IEnumerator MoverEntrePuntos()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoActual, Speed * Time.deltaTime);

            // Si llega al destino, cambiar dirección
            if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
            {
                // Alternar entre la posición inicial y el extremo de la distancia
                destinoActual = destinoActual == puntoInicial
                    ? puntoInicial + Vector3.right * DistanciaPatrullaje
                    : puntoInicial;

                yield return new WaitForSeconds(1.0f); // Esperar antes de moverse al siguiente punto
            }

            yield return null;
        }
    }

    private void Disparar()
    {
        if (Time.time > ultimoDisparo + CadenciaDisparo)
        {
            ultimoDisparo = Time.time;

            Vector3 direccion;
            if (transform.localScale.x == 1.0f) direccion = Vector3.right;
            else direccion = Vector3.left;

            GameObject Bala = Instantiate(BalaPrefab, transform.position + direccion * 0.5f, Quaternion.identity);
            Bala.GetComponent<Bala>().SetDirection(direccion);
        }
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0)
        {
            Destroy(gameObject);
            Muerte();
        }
    }

    private void Muerte()
    {
        // Instanciar la medallita en la posición del enemigo
        if (MedallitaPrefab != null)
        {
            Instantiate(MedallitaPrefab, transform.position, Quaternion.identity);
        }

        // Destruir al enemigo
        Destroy(gameObject);
    }

}
