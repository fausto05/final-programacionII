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
    private float tiempoRestante; // Tiempo que resta para completar el nivel
    private bool juegoTerminado = false; // Para terminar el juego

    private void Start()
    {
        tiempoRestante = tiempoMaximo;
        ActualizarUI();
    }

    private void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;

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
        if (textoMedallas != null)
        {
            textoMedallas.text = "Medallas: " + medallasRecolectadas + "/" + MedallasParaGanar;
        }

        if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante);
        }
    }

    private void GanarJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        SceneManager.LoadScene(2); 
    }

    public void PerderJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        SceneManager.LoadScene(3); 

        if (!juegoTerminado) 
        {
            juegoTerminado = true;
            SceneManager.LoadScene(3); 
        }
    }
}
