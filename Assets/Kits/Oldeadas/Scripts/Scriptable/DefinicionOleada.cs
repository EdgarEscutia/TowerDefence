using UnityEngine;

[CreateAssetMenu(fileName = "NuevaOleada", menuName = "Oleadas/Definición de Oleada")]
public class DefinicionOleada : ScriptableObject
{
    [System.Serializable]
    public class BloqueEnemigos
    {
        // PREFAB DEL ENEMIGO
        public GameObject tipoEnemigos; 

         // NÚMERO DE ENEMIGOS EN ESTE BLOQUE
        public int cantidad;

        // FRECUENCIA DE APARICIÓN
        public float enemigosPorSegundo;  
    }

    public BloqueEnemigos[] bloques;
}
