using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; // ������ �ӵ�
    private float initialY; // �ʱ� Y ��ǥ
    private float minY; // �ּ� Y ��ǥ
    private float maxY; // �ִ� Y ��ǥ

    private int direction = 1; // ������ ���� (1: ����, -1: �Ʒ���)

    void Start()
    {
        // �ʱ� Y ��ǥ ����
        initialY = transform.position.y;
        minY = initialY;
        maxY = initialY + 0.1f;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.y += moveSpeed * direction * Time.deltaTime;

        // ���� ������ ����� ������ �ݴ�� �ٲ�
        if (currentPosition.y >= maxY)
        {
            currentPosition.y = maxY;
            direction = -1; // �Ʒ��� �̵�
        }
        else if (currentPosition.y <= minY)
        {
            currentPosition.y = minY;
            direction = 1; // ���� �̵�
        }

        transform.position = currentPosition;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "upWater" || other.gameObject.tag == "downWater" || other.gameObject.tag == "leftWater" || other.gameObject.tag == "rightWater" || other.gameObject.tag == "BalloonCollider")
        {

            gameObject.SetActive(false);

            Debug.Log(other.name);

        }
    }
}
