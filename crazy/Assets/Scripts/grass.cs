using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grass : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        // Grass 안에 있는 오브젝트의 스프라이트 렌더러를 끕니다.
        SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
        otherSprite.enabled = false;

        if (other.tag == "PlayerA" || other.tag == "PlayerB")
        {
            
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Grass 안에 있는 오브젝트의 스프라이트 렌더러를 끕니다.
        SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
        otherSprite.enabled = true;

    }
}




