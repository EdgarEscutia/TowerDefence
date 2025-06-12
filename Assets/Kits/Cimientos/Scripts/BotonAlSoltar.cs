using UnityEngine;
using UnityEngine.Events;

public class BotonAlSoltar : MonoBehaviour
{
    public UnityEvent accion;
    MenuDesplegable menu;

    private void Awake()
    {menu = GetComponentInParent<MenuDesplegable>(); }

    public void EntraPuntero()
    {
        if(menu != null)
        { menu.EstableceEvento(accion); }
    }

    public void SalePuntero()
    {
        if(menu != null)
        { menu.EstableceEvento(null); }
    }
}
