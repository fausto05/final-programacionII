using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject EnemigoPrefab; // Prefab del enemigo a spawnear
    public GameObject EnemigoPerseguidorPrefab;
    public GameObject EnemigoJefePrefab;
    public Transform[] PuntosSpawn; // Puntos donde pueden aparecer los enemigos
    public float IntervaloSpawn = 5f; // Tiempo entre spawns
    public int MaxEnemigos = 10; // Número máximo de enemigos activos

    private int enemigosActivos = 0; // Contador de enemigos activos

    private void Start()
    {
        // Inicia la corrutina para spawnear enemigos
        StartCoroutine(SpawnEnemigosPeriodicamente());
    }

    private IEnumerator SpawnEnemigosPeriodicamente()
    {
        while (enemigosActivos < MaxEnemigos)
        {
            yield return new WaitForSeconds(IntervaloSpawn);

            if (enemigosActivos < MaxEnemigos)
            {
                SpawnEnemigo();
                SpawnEnemigoJefe();
                SpawnEnemigoPerseguidor();
            }
        }
    }

    private void SpawnEnemigo()
    {
        // Selecciona un punto de spawn aleatorio
        int index = Random.Range(0, PuntosSpawn.Length);

        // Instancia el enemigo en el punto seleccionado
        Instantiate(EnemigoPrefab, PuntosSpawn[index].position, Quaternion.identity);
        
        // Incrementa el contador de enemigos activos
        enemigosActivos++;
    }

    private void SpawnEnemigoJefe()
    {
        // Selecciona un punto de spawn aleatorio
        int index = Random.Range(0, PuntosSpawn.Length);

        // Instancia el enemigo en el punto seleccionado
        Instantiate(EnemigoJefePrefab, PuntosSpawn[index].position, Quaternion.identity);

        // Incrementa el contador de enemigos activos
        enemigosActivos++;
    }

    private void SpawnEnemigoPerseguidor()
    {
        // Selecciona un punto de spawn aleatorio
        int index = Random.Range(0, PuntosSpawn.Length);

        // Instancia el enemigo en el punto seleccionado
        Instantiate(EnemigoPerseguidorPrefab, PuntosSpawn[index].position, Quaternion.identity);

        // Incrementa el contador de enemigos activos
        enemigosActivos++;
    }

    public void EnemigoMuerto()
    {
        // Llama este método desde los enemigos cuando mueran
        enemigosActivos--;
    }
}
