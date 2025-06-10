using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Proyectil : MonoBehaviour
{
    [Header("Daño")]
    public float danyoImpactoDirecto = 0f;
    public float radioDanyoArea = 0f;
    public float danyoAreaEnOrigen = 0f;
    public float danyoAreaEnBorde = 0f;

    [Header("SubProyectiles")]
    public int subProyectilesAGenerar = 0;
    public float radioSubProyectiles = 0f;
    public GameObject prefabSubProyectil;

    [Header("Tags Afectados")]
    public string[] tagsAfectados;

    [Header("Debug")]
    public Transform debugTransformPuntoFinal;
    public bool debugTransformInit = false;

    private Vector3 puntoInicial;
    private Vector3 puntoFinal;
    private float alturaSalto;

    private bool impactoDirecto = false;
    private Collider colliderImpactadoDirecto;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    private void OnValidate()
    {
        if (debugTransformInit && debugTransformPuntoFinal != null)
        {
            debugTransformInit = false;
            Init(transform.position, debugTransformPuntoFinal.position, 2f);
        }
    }

    public void Init(Vector3 puntoInicial, Vector3 puntoFinal, float alturaSalto)
    {
        this.puntoInicial = puntoInicial;
        this.puntoFinal = puntoFinal;
        this.alturaSalto = alturaSalto;

        transform.position = puntoInicial;

        float duracion = Vector3.Distance(puntoInicial, puntoFinal) / 5f; // velocidad arbitraria

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(puntoFinal, alturaSalto, 1, duracion).SetEase(Ease.Linear));
        seq.OnComplete(() => {
            TerminaRecorrido();
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in tagsAfectados)
        {
            if (other.CompareTag(tag))
            {
                IGolpeable golpeable = other.GetComponent<IGolpeable>();
                if (golpeable != null)
                {
                    golpeable.RecibeDanyo(danyoImpactoDirecto);
                    impactoDirecto = true;
                    colliderImpactadoDirecto = other;
                }
                DestruyeProyectil();
                break;
            }
        }
    }

    private void TerminaRecorrido()
    {
        DestruyeProyectil();
    }

    private void DestruyeProyectil()
    {
        // Daño de área
        if (radioDanyoArea > 0f)
        {
            Collider[] colisiones = Physics.OverlapSphere(transform.position, radioDanyoArea);
            foreach (Collider col in colisiones)
            {
                foreach (string tag in tagsAfectados)
                {
                    if (col.CompareTag(tag))
                    {
                        IGolpeable golpeable = col.GetComponent<IGolpeable>();
                        if (golpeable != null)
                        {
                            float distancia = Vector3.Distance(transform.position, col.transform.position);
                            float damage = Mathf.Lerp(danyoAreaEnOrigen, danyoAreaEnBorde, distancia / radioDanyoArea);
                            golpeable.RecibeDanyo(damage);
                        }
                        break;
                    }
                }
            }
        }

        // Generar subproyectiles
        if (subProyectilesAGenerar > 0 && prefabSubProyectil != null)
        {
            for (int i = 0; i < subProyectilesAGenerar; i++)
            {
                Vector3 randomOffset = Random.insideUnitSphere * radioSubProyectiles;
                randomOffset.y = Mathf.Abs(randomOffset.y); // que no caiga por debajo del suelo

                Vector3 posSpawn = transform.position + randomOffset;

                GameObject subProy = Instantiate(prefabSubProyectil, posSpawn, Quaternion.identity);

                Proyectil subProyComp = subProy.GetComponent<Proyectil>();
                if (subProyComp != null)
                {
                    subProyComp.Init(posSpawn, posSpawn, 0.2f);

                    if (impactoDirecto && colliderImpactadoDirecto != null)
                    {
                        Collider colSub = subProy.GetComponent<Collider>();
                        if (colSub != null)
                            Physics.IgnoreCollision(colSub, colliderImpactadoDirecto);
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}
