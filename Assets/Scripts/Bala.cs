using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Speed; // Velocidad de la bala

    private Rigidbody2D Rigidbody2D; // Rigidbody de la bala
    private Vector2 Direccion; // Direccion de la bala
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direccion * Speed;
    }

    public void SetDirection(Vector2 direccion)
    {
        Direccion = direccion; 
    }

    public void DestroyBala()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJonh jonh = collision.GetComponent<MovimientoJonh>();
        Enemigo enemigo = collision.GetComponent<Enemigo>();
        EnemigoPerseguidor enemigoperseguidor = collision.GetComponent<EnemigoPerseguidor>();
        EnemigoJefe enemigojefe = collision.GetComponent<EnemigoJefe>();

        if (jonh != null)
        {
            jonh.Hit();
        }
        if (enemigo != null)
        {
            enemigo.Hit();
        }
        DestroyBala();
        
        if (enemigoperseguidor != null)
        {
            enemigoperseguidor.Hit();
        }
        DestroyBala();

        if (enemigojefe != null)
        {
            enemigojefe.Hit(); 
            DestroyBala(); 
        }
    }
}
