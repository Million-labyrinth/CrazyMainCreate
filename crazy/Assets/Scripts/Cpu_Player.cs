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
        // Ÿ�ٰ� �÷��̾��� ��ġ ���� ���
        Vector3 direction = target.position - transform.position;

        // �밢�� �̵� ����
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

        // �̵� �ڵ� �ۼ�
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
