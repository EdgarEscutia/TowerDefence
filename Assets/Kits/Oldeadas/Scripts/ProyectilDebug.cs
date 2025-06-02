using UnityEngine;

public partial class Proyectil : MonoBehaviour
{

    [Header("Debug")]
    [SerializeField] bool debugDisparo;
    [SerializeField] Transform debugPuntoInicial;
    [SerializeField] Transform debugPuntoFinal;
    [SerializeField] float debugAlturaSalto;

    public void OnValidate()
    {
        if(debugDisparo)
        { 
            debugDisparo = false;
            Init(debugPuntoInicial.transform.position, debugPuntoFinal.transform.position, debugAlturaSalto);
        }
       
    }
}
