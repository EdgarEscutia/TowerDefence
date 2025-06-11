using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class GestorOleadas : MonoBehaviour
{
    [System.Serializable]
    public class LineaGuion
    {
        public float espera;                 
        public DefinicionOleada oleada;
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
            yield return new WaitForSeconds(linea.espera);
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
                float esperaEntreEnemigos = 1f / bloque.enemigosPorSegundo;
                yield return new WaitForSeconds(esperaEntreEnemigos);

                if (rutasDisponibles == null || rutasDisponibles.Length == 0)
                {
                    Debug.LogError("No hay rutas asignadas en GestorOleadas.");
                    yield break;
                }

                // Ruta aleatoria
                SplineContainer rutaElegida = rutasDisponibles[Random.Range(0, rutasDisponibles.Length)];

                // Instanciar enemigo
                GameObject enemigoGO = Instantiate(bloque.tipoEnemigos);

                // Posicionar al inicio de la ruta
                Vector3 inicioRuta = rutaElegida.Spline.EvaluatePosition(0f);
                enemigoGO.transform.position = inicioRuta;

                // Asignar ruta
                Enemigo enemigo = enemigoGO.GetComponent<Enemigo>();
                if (enemigo == null)
                {
                    Debug.LogError("El prefab no tiene componente Enemigo.");
                    Destroy(enemigoGO);
                    continue;
                }

                enemigo.ruta = rutaElegida;
            }
        }
    }
}
