using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direccion;
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
}