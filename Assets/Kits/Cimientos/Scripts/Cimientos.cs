using UnityEngine;

public class Cimientos : MonoBehaviour
{
    [Header("TORRES PREFAB")]
    public GameObject torreArqueroPrefab;
    public GameObject torreMagicaPrefab;
    public GameObject torreCanonesPrefab;



    Torre torreContruida = null;

    public bool HayTorreContruida()
    {
        return torreContruida != null;
    }

    public void ContruyeTorreArqueros()
    {
        if (HayTorreContruida())
        { return; }
            
        GameObject nuevaTorre = Instantiate(torreArqueroPrefab, transform.position, Quaternion.identity);
        torreContruida = nuevaTorre.GetComponent<Torre>();

        Debug.Log("TORRE ARQUEROS CONTRUIDA");
    }

    public void ContruyeTorreMagica()
    {
        if (HayTorreContruida())
        { return; }

        GameObject nuevaTorre = Instantiate(torreMagicaPrefab, transform.position, Quaternion.identity);
        torreContruida = nuevaTorre.GetComponent<Torre>();

        Debug.Log("TORRE MAGICA CONTRUIDA");
    }

    public void ContruyeTorreCoñones()
    {
        if (HayTorreContruida())
        { return; }

        GameObject nuevaTorre = Instantiate(torreMagicaPrefab, transform.position, Quaternion.identity);
        torreContruida = nuevaTorre.GetComponent<Torre>();

        Debug.Log("TORRE CAÑON CONTRUIDA");
    }
}
