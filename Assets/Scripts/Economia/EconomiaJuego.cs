using UnityEngine;
using UnityEngine.UI;

public class EconomiaJuego : MonoBehaviour
{
    public static EconomiaJuego instancia;

    public int dineroInicial = 500;
    public int dineroActual = 0;

    public Text textoDinero;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dineroActual = dineroInicial;
        ActualizarUI();
    }

    public bool PuedePagar(int cantidad)
    {
        return dineroActual >= cantidad;
    }

    public void Pagar(int cantidad)
    {
        dineroActual -= cantidad;
        ActualizarUI();
    }

    public void GanarDinero(int cantidad)
    {
        dineroActual += cantidad;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoDinero != null)
        {
            textoDinero.text = "$ " + dineroActual.ToString();
        }
    }
}

