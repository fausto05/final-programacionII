using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float Speed = 2.0f; // Velocidad de movimiento
    public int Health = 3;    // Vida del enemigo
    public GameObject BalaPrefab; // Prefab de bala
    public GameObject MedallitaPrefab; // Prefab de la medallita

    private float LastShoot; // Ultima vez que disparo
    private GameObject Jonh; // Jugador a seguir

    private void Start()
    {
        Jonh = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Jonh == null) return;
        
        Vector3 direction = Jonh.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Jonh.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 2f)
        {
            Disparo();
            LastShoot = Time.time;
        }
    }

    private void Disparo()
    {
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;

        GameObject Bala = Instantiate(BalaPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
        Bala.GetComponent<Bala>().SetDirection(direccion);
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
        if (MedallitaPrefab != null)
        {
            Instantiate(MedallitaPrefab, transform.position, Quaternion.identity);
        }

        GameObject spawnManager = GameObject.Find("SpawnManager");
        if (spawnManager != null)
        {
            spawnManager.GetComponent<SpawnEnemigos>().EnemigoMuerto();
        }

        Destroy(gameObject);
    }
}
