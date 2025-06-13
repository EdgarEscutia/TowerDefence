using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camaraPivot; // Asigna el objeto que contiene la c�mara (el hijo de este GameObject)

    [Header("Movimiento")]
    public float velocidadMovimiento = 10f;

    [Header("Rotaci�n")]
    public float velocidadRotacion = 3f;
    public float minRotacionX = -60f;
    public float maxRotacionX = 60f;

    private float rotacionX = 0f;

    void Update()
    {
        // Movimiento con WASD o flechas
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(h, 0f, v) * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movimiento, Space.Self);

        // Rotaci�n con bot�n derecho del mouse
        if (Input.GetMouseButton(1))
        {
            float rotY = Input.GetAxis("Mouse X") * velocidadRotacion;
            float rotX = -Input.GetAxis("Mouse Y") * velocidadRotacion;

            // Rotaci�n horizontal del cuerpo
            transform.Rotate(0f, rotY, 0f);

            // Rotaci�n vertical del pivot
            rotacionX = Mathf.Clamp(rotacionX + rotX, minRotacionX, maxRotacionX);
            camaraPivot.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        }
    }
}
