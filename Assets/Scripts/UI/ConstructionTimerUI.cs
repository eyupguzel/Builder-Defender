using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image buildingConstructionUIImage;
    private void Awake()
    {
        buildingConstructionUIImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }
    private void Update()
    {
        buildingConstructionUIImage.fillAmount =  buildingConstruction.GetBuildingConstructionTimerNormalized();
    }
}
