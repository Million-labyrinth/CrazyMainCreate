using UnityEngine;

public class CpuPlayer : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2[] waypoints; // ������ ��ǥ �迭
    int currentWaypointIndex = 0; // ���� ������ �ε���
    public float moveSpeed = 3.0f; // ������ �̵� �ӵ�

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        // ������ ��ǥ �迭 �ʱ�ȭ
        waypoints = new Vector2[] {
            new Vector2(-1, 0),
            new Vector2(3, 0),
            new Vector2(3, 3),
            new Vector2(3, 0)
        };

        // �ʱ� �̵� ���� ����
        SetNextWaypoint();
    }

    void FixedUpdate()
    {
        // ���͸� ���� �������� �̵���Ŵ
        Vector2 direction = (waypoints[currentWaypointIndex] - (Vector2)transform.position).normalized;
        rigid.velocity = direction * moveSpeed;

        // �������� �����ϸ� ���� �������� ����
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        // ���� �������� �����ϰ� �ε����� ������Ʈ
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}