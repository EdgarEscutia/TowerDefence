using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Enemigo enemigo;
    public Image imagenBarra;

    void Update()
    {
        if (enemigo != null)
        {
            float vidaActual = Mathf.Clamp01(enemigo.vida / enemigo.vidaMaxima);
            imagenBarra.fillAmount = vidaActual;

            // Siempre mirar hacia la cámara
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
        else
        {
            Destroy(gameObject); // Por si el enemigo muere
        }
    }
}
