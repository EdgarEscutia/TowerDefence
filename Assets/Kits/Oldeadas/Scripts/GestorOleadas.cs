using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class GestorOleadas : MonoBehaviour
{
    [System.Serializable]
    public class LineaGuion
    {
        public float espera;                // Tiempo de espera antes de la oleada
        public DefinicionOleada oleada;    // Oleada a lanzar
    }

    [System.Serializable]
    public class GuionOleadas
    {
        public LineaGuion[] lineas;
    }

    [Header("Guion de Oleadas")]
    public GuionOleadas guion;

    [Header("Caminos disponibles")]
    public SplineContainer[] rutasDisponibles;

    void Start()
    {
        StartCoroutine(LeeGuion());
    }

    IEnumerator LeeGuion()
    {
        foreach (var linea in guion.lineas)
        {
            // Esperar antes de lanzar la oleada
            yield return new WaitForSeconds(linea.espera);
            // Lanzar la oleada
            yield return StartCoroutine(LanzaOleada(linea.oleada));
        }
    }

    IEnumerator LanzaOleada(DefinicionOleada oleada)
    {
        if (oleada == null)
        {
            Debug.LogWarning("Oleada nula en GestorOleadas.");
            yield break;
        }

        foreach (var bloque in oleada.bloques)
        {
            for (int i = 0; i < bloque.cantidad; i++)
            {
                // Esperar el tiempo según enemigosPorSegundo
                float esperaEntreEnemigos = 1f / bloque.enemigosPorSegundo;
                yield return new WaitForSeconds(esperaEntreEnemigos);

                if (rutasDisponibles == null || rutasDisponibles.Length == 0)
                {
                    Debug.LogError("No hay rutas asignadas en GestorOleadas.");
                    yield break;
                }

                // Elegir una ruta al azar
                SplineContainer rutaElegida = rutasDisponibles[Random.Range(0, rutasDisponibles.Length)];

                // Instanciar enemigo
                GameObject enemigoGO = Instantiate(bloque.tipoEnemigos);

                // Asignar la ruta al componente Enemigo
                Enemigo enemigo = enemigoGO.GetComponent<Enemigo>();
                if (enemigo == null)
                {
                    Debug.LogError("El prefab no tiene componente Enemigo.");
                    Destroy(enemigoGO);
                    yield break;
                }

                enemigo.ruta = rutaElegida;

                // El enemigo se posicionará automáticamente en el primer punto de la ruta en su Start()
            }
        }
    }
}
