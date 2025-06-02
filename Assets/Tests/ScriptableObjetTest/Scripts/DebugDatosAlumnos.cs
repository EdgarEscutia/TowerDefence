using UnityEngine;

public class DebugDatosAlumnos : MonoBehaviour
{
    [SerializeField] DatosAlumnoFTW[] datosAlumno;

    private void Start()
    {
        foreach (DatosAlumnoFTW da in datosAlumno) 
        {
            DatosAlumnoFTW instanciaDA = Instantiate(da);
            instanciaDA.DebugAlumno();
        }
    }
}
