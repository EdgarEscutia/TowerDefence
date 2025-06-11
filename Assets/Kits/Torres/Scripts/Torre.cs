using UnityEngine;

public class Torre : MonoBehaviour, IGolpeable
{
    public float resistencia = 10f;
    public void RecibeDanyo(float cantidad)
    {
        resistencia -= cantidad;

        if (resistencia < 0) 
        {Destroy(gameObject);} 
    }
}
