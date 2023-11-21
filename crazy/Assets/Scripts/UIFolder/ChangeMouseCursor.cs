using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorArrow;
    [SerializeField] float hideDelay = 3f; // 숨기기까지의 지연 시간 (초)
    private float lastMouseMoveTime;

    private void Awake()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        lastMouseMoveTime = Time.time;
    }

    private void Update()
    {
        // 마우스 움직임이 감지되면 시간 갱신
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            lastMouseMoveTime = Time.time;
            Cursor.visible = true; // 마우스 움직임이 감지되면 커서 표시
        }

        // 일정 시간 동안 아무런 동작이 없으면 커서 숨김
        if (Time.time - lastMouseMoveTime > hideDelay)
        {
            Cursor.visible = false;
        }
    }
}
