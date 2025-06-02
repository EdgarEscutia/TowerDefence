using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DefinicionOleada", menuName = "Scriptable Objects/DefinicionOleada")]
public class DefinicionOleada : ScriptableObject
{
    [System.Serializable]
    public class BloqueEnemigos
    {
        public GameObject tipoEnemigos;
        public int cantidad;
        public float tiempoEntreEnemigos;
    }
    public BloqueEnemigos[] bloques;
}
