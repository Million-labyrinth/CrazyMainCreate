using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAi : MonoBehaviour
{
    private float xMin = -8.0f;
    private float xMax = 8.0f;
    private float yMin = -7.0f;
    private float yMax = 7.0f;
    private Vector3 targetPosition;
    private float moveSpeed = 2.0f;

    void Awake()
    {
        // ���� �� �ʱ� ��ġ ����

        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ���� ��ǥ ��ġ�� �����ߴٸ�, �ٸ� ���� ��ġ�� ����
        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
        AIRay();
    }

    Vector3 GetRandomPosition()
    {
        // �����ϰ� x �Ǵ� y ��ǥ�� ����
        bool chooseX = Random.Range(0, 2) == 0; // true�� x, false�� y
        float randomX = chooseX ? Random.Range(xMin, xMax) : transform.position.x;
        float randomY = chooseX ? transform.position.y : Random.Range(yMin, yMax);

        return new Vector3(randomX, randomY, 0);
    }
    void AIRay()
    {
        // �浹 üũ
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        // �浹�� �����Ǹ� ���ο� ��ġ ����
        if (upRayHit.collider != null)
        {
            yMax = transform.position.y;
            targetPosition = GetRandomPosition();
            yMax = 7.0f;
        }
        else if(downRayHit.collider != null)
        {
            yMin = transform.position.y;
            targetPosition = GetRandomPosition();
            yMin = -7.0f;
        }
        else if(leftRayHit.collider != null)
        {
            xMin = transform.position.x;
            targetPosition = GetRandomPosition();
            xMin = -8.0f;
        }
        else if(rightRayHit.collider != null)
        {
            xMax = transform.position.x;
            targetPosition = GetRandomPosition();
            xMax = 7.0f;
        }
    }

}
