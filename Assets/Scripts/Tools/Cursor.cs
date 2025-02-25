using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : Singleton<Cursor>
{
    [SerializeField] Sprite cursor;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Vector3 mouseWorldPosition;

    public Action onClickGround;
    public Action onClickUI;
    private void Start()
    {
        UnityEngine.Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            onClickGround?.Invoke();
        /*else if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
            //onClickUI?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            SetCursorSprite(null);*/

        gameObject.transform.position = GetMouseWorldPosition();
    }
    public void SetCursorSprite(BuildingTypesSo buildType)
    {
        if(buildType != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = buildType.sprite;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            SetSpriteAlpha(0.5f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cursor;
            gameObject.transform.localScale = new Vector3(.5f, .5f, .5f);
            SetSpriteAlpha(1f);
        }
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public Vector3 GetMouseWorldPosition()
    {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
