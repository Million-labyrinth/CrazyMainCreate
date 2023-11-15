using UnityEngine;

public class CPUPlayer : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2[] waypoints; // 목적지 좌표 배열
    int currentWaypointIndex = 0; // 현재 목적지 인덱스
    public float moveSpeed = 3.0f; // 몬스터의 이동 속도

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        // 목적지 좌표 배열 초기화
        waypoints = new Vector2[] {
            new Vector2(-1, 0),
            new Vector2(3, 0),
            new Vector2(3, 3),
            new Vector2(3, 0)
        };

        // 초기 이동 방향 설정
        SetNextWaypoint();
    }

    void FixedUpdate()
    {
        // 몬스터를 현재 목적지로 이동시킴
        Vector2 direction = (waypoints[currentWaypointIndex] - (Vector2)transform.position).normalized;
        rigid.velocity = direction * moveSpeed;

        // 목적지에 도착하면 다음 목적지로 설정
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        // 다음 목적지로 설정하고 인덱스를 업데이트
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}