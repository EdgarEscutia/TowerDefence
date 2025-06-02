using UnityEngine;

[CreateAssetMenu(fileName = "DatosAlumno", menuName = "Scriptable Objects/DatosAlumno")]
public class DatosAlumnoFTW : ScriptableObject
{
    public string nombre = "<Nombre>";
    public string primerApellido = "<PrimerApellido>";
    public string segundoApellido = "<SegundoApellido>";


    [Range(0f,10f)] public float notaRedes = 10f;
    [Range(0f, 10f)] public float notaProyecto= 10f;
    [Range(0f, 10f)] public float notaMoviles= 10f;


    public void DebugAlumno()
    {
        Debug.Log(
                $"Nombre: {nombre} \n" +
                $"Primer Apellido: {primerApellido} \n" +
                $"Segundo Apellido: {segundoApellido} \n");
    }


}
