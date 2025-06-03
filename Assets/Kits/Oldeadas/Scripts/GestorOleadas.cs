using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class GestorOleadas : MonoBehaviour
{
    [SerializeField] SplineContainer[] rutas;
    [SerializeField] GuionOleadas guionOleadas;

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
  
    float contador = 0f;
    bool enemigoCreado = false;


    private void Start()
    {
        StartCoroutine(LeeGion());
    }

    IEnumerator LeeGion()
    {
        for (int i = 0; i < guionOleadas.lineas.Length; i++)
        {
            
            StartCoroutine(LanzaOleadas(guionOleadas.lineas[i].oleada));
            yield return new WaitForSeconds(guionOleadas.lineas[i].espera);

        }
        GameManager.instance.NotificaUltimoEnemigoCreado();

    }

    public IEnumerator LanzaOleadas(DefinicionOleada oleada)
    {
        // Pasa por todos los BLOQUES de la OLEADA
        for (int i = 0; i < oleada.bloques.Length; i++)
        {
            // Instancia la CANTIDAD de ENEMIGOS del BLOQUE con su RUTA
            for (int j = 0; j < oleada.bloques[i].cantidad; j++)
            {
                Enemigo enemigo = Instantiate(oleada.bloques[i].tipoEnemigos, Vector3.zero, Quaternion.identity).GetComponent<Enemigo>();

                enemigo.EstablecerRuta(rutas[System.Random.Range(0, rutas.Length)]);

                yield return new WaitForSeconds(oleada.bloques[i].tiempoEntreEnemigos);
            }
        }

    }
}