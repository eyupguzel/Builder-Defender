using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum DayNightState
{
    Day,
    Night
}

public class DayNightCycle : Singleton<DayNightCycle>
{
    public static event Action OnDayNightChange;

    private Light2D globalLight;

    [SerializeField] private float transitionDuration = 120f;
    private float nightIntensity = 0.3f; 
    private float dayIntensity = 1f; 
    private float timer = 0f;

    public DayNightState state;

    protected override void Init()
    {
        globalLight = GetComponent<Light2D>();
        state = DayNightState.Day;
    }


    void Update()
    {
        timer += Time.deltaTime;
        float lerpFactor = Mathf.Clamp01(timer / transitionDuration);

        switch (state)
        {
            case DayNightState.Day:
                globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, lerpFactor);
                break;
            case DayNightState.Night:
                globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpFactor);
                break;
        }
        if (timer >= transitionDuration)
        {
            timer = 0f;
            
            state = state == DayNightState.Day ? DayNightState.Night : DayNightState.Day;

            OnDayNightChange?.Invoke();
        }
    }
}
