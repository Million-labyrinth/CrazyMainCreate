using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grass : MonoBehaviour
{
    public SpriteRenderer playerRenderer;

    private void Awake()
    {
        // 여기서 playerRenderer를 초기화해야 합니다.
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 스프라이트 렌더러를 끕니다.
            playerRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 스프라이트 렌더러를 켭니다.
            playerRenderer.enabled = true;
        }
    }
}




