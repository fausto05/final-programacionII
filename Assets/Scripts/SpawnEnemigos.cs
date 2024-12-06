using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject EnemigoPrefab; 
    public GameObject EnemigoPerseguidorPrefab;     // Prefabs de los enemigos a spawnear
    public GameObject EnemigoJefePrefab;
    
    public Transform[] PuntosSpawn; // Puntos donde pueden aparecer los enemigos
    public float IntervaloSpawn = 5f; // Tiempo entre spawns
    public int MaxEnemigos = 10; // Número máximo de enemigos activos

    private int enemigosActivos = 0; // Contador de enemigos activos

    private void Start()
    {
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
        int index = Random.Range(0, PuntosSpawn.Length);  // Selecciona un punto de spawn aleatorio

        Instantiate(EnemigoPrefab, PuntosSpawn[index].position, Quaternion.identity);
        
        enemigosActivos++;
    }

    private void SpawnEnemigoJefe()
    {
        int index = Random.Range(0, PuntosSpawn.Length);

        Instantiate(EnemigoJefePrefab, PuntosSpawn[index].position, Quaternion.identity);

        enemigosActivos++;
    }

    private void SpawnEnemigoPerseguidor()
    {
        int index = Random.Range(0, PuntosSpawn.Length);

        Instantiate(EnemigoPerseguidorPrefab, PuntosSpawn[index].position, Quaternion.identity);

        enemigosActivos++;
    }

    public void EnemigoMuerto()
    {
        enemigosActivos--;
    }
}
