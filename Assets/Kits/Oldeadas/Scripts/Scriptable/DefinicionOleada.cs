using UnityEngine;

[CreateAssetMenu(fileName = "NuevaOleada", menuName = "Oleadas/Definición de Oleada")]
public class DefinicionOleada : ScriptableObject
{
    [System.Serializable]
    public class BloqueEnemigos
    {
        public GameObject tipoEnemigos;     // Prefab del enemigo
        public int cantidad;               // Número de enemigos en este bloque
        public float enemigosPorSegundo;   // Frecuencia de aparición
    }

    public BloqueEnemigos[] bloques;
}
