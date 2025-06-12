using UnityEngine;
using UnityEngine.Events;

public class MenuDesplegable : MonoBehaviour
{
    [SerializeField] GameObject panelTorres;
    [SerializeField] GameObject panelTorreArquero;
    [SerializeField] GameObject panelTorreMagica;
    [SerializeField] GameObject panelTorreCanyones;

    private UnityEvent eventoAlOcultar;
    private Cimientos cimiento;

    void Awake()
    {
        panelTorres.SetActive(false);
        panelTorreArquero.SetActive(false);
        panelTorreMagica.SetActive(false);
        panelTorreCanyones.SetActive(false);

        cimiento = GetComponentInParent<Cimientos>();
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void EstableceEvento(UnityEvent accion)
    {
        eventoAlOcultar = accion;
    }

    public void Mostrar()
    {
        if (!cimiento.HayTorreConstruida())
        {
            panelTorres.gameObject.SetActive(true);
        }
        
        else if (cimiento.TipoTorre() == "TorreArqueros")
        {
            panelTorreArquero.gameObject.SetActive(true);
        }  
        
        else if (cimiento.TipoTorre() == "TorreMagica")
        {
            panelTorreMagica.gameObject.SetActive(true);
        }   
        
        else if (cimiento.TipoTorre() == "TorreCanyones")
        {
            panelTorreCanyones.gameObject.SetActive(true);
        }
            
    }

    public void Ocultar()
    {
        panelTorres.SetActive(false);
        panelTorreArquero.SetActive(false);
        panelTorreMagica.SetActive(false);
        panelTorreCanyones.SetActive(false);

        eventoAlOcultar?.Invoke();
    }
}
