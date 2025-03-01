using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform healthBar;
    private void Awake()
    {
        healthBar = transform.Find("bar");
    }
    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
       // SetHealthBarVisible();
    }
    private void HealthSystem_OnDamaged(object sendler, EventArgs e)
    {
        UpdateBar();
        SetHealthBarVisible();
    }
    private void UpdateBar()
    {
        healthBar.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1, 1);
    }
    private void SetHealthBarVisible()
    {
        if(healthSystem.IsHealthFull())
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
