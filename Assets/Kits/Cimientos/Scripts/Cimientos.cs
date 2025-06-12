using UnityEngine;

public class Cimientos : MonoBehaviour
{
    [Header("Torres Arqueros")]
    [SerializeField] GameObject prefabTorreArquerosLv0;
    [SerializeField] GameObject prefabTorreArquerosLv1;
    [SerializeField] GameObject prefabTorreArquerosLv2;

    [Header("Torres Mágicas")]
    [SerializeField] GameObject prefabTorreMagicaLv0;
    [SerializeField] GameObject prefabTorreMagicaLv1;
    [SerializeField] GameObject prefabTorreMagicaLv2;

    [Header("Torres Cañones")]
    [SerializeField] GameObject prefabTorreCanyonesLv0;
    [SerializeField] GameObject prefabTorreCanyonesLv1;
    [SerializeField] GameObject prefabTorreCanyonesLv2;

    private Torre torreActual;
    private string tipoTorre;
    private GameObject objetoTorre;

    public void ConstruirTorreArqueros(int nivel)
    {
        DestruirTorreActual();

        if (nivel == 0)
            objetoTorre = Instantiate(prefabTorreArquerosLv0, transform.position, Quaternion.identity);
        else if (nivel == 1)
            objetoTorre = Instantiate(prefabTorreArquerosLv1, transform.position, Quaternion.identity);
        else
            objetoTorre = Instantiate(prefabTorreArquerosLv2, transform.position, Quaternion.identity);

        torreActual = objetoTorre.GetComponent<Torre>();
        tipoTorre = "TorreArqueros";
    }

    public void ConstruirTorreMagica(int nivel)
    {
        DestruirTorreActual();

        if (nivel == 0)
            objetoTorre = Instantiate(prefabTorreMagicaLv0, transform.position, Quaternion.identity);
        else if (nivel == 1)
            objetoTorre = Instantiate(prefabTorreMagicaLv1, transform.position, Quaternion.identity);
        else
            objetoTorre = Instantiate(prefabTorreMagicaLv2, transform.position, Quaternion.identity);

        torreActual = objetoTorre.GetComponent<Torre>();
        tipoTorre = "TorreMagica";
    }

    public void ConstruirTorreCanyones(int nivel)
    {
        DestruirTorreActual();

        if (nivel == 0)
            objetoTorre = Instantiate(prefabTorreCanyonesLv0, transform.position, Quaternion.identity);
        else if (nivel == 1)
            objetoTorre = Instantiate(prefabTorreCanyonesLv1, transform.position, Quaternion.identity);
        else
            objetoTorre = Instantiate(prefabTorreCanyonesLv2, transform.position, Quaternion.identity);

        torreActual = objetoTorre.GetComponent<Torre>();
        tipoTorre = "TorreCanyones";
    }

    public void DestruirTorreActual()
    {
        if (torreActual != null)
        {
            Destroy(torreActual.gameObject);
            torreActual = null;
        }
    }

    public bool HayTorreConstruida()
    {
        return torreActual != null;
    }

    public string TipoTorre()
    {
        return tipoTorre;
    }
}

