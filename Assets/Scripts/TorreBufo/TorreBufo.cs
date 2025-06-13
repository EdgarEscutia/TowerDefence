using UnityEngine;
using System.Collections.Generic;

public class TorreBufo : MonoBehaviour
{
    [Header("Buff")]
    public float radioDeBuffo = 5f;
    public float multiplicadorVelocidadDisparo = 0.8f; // Ej: 0.8 = 20% más rápido

    private List<Disparador> torresBuffadas = new List<Disparador>();

    void Update()
    {
        AplicarBuffo();
    }

    void AplicarBuffo()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioDeBuffo);
        torresBuffadas.Clear();

        foreach (Collider col in colliders)
        {
            Disparador torreDisparo = col.GetComponent<Disparador>();

            if (torreDisparo != null && !torresBuffadas.Contains(torreDisparo))
            {
                if (col.gameObject != gameObject) // Evita buffearse a sí misma si tuviera Disparador
                {
                    torreDisparo.tiempoEntreProyectiles *= multiplicadorVelocidadDisparo;
                    torresBuffadas.Add(torreDisparo);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeBuffo);
    }
}
