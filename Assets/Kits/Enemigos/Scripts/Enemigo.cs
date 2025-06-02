using UnityEngine;
using UnityEngine.Splines;

public class Enemigo : MonoBehaviour
{
    public SplineContainer ruta;
    [SerializeField] float velocidad = 4f;
    [SerializeField] float vida, vidaMaxima = 2f;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] float umbralLlegada = 1f;

    float distanciaEntrePuntos = 5f;

    Vector3[] pathPointsCache;
    Vector3 posicionSiguiente;
    int indiceSiguientePosicion = 1;

    private void Start()
    {
        float longitudRuta = ruta.CalculateLength();
        int cantidadPuntos = Mathf.CeilToInt(longitudRuta / distanciaEntrePuntos) + 1;

        pathPointsCache = new Vector3[cantidadPuntos];

        for (int i = 0; i < cantidadPuntos; i++)
        {
            float t = (float)i / (float)cantidadPuntos;
            pathPointsCache[i] = ruta.EvaluatePosition(t);
        }

        transform.position = pathPointsCache[0];
        posicionSiguiente = pathPointsCache[indiceSiguientePosicion];
        vida = vidaMaxima;
        healthBar.UpdateHealthBar(vida, vidaMaxima);
        GameManager.instance.NotificaEnemigoCreado();

    }

    private void Update()
    {
        Vector3 direccion= posicionSiguiente - transform.position;
        Vector3 velocidadMovimiento = direccion.normalized * velocidad;
        transform.position += velocidadMovimiento * Time.deltaTime;

        if(Vector3.Distance(posicionSiguiente, transform.position) < umbralLlegada)
        {
            indiceSiguientePosicion++;
            if(indiceSiguientePosicion == pathPointsCache.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                posicionSiguiente = pathPointsCache[indiceSiguientePosicion];
            }
        }
    }

}
