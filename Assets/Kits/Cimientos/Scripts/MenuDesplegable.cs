using UnityEngine;
using UnityEngine.Events;

public class MenuDesplegable : MonoBehaviour
{
    UnityEvent eventoParaLlamarAlOcultar = null;

    [SerializeField] GameObject panelTorresInicio;
    [SerializeField] GameObject panelTorreArquero;
    [SerializeField] GameObject pantelTorreMagica;
    [SerializeField] GameObject panelTorreCanones;


    public void EstableceEvento(UnityEvent nuevoEvento)
    {eventoParaLlamarAlOcultar = nuevoEvento; }
        
    public void Mostrar()
    {gameObject.SetActive(false); }
        
    public void Ocultar()
    {
        gameObject.SetActive(false);

        if(eventoParaLlamarAlOcultar != null)
        {
            eventoParaLlamarAlOcultar.Invoke();
            eventoParaLlamarAlOcultar = null;
        }
    }
}
