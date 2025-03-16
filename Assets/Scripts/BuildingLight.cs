using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuildingLight : MonoBehaviour
{
    private Light2D spotLight;

    private void Awake()
    {
        DayNightCycle.OnDayNightChange += HandleDayNightChange;

        spotLight = GetComponent<Light2D>();
        spotLight.enabled = false;
    }

    private void HandleDayNightChange(bool isDay)
    {
        if (isDay)
            spotLight.enabled = false;
        else
            spotLight.enabled = true;
    }
    private void OnDestroy()
    {
        DayNightCycle.OnDayNightChange -= HandleDayNightChange;
    }
}
