using UnityEngine;

public class Disparador : MonoBehaviour
{
    [Header("DETECCION")]
    public float radioDeteccion = 10f;
    public float radioPerdidaObjetivo = 15f;
    public string[] tagsObjetivos;

    [Header("DETECCION")]
    public GameObject prefabProyectil;
    public float tiempoEntreProyectiles = 1.5f;
    public Transform origenProyectil;
    public float alturaSaltoProyectil = 3f;

    Transform objetivo;
    float tiempoParaElSiguienteProyectil = 0f;

    private void Update()
    {
        tiempoParaElSiguienteProyectil -= Time.deltaTime;
        tiempoParaElSiguienteProyectil = Mathf.Max(0f, tiempoParaElSiguienteProyectil);

        if(objetivo == null)
        { BuscarObjetivo(); }

        else
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            if(distancia > radioPerdidaObjetivo || objetivo == null)
            { objetivo = null; }
        }

        if(objetivo != null && tiempoParaElSiguienteProyectil <= 0f)
        { 
            Disparar();
            tiempoParaElSiguienteProyectil = tiempoEntreProyectiles;
        }
    }

    void BuscarObjetivo()
    {
        Collider[] posibles = Physics.OverlapSphere(transform.position, radioDeteccion);
        Transform mejorObjetivo = null;

        float distanciaMasCercana = Mathf.Infinity;

        

        foreach (Collider col in posibles)
        {
            foreach (string tagObjetivo in tagsObjetivos)
            {
                if (col.CompareTag(tagObjetivo))
                {
                    float dist = Vector3.Distance(transform.position, col.transform.position);
                    if (dist < distanciaMasCercana)
                    {
                        distanciaMasCercana = dist;
                        mejorObjetivo = col.transform;
                    }
                }
            }
        }

        objetivo = mejorObjetivo;
    }

    void Disparar()
    {
        //COMPROBAR ERRORES DE NULLS(POR SI FALTA ALGO)
        if(prefabProyectil == null || origenProyectil == null || objetivo == null) 
        {  return;  }
          
      
        //INSTANCIAR EL PROYECTIL EN COORDENADA CORRECTA
        GameObject proyectilGO = Instantiate(prefabProyectil, origenProyectil.position, Quaternion.identity);
        Proyectil proyectil = proyectilGO.GetComponent<Proyectil>();

        //INICIALIZA EL COMPONENTE
        if (proyectil != null)
        { proyectil.Init(origenProyectil.position, objetivo.position, alturaSaltoProyectil);}
           
        
    }
}
