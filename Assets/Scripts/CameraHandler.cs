using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineCamera cam;

    [SerializeField] private float speed;
    Vector3 newPosition;
    private float x;
    private float y;

    float orthographicSize;
    float targetOrthographicSize;
    float mouseMaxLimit = 25;
    float mouseMinLimit = 10;

    private void Update()
    {
        NewPosition();
        NewOrthographicSize();
    }

    private void NewOrthographicSize()
    {
        targetOrthographicSize += -Input.mouseScrollDelta.y * 2;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, mouseMinLimit, mouseMaxLimit);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * 5f);

        cam.Lens.OrthographicSize = orthographicSize;
    }

    private void NewPosition()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        newPosition = new Vector3(x, y).normalized;
        transform.position += newPosition * speed * Time.deltaTime;
    }
}
