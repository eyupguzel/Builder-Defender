using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StreetLight : MonoBehaviour
{
    private Light2D spotLight;
    private Animator animator;

    private DayNightCycle cycle;
    private void Awake()
    {
        DayNightCycle.OnDayNightChange += HandleDayNightChange;

        spotLight = GetComponent<Light2D>();
        animator = GetComponent<Animator>();

        HandleDayNightChange();
    }

    private void HandleDayNightChange()
    {
        switch (cycle.state)
        {
            case DayNightState.Day: 
                spotLight.enabled = false;
                animator.SetBool("IsOn", false);
                break;
            case DayNightState.Night: 
                spotLight.enabled = true;
                animator.SetBool("IsOn", true);
                break;
        }
    }
    private void OnDestroy()
    {
        DayNightCycle.OnDayNightChange -= HandleDayNightChange;
    }
}
