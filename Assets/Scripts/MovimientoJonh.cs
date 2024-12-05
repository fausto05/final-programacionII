using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovimientoJonh : MonoBehaviour
{
    public float JumpForce; // Fuerza del salto
    public float Speed;     // Velocidad de movimiento
    public GameObject BalaPrefab;
    public LayerMask sueloLayer; // Capa que define qué es considerado "suelo"
    public Transform detectorSuelo;
    public float radioDeteccion;

    private Rigidbody2D rigidbody2;
    private Animator animator;
    private float Horizontal;
    private bool Grounded;
    private float Vida = 5;
    

    

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

        Grounded = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, sueloLayer);

        // Salto solo si estamos en el suelo
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

    public void Hit()
    {
        Vida = Vida - 1;
        if (Vida == 0)
            Morir();
    }

    private void Morir()
    {
        Debug.Log("El jugador ha muerto");

        // Llama al GameManager para cargar la escena de derrota
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.PerderJuego();
        }
        Destroy(gameObject); // Destruye al jugador
        
    }

    

}
