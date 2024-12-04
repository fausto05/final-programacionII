using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJonh : MonoBehaviour
{
    public float JumpForce; // Fuerza del salto
    public float Speed;     // Velocidad de movimiento
    public GameObject BalaPrefab;
    

    private Rigidbody2D rigidbody2;
    private Animator animator;
    private float Horizontal;
    private bool Grounded;


    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Obtener el input horizontal del jugador
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        animator.SetBool("estaCorriendo", Horizontal != 0.0f);
        
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;
        
        // Detectar salto al presionar Escape (parece que quieres probarlo, pero considera usar "Jump" o Space)
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Disparo();
        }
    }

    private void Jump()
    {
        // Agregar fuerza hacia arriba
        rigidbody2.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Disparo()
    {
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;
        
        GameObject Bala = Instantiate(BalaPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
        Bala.GetComponent<Bala>().SetDirection(direccion);
    }
    private void FixedUpdate()
    {
        // Aplicar movimiento horizontal
        rigidbody2.velocity = new Vector2(Horizontal * Speed, rigidbody2.velocity.y);
    }
}
