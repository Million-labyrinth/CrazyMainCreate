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

        firstMoveSpeed = 0.3f;
        SecondMoveSpeed = 0.3f;
    }

    void OnEnable()
    {
        StartCoroutine(DoTween());
    }


    IEnumerator DoTween()
    {
        // InQuad : ������ �� ������ ����, ���� �� ����
        // OutQuad : ������ �� ����, ���� �� ����
        transform.DOMoveX(firstPos.x, firstMoveSpeed).SetEase(Ease.InQuad);
        transform.DOMoveY(firstPos.y, firstMoveSpeed).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(0.2f);

        transform.DOMoveX(secondPos.x, SecondMoveSpeed).SetEase(Ease.OutQuad);
        transform.DOMoveY(secondPos.y, SecondMoveSpeed).SetEase(Ease.InQuad);

        // ��ǳ�� �̵� �� ������ �ð� �ʱ�ȭ
        balloonLogic.enableTime = 0;

        yield return new WaitForSeconds(0.4f);
        // ��ǳ�� �̵� �� ������ �ð� �ʱ�ȭ
        balloonLogic.enableTime = 0;
    }
}
