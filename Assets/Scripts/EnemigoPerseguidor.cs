using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    public float Speed = 2.0f; // Velocidad de movimiento
    public int Health = 3;    // Vida del enemigo
    public GameObject BalaPrefab;
    public GameObject MedallitaPrefab; // Prefab de la medallita

    private GameObject Jonh;

    private void Start()
    {
        // Buscar al objeto con el tag "Player"
        Jonh = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        // Si Jonh no existe (destruido o no asignado), no hacer nada
        if (Jonh == null) return;

        // Moverse hacia Jonh
        Vector3 direction = (Jonh.transform.position - transform.position).normalized;
        transform.position += direction * Speed * Time.deltaTime;

        // Ajustar dirección visual
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con Jonh, reducir su vida
        MovimientoJonh jonh = collision.gameObject.GetComponent<MovimientoJonh>();
        if (jonh != null)
        {
            jonh.Hit(); // Reduce la vida de Jonh
        }
    }

    public void Hit()
    {
        // Reducir la vida del enemigo al recibir un disparo
        Health--;
        if (Health <= 0)
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
