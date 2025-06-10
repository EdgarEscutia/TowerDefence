using UnityEngine;

[CreateAssetMenu(fileName = "NuevaOleada", menuName = "Oleadas/Definici�n de Oleada")]
public class DefinicionOleada : ScriptableObject
{
    [System.Serializable]
    public class BloqueEnemigos
    {
        public GameObject tipoEnemigos;     // Prefab del enemigo
        public int cantidad;               // N�mero de enemigos en este bloque
        public float enemigosPorSegundo;   // Frecuencia de aparici�n
    }

    public BloqueEnemigos[] bloques;
}
