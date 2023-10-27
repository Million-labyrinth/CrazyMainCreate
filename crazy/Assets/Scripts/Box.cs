using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float minPushForce = 2.0f; // 최소 밀리는 힘
    public float maxPushForce = 5.0f; // 최대 밀리는 힘
    Rigidbody2D boxRigidbody;

    void Start()
    {
        // 박스 오브젝트의 Rigidbody 컴포넌트를 가져옵니다.
        boxRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 부딪힌 경우:
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌 시 무작위로 힘을 설정합니다.
            float pushForce = Random.Range(minPushForce, maxPushForce);

            // 플레이어와의 충돌 방향을 가져옵니다.
            Vector2 pushDirection = -collision.contacts[0].normal;

            // 힘을 가합니다.
            boxRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
