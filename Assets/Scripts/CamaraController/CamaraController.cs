using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float velocidad = 10f;
    public float bordePantalla = 10f;
    public Vector2 limitesX = new Vector2(-50, 50);
    public Vector2 limitesZ = new Vector2(-50, 50);

    public float velocidadZoom = 20f;
    public float alturaMin = 10f;
    public float alturaMax = 50f;

    public float velocidadRotacion = 5f;

    private float rotacionY = 0f;

    void Update()
    {
        MoverCamara();
        HacerZoom();
        RotarCamara();
    }

    void MoverCamara()
    {
        Vector3 direccion = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - bordePantalla)
        {
            direccion += transform.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= bordePantalla)
        {
            direccion -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - bordePantalla)
        {
            direccion += transform.right;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= bordePantalla)
        {
            direccion -= transform.right;
        }

        direccion.y = 0f;

        Vector3 movimiento = direccion.normalized * velocidad * Time.deltaTime;
        Vector3 nuevaPos = transform.position + movimiento;

        nuevaPos.x = Mathf.Clamp(nuevaPos.x, limitesX.x, limitesX.y);
        nuevaPos.z = Mathf.Clamp(nuevaPos.z, limitesZ.x, limitesZ.y);

        transform.position = nuevaPos;
    }

    void HacerZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 posZoom = transform.position;
        posZoom.y -= scroll * velocidadZoom;
        posZoom.y = Mathf.Clamp(posZoom.y, alturaMin, alturaMax);
        transform.position = posZoom;
    }

    void RotarCamara()
    {
        if (Input.GetMouseButton(1))
        {
            float rotX = Input.GetAxis("Mouse X");
            rotacionY += rotX * velocidadRotacion;
            transform.rotation = Quaternion.Euler(0f, rotacionY, 0f);
        }
    }
}
