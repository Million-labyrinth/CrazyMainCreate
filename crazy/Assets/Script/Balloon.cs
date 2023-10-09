
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collider;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public GameObject mainBalloon; // 물풍선 본체

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnEnable() {
        Invoke("Boom", 3f);
        Invoke("Finish", 5f);
    }

    void Boom() {
        anim.SetBool("Boom", true);
        
        up.SetActive(true);
        down.SetActive(true);
        left.SetActive(true);
        right.SetActive(true);
    }

    void Finish() {
        anim.SetBool("Boom", false);

        mainBalloon.SetActive(false);
        up.SetActive(false);
        down.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);

        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D obj) {
        // 다른 물풍선의 물줄기에 맞으면 바로 터지게 만듦
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {
            Boom();
            Invoke("Finish", 2f);
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        // 플레이어가 물풍선과 같이 있을 시, 트리거 활성화
        if(collision.gameObject.tag == "Player") {
            collider.isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        // 플레이어가 물풍선 밖으로 나갈 시, 트리거 비활성화
        if(collision.gameObject.tag == "Player") {
            collider.isTrigger = false;
        }
    }
}
