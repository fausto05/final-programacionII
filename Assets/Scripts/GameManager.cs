using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int MedallasParaGanar = 10; // Número de medallas necesarias para ganar
    private int medallasRecolectadas = 0; // Contador de medallas actuales

    public TMP_Text textoMedallas; // Para mostrar las medallas recolectadas
    public TMP_Text textoTiempo; // Para mostrar el tiempo restante
    

    public float tiempoMaximo = 120f; // Tiempo máximo del juego en segundos
    private float tiempoRestante;
    private bool juegoTerminado = false;

    

    private void Start()
    {
        tiempoRestante = tiempoMaximo;
        ActualizarUI();

        
    }

    private void Update()
    {
        if (juegoTerminado) return;

        // Reduce el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Si el tiempo se acaba, pierde el juego
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            PerderJuego();
        }

        ActualizarUI();
    }

    public void AgregarMedalla()
    {
        if (juegoTerminado) return;

        medallasRecolectadas++;
        ActualizarUI();

        if (medallasRecolectadas >= MedallasParaGanar)
        {
            GanarJuego();
        }
    }

    private void ActualizarUI()
    {
        // Actualiza el texto de las medallas
        if (textoMedallas != null)
        {
            textoMedallas.text = "Medallas: " + medallasRecolectadas + "/" + MedallasParaGanar;
        }

        // Actualiza el texto del tiempo restante
        if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante);
        }

        
    }

    private void GanarJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        Debug.Log("¡Ganaste el juego!");
        SceneManager.LoadScene(2); // Cambia a la escena de victoria
    }

    public void PerderJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        Debug.Log("¡Perdiste el juego!");
        SceneManager.LoadScene(3); // Cambia a la escena de derrota

        if (!juegoTerminado) // Evita múltiples llamadas
        {
            juegoTerminado = true;
            Debug.Log("¡Perdiste el juego!");
            SceneManager.LoadScene(3); // Cambia a la escena de derrota
        }
    }
}
