using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

[RequireComponent(typeof(SplineContainer))]
public class Enemigo : MonoBehaviour, IGolpeable
{
    [Header("Ajustes Modificables")]
    public SplineContainer ruta;
    public float velocidad = 4f;
    public float vida = 2f;
    public float distanciaEntrePuntos = 0.5f;

    private Vector3[] pathPoints;
    private int currentPointIndex = 0;
    private Vector3 nextPoint;

    void Start()
    {
        if (ruta == null)
        {
            Debug.LogError("Ruta no asignada al enemigo.");
            return;
        }

        CachearRuta();

        if (pathPoints.Length < 2)
        {
            Debug.LogError("La ruta tiene muy pocos puntos.");
            return;
        }

        transform.position = pathPoints[0];

        currentPointIndex = 1;
        nextPoint = pathPoints[currentPointIndex];
    }

    void Update()
    {
        if (pathPoints == null || currentPointIndex >= pathPoints.Length)
            return;

        Vector3 direction = (nextPoint - transform.position).normalized;
        transform.position += direction * velocidad * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, nextPoint);
        if (distance < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                nextPoint = pathPoints[currentPointIndex];
            }
        }
    }

    void CachearRuta()
    {
        Spline spline = ruta.Spline;

        int muestras = 100;
        float totalLength = 0f;
        Vector3 prevPoint = ruta.transform.TransformPoint(spline.EvaluatePosition(0f));

        for (int i = 1; i <= muestras; i++)
        {
            float t = i / (float)muestras;
            Vector3 localPos = spline.EvaluatePosition(t);
            Vector3 worldPos = ruta.transform.TransformPoint(localPos);
            totalLength += Vector3.Distance(prevPoint, worldPos);
            prevPoint = worldPos;
        }

        int numPoints = Mathf.CeilToInt(totalLength / distanciaEntrePuntos);
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
            Vector3 localPos = spline.EvaluatePosition(t);
            Vector3 worldPos = ruta.transform.TransformPoint(localPos);
            points.Add(worldPos);
        }

        pathPoints = points.ToArray();
    }

    public void RecibeDanyo(float cantidad)
    {
        vida -= cantidad;
        Debug.Log($"Enemigo recibe daño: {cantidad}, vida restante: {vida}");

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
