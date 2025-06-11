using UnityEngine;

[CreateAssetMenu(fileName = "NuevaOleada", menuName = "Oleadas/Definici�n de Oleada")]
public class DefinicionOleada : ScriptableObject
{
    [System.Serializable]
    public class BloqueEnemigos
    {
        // PREFAB DEL ENEMIGO
        public GameObject tipoEnemigos; 

         // N�MERO DE ENEMIGOS EN ESTE BLOQUE
        public int cantidad;

        // FRECUENCIA DE APARICI�N
        public float enemigosPorSegundo;  
    }

    public BloqueEnemigos[] bloques;
}
