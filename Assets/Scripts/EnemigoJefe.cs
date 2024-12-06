using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoJefe : MonoBehaviour
{
    public float Speed = 2.0f; // Velocidad de movimiento
    public float RangoDisparo = 3.0f; // Rango para detenerse y disparar
    public GameObject BalaPrefab; // Prefab de la bala
    public float DistanciaPatrullaje = 5.0f; // Distancia total que recorrerá
    public int Health = 3; // Vida del enemigo
    public GameObject MedallaPrefab; // Prefab de la medalla

    private Vector3 puntoInicial; // Posición inicial del enemigo
    private Vector3 destinoActual; // Destino actual del enemigo
    private bool disparando = false; // Si está disparando
    private float LastShoot; // Última vez que disparó
    private GameObject Jonh; // Jugador a seguir

    private void Start()
    {
        Jonh = GameObject.FindWithTag("Player");
        
        puntoInicial = transform.position; // Guardar la posición inicial y calcular el primer destino
        destinoActual = puntoInicial + Vector3.right * DistanciaPatrullaje;
        StartCoroutine(MoverEntrePuntos());
    }

    private void Update()
    {
        if (Jonh == null) return;

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

        Vector3 direccion = (destinoActual - transform.position).normalized;
        if (direccion.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    private IEnumerator MoverEntrePuntos()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoActual, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
            {
                destinoActual = destinoActual == puntoInicial // Alternar entre la posición inicial y el extremo de la distancia
                    ? puntoInicial + Vector3.right * DistanciaPatrullaje
                    : puntoInicial;

                yield return new WaitForSeconds(1.0f); 
            }
            yield return null;
        }
    }

    private void Disparar()
    {
        if (Time.time > LastShoot + 2f) 
        {
            LastShoot = Time.time;

            Vector3 direccion;
            if (transform.localScale.x == 1.0f) direccion = Vector3.right;
            else direccion = Vector3.left;

            GameObject Bala = Instantiate(BalaPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
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
        if (MedallaPrefab != null)
        {
            Instantiate(MedallaPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

