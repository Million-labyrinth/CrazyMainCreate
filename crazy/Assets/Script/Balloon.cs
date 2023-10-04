
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collider;
    GameObject scanObject;
    Rigidbody2D rigid;

    public GameObject player;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public GameObject mainBalloon; // 물풍선 본체

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        Invoke("Boom", 3f);
        Invoke("Finish", 5f);
        Debug.Log("테스트");
    }

    void Update()
    {
        Ray();
    }

    void Boom()
    {
        anim.SetBool("Boom", true);

        up.SetActive(true);
        down.SetActive(true);
        left.SetActive(true);
        right.SetActive(true);
    }

    void Finish()
    {
        anim.SetBool("Boom", false);

        mainBalloon.SetActive(false);
        up.SetActive(false);
        down.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);

        collider.isTrigger = true;
    }

    void Ray()
    {
        // 중심축에서 작은 원을 만듦 {중심, 반지름, 반지름 방향, 특정 Layer 설정}
        int layerMask = LayerMask.GetMask("Player");
        RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.right, layerMask);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        // 다른 물풍선의 물줄기에 맞으면 바로 터지게 만듦
        if (obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater")
        {
            Boom();
            Invoke("Finish", 2f);
        }
        string iname = obj.gameObject.name;
        Debug.Log("물풍선이 오브젝트에 닿음");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // 플레이어가 물풍선과 같이 있을 시, 트리거 활성화
        if (collision.gameObject.tag == "Player")
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 물풍선 밖으로 나갈 시, 트리거 비활성화
        if (collision.gameObject.tag == "Player")
        {
            collider.isTrigger = false;
        }
    }

}
