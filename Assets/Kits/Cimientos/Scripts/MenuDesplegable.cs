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
        if (cimiento == null) return;

        if (!cimiento.HayTorreConstruida())
        {
            panelTorres.SetActive(true);
        }
        else
        {
            string tipo = cimiento.TipoTorre();

            if (tipo == "TorreArqueros")
                panelTorreArquero.SetActive(true);
            else if (tipo == "TorreMagica")
                panelTorreMagica.SetActive(true);
            else if (tipo == "TorreCanyones")
                panelTorreCanyones.SetActive(true);
        }
    }

    public void Ocultar()
    {
        panelTorres.SetActive(false);
        panelTorreArquero.SetActive(false);
        panelTorreMagica.SetActive(false);
        panelTorreCanyones.SetActive(false);

        if (eventoAlOcultar != null)
        {
            eventoAlOcultar.Invoke();
            eventoAlOcultar = null;
        }
    }
}
