using UnityEngine;
using DG.Tweening;
using UnityEngine.AdaptivePerformance.VisualScripting;

public partial class Proyectil : MonoBehaviour
{
    [Header("General")]
    [SerializeField] string[] tagsAfectados;
    [SerializeField] float tiempoMovimiento = 1f;

    [Header("Danyo")]
    [SerializeField] float danyoImpactoDirecto;
    [SerializeField] float radioDanyoArea;
    [SerializeField] float danyoAreaEnOrigen;
    [SerializeField] float danyoAreaEnBorde;
    


    [Header("SubProyectil")]
    [SerializeField] int subProyectilesAGenerar;
    [SerializeField] float radioSubProyectiles;
    [SerializeField] GameObject prefabSubProyectil;
    [SerializeField] float alturaSaltoSubProyectil = 5f;


    public void Init( Vector3 puntoInicial, Vector3 puntoFinal, float alturaSalto)
    {
        transform.position = puntoInicial;
        transform.DOJump(puntoFinal, alturaSalto, 1, tiempoMovimiento).OnComplete(() => RealizaLaDestruccion());
    }


    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IGolpeable>()?.RecibeDanyo(danyoImpactoDirecto);
        RealizaLaDestruccion();
    }

    private bool ColliderEsAfectable(Collider other)
    {
        bool esAfectable = false;
        foreach (string t in tagsAfectados)
        {
            if (other.CompareTag(t))
            {
                esAfectable = true;
            }
        }

        return esAfectable;
    }

    void RealizaLaDestruccion()
    {
        Destroy(gameObject);

        DanyaPorDestruccion();
        GeneraSubProyectiles();
    }

    private void DanyaPorDestruccion()
    {
        Collider[] objectosCercanos = Physics.OverlapSphere(transform.position, radioDanyoArea);
        foreach (Collider c in objectosCercanos)
        {
            if (ColliderEsAfectable(c))
            {
                float distanciaAlCentro = Vector3.Distance(c.transform.position, transform.position);
                float t = distanciaAlCentro / radioDanyoArea;

                float danyo = Mathf.Lerp(danyoAreaEnOrigen, danyoAreaEnBorde, Mathf.Clamp01(t));

                c.GetComponent<IGolpeable>().RecibeDanyo(danyo);
            }
        }
    }

    void GeneraSubProyectiles()
    {
        for (int i = 0; i < subProyectilesAGenerar; i++)
        {
            Vector2 randomPositionXY = Random.insideUnitCircle * radioSubProyectiles;
            Vector3 randomPosition = new Vector3(randomPositionXY.x, 0f, randomPositionXY.y);
            randomPosition *= radioSubProyectiles;
            randomPosition += transform.position;

            GameObject newProyectil = Instantiate(prefabSubProyectil);
            newProyectil.transform.position = transform.position;
            prefabSubProyectil.GetComponent<Proyectil>().Init(transform.position, randomPosition, alturaSaltoSubProyectil);
        }
    }
}
