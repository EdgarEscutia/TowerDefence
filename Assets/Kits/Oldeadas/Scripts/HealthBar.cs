using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]    Image sliderFill;
    [SerializeField]    Slider slider;
    [SerializeField]    Transform target;
    [SerializeField]    Vector3 vector3;

    Color blue;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;

        float porcentage = currentValue/maxValue * 100;
        CambiarColor(porcentage);
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        blue = new Color32(255, 128, 13, 255);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + vector3;
    }

    private void CambiarColor(float percentageOfHitPoints)
    {
        if (percentageOfHitPoints >= 50)
        { sliderFill.color = Color.green; }

        else if (percentageOfHitPoints >= 25)
        { sliderFill.color = blue;}
    
        else
        { sliderFill.color = Color.red; }
    }
}