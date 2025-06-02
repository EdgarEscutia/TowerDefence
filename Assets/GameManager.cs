using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Estadisticas")]
    [SerializeField] private float resistencia;
    [SerializeField] TMP_Text textoVidas;
    [SerializeField] private GameObject derrotaCanvas;
    [SerializeField] private GameObject victoriaCanvas;

    float cantidadDeEnemigos;
    bool ultimoEnemigoCreado = false;

    private void Awake()
    {
        if (instance == null)
        { 
            //Destroy
            if(instance != null && instance != this)
            {Destroy(gameObject); return;}

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if(resistencia <= 0)
        { StartCoroutine(FinalPartida(false));}
           
        if(cantidadDeEnemigos <= 0 && ultimoEnemigoCreado)
        { StartCoroutine(FinalPartida(true)); }

        textoVidas.text = $"Vidas: {resistencia}";
    }

    public void NotificaEnemigoCreado() { cantidadDeEnemigos++; }
    public void NotificaEnemigoDestruido() { cantidadDeEnemigos--; }
    public void NotificaEnemigoLlegaAlFinal() { resistencia++; }
    public void NotificaUltimoEnemigoCreado() { ultimoEnemigoCreado = true; }

    IEnumerator FinalPartida(bool victory)
    {
        if (victory)
        { victoriaCanvas.SetActive(true); }

        else { victoriaCanvas.SetActive(false); }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}