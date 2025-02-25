using UnityEngine;

public class SpriteRendererSortingOrder : MonoBehaviour
{
    [SerializeField] float offsetPositionY;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + offsetPositionY) * precisionMultiplier);
    }
}
