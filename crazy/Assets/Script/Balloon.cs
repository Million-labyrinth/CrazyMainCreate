using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collider;

    public GameObject upWater;
    public GameObject downWater;
    public GameObject leftWater;
    public GameObject rightWater;

    public GameObject mainBalloon; // 물풍선 본체

    bool waterLineActive = true; // 물줄기 위에 물풍선 설치 시, 안 터지게 만들기 위한 변수

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnEnable() {
        Invoke("WaterLineActive", 0.1f);
        Invoke("Boom", 2.5f);
        Invoke("Finish", 3f);
    }

    void Boom() {
        // 애니메이션
        anim.SetBool("Boom", true);

        // 물줄기 활성화
        upWater.SetActive(true);
        downWater.SetActive(true);
        leftWater.SetActive(true);
        rightWater.SetActive(true);
    }

    void Finish() {
        // 애니메이션
        anim.SetBool("Boom", false);

        collider.isTrigger = true;
        mainBalloon.SetActive(false);

        // 물줄기 비활성화
        upWater.SetActive(false);
        downWater.SetActive(false);
        leftWater.SetActive(false);
        rightWater.SetActive(false);

        waterLineActive = true;
    }
    void WaterLineActive()
    {
        waterLineActive = false;
    }

    void OnTriggerEnter2D(Collider2D obj) {
        // 다른 물풍선의 물줄기에 맞으면 바로 터지게 만듦
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {

            // 물줄기 위에서는 작동을 안하게 만듦
            if (!waterLineActive)
            {
                Boom();
                Invoke("Finish", 0.5f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        // 플레이어가 물풍선 설치 후 위에 있을 시, 트리거 활성화
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
