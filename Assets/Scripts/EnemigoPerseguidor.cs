using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    public float Speed = 2.0f; // Velocidad de movimiento de enemigoP
    public int Health = 3;    // Vida del enemigo
    public GameObject BalaPrefab; // Prefab de bala
    public GameObject MedallitaPrefab; // Prefab de la medallita
    
    private GameObject Jonh; // Jugador que sigue 
    
    private void Start()
    {
        Jonh = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (Jonh == null) return;

        Vector3 direction = (Jonh.transform.position - transform.position).normalized;
        transform.position += direction * Speed * Time.deltaTime;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        MovimientoJonh jonh = collision.gameObject.GetComponent<MovimientoJonh>();
        if (jonh != null)
        {
            jonh.Hit(); 
        }
    }

    public void Hit()
    {
        Health--;
        if (Health <= 0)
        {
            Destroy(gameObject);
            Muerte();
        }
    }

    private void Muerte()
    {
        if (MedallitaPrefab != null)
        {
            Instantiate(MedallitaPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
