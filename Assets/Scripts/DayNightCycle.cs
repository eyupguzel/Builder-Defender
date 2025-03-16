using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public static event Action<bool> OnDayNightChange;

    private Light2D globalLight;

    [SerializeField] private float transitionDuration = 120f;
    private float nightIntensity = 0.3f; 
    private float dayIntensity = 1f; 

    private bool isDay = true;
    private float timer = 0f;

    void Awake()
    {
        globalLight = GetComponent<Light2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float lerpFactor = Mathf.Clamp01(timer / transitionDuration);

        if (isDay)
        {
            globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, lerpFactor);
        }
        else
        {
            globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpFactor);
        }

        if (timer >= transitionDuration)
        {
            timer = 0f;
            isDay = !isDay;

            OnDayNightChange?.Invoke(isDay);
        }
    }
}
