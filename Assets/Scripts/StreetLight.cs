using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StreetLight : MonoBehaviour
{
    private Light2D spotLight;
    private Animator animator;

    private void Awake()
    {
        DayNightCycle.OnDayNightChange += HandleDayNightChange;

        spotLight = GetComponent<Light2D>();
        animator = GetComponent<Animator>();

        spotLight.enabled = false;
        animator.SetBool("IsOn", false);
    }

    private void HandleDayNightChange(bool isDay)
    {
        if (isDay)
        {
            spotLight.enabled = false;
            animator.SetBool("IsOn", false);
        }
        else
        {
            spotLight.enabled = true;
            animator.SetBool("IsOn", true);
        }
    }
    private void OnDestroy()
    {
        DayNightCycle.OnDayNightChange -= HandleDayNightChange;
    }
}
