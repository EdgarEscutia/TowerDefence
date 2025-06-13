using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TMP_Text resistenciaText;
    [SerializeField] private GameObject derrotaPanel;
    [SerializeField] private GameObject victoriaPanel;

    [Header("Game Settings")]
    [SerializeField] private float vida = 10f;

    private int enemigosActivos = 0;
    private bool ultimoEnemigoSpawned = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        ActualizarEstadoJuego();
        ActualizarUI();
    }

    private void ActualizarEstadoJuego()
    {
        if (vida <= 0)
        {
            StartCoroutine(TerminarPartida(false));
        }
        else if (enemigosActivos <= 0 && ultimoEnemigoSpawned)
        {
            StartCoroutine(TerminarPartida(true));
        }
    }

    private void ActualizarUI()
    {
        resistenciaText.text = $"HP: {vida}";
    }

    public void EnemigoCreado()
    {
        enemigosActivos++;
    }

    public void EnemigoDestruido()
    {
        enemigosActivos--;
    }

    public void EnemigoLlegoAlFinal()
    {
        vida--;
    }

    public void UltimoEnemigoSpawned()
    {
        ultimoEnemigoSpawned = true;
    }

    private IEnumerator TerminarPartida(bool victoria)
    {
        if (victoria)
            victoriaPanel.SetActive(true);
        else
            derrotaPanel.SetActive(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
