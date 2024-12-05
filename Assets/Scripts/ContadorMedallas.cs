using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ContadorMedallas : MonoBehaviour
{
    public int MedallasParaGanar = 10; // Número de medallas necesarias para ganar
    private int medallasRecolectadas = 0; // Contador de medallas actuales

    public TMP_Text textoMedallas; // Para TextMeshPro
    
    public void AgregarMedalla()
    {
        medallasRecolectadas++;
        ActualizarUI();

        if (medallasRecolectadas >= MedallasParaGanar)
        {
            GanarJuego();
        }
    }

    private void ActualizarUI()
    {
        textoMedallas.text = "Medallas: " + medallasRecolectadas;
    }

    private void GanarJuego()
    {
        Debug.Log("¡Ganaste el juego!");
        // Implementa aquí lo que ocurre al ganar, como cargar una nueva escena o mostrar un mensaje
    }
}
