using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorArrow;
    [SerializeField] float hideDelay = 3f; // ���������� ���� �ð� (��)
    private float lastMouseMoveTime;

    private void Awake()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        lastMouseMoveTime = Time.time;
    }

    private void Update()
    {
        // ���콺 �������� �����Ǹ� �ð� ����
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            lastMouseMoveTime = Time.time;
            Cursor.visible = true; // ���콺 �������� �����Ǹ� Ŀ�� ǥ��
        }

        // ���� �ð� ���� �ƹ��� ������ ������ Ŀ�� ����
        if (Time.time - lastMouseMoveTime > hideDelay)
        {
            Cursor.visible = false;
        }
    }
}
