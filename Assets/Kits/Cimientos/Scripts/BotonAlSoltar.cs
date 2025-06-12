using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BotonAlSoltar : MonoBehaviour
{
    public UnityEvent accion;

    MenuDesplegable menuDesplegable;

    private void Awake()
    { menuDesplegable = GetComponentInParent<MenuDesplegable>(); }

    public void PunteroEntra()
    { menuDesplegable.EstableceEvento(accion); }

    public void PunteroSale()
    { menuDesplegable.EstableceEvento(null); }
       
    
}
