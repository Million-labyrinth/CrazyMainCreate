using UnityEngine;

public class Cpu_Player : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;

    private void Update()
    {
        TrackTarget(target);
    }

    public void TrackTarget(Transform target)
    {
        // 타겟과 플레이어의 위치 차이 계산
        Vector3 direction = target.position - transform.position;

        // 대각선 이동 제한
        float diagonalLimit = Mathf.Sqrt(2) / 2;
        if (Mathf.Abs(direction.x) > minimumDistance && Mathf.Abs(direction.z) > minimumDistance)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                direction.z = 0;
            }
            else
            {
                direction.x = 0;
            }
        }

        // 이동 코드 작성
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
