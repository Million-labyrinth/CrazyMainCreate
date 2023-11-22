using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grass : MonoBehaviour
{
    Animator anim;

    public bool haveObj = false; // 안에 오브젝트가 있으면 Grass 는 파괴가 안됨.

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator Movement()
    {
        transform.position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.2f);
        transform.position -= new Vector3(0, 0.1f, 0);

        StopCoroutine(Movement());
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "upWater" || other.gameObject.tag == "downWater" || other.gameObject.tag == "leftWater" || other.gameObject.tag == "rightWater" || other.gameObject.tag == "hitCollider")
        {
            if (!haveObj)
            {
                gameObject.SetActive(false);
                Debug.Log("false");
            }

        }
        else if (other.tag == "Block" || other.tag == "PlayerA" || other.tag == "PlayerB")
        {
            haveObj = true;

            // Grass 안에 있는 오브젝트의 스프라이트 렌더러를 끕니다.
            SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
            otherSprite.enabled = false;

            if(other.tag != "Block")
            {
                StartCoroutine(Movement());
            }
        } else if(other.tag == "Balloon")
        {
            SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
            otherSprite.enabled = false;
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Balloon")
        {
            SpriteRenderer otherSprite = collision.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
            otherSprite.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Balloon" || other.tag == "Block" || other.tag == "PlayerA" || other.tag == "PlayerB")
        {
            SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
            otherSprite.enabled = true;
        }

        haveObj = false;
    }
}




