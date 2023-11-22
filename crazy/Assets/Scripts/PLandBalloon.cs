using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PLandBalloon : MonoBehaviour
{
    public float lerpTime = 1.0f;
    public Rigidbody2D rigid;

    Vector3 balloonPos;
    public Vector3 firstPos;
    public Vector3 secondPos;
    public float firstMoveSpeed;
    public float SecondMoveSpeed;

    public GameObject balloon;
    public Balloon balloonLogic;

    void Awake()
    {
        balloonLogic = balloon.GetComponent<Balloon>();

        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(DoTween());
    }


    IEnumerator DoTween()
    {
        // InQuad : 시작할 때 빠르게 가속, 끝날 때 감속
        // OutQuad : 시작할 때 감속, 끝날 때 가속
        transform.DOMoveX(firstPos.x, firstMoveSpeed).SetEase(Ease.InQuad);
        transform.DOMoveY(firstPos.y, firstMoveSpeed).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(0.2f);

        transform.DOMoveX(secondPos.x, SecondMoveSpeed).SetEase(Ease.OutQuad);
        transform.DOMoveY(secondPos.y, SecondMoveSpeed).SetEase(Ease.InQuad);

        // 물풍선 이동 시 터지는 시간 초기화
        balloonLogic.enableTime = 0;
    }
}
