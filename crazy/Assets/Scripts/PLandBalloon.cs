using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PLandBalloon : MonoBehaviour
{
    public float lerpTime = 1.0f;
    public Rigidbody2D rigid;

    Vector3 balloonPos;
    public Vector3 nextPos;

    public GameObject balloon;
    public Balloon balloonLogic;

    void Awake()
    {
        balloonLogic = balloon.GetComponent<Balloon>();

        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveBalloon();
    }


    IEnumerator lerpCoroutine(Vector3 current, Vector3 target, float time)
    {
        yield return new WaitForSeconds(0.5f);
        float elapsedTime = 0.0f;

        this.transform.position = current;
        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            this.transform.position
                = Vector3.Lerp(current, target, elapsedTime / time);

            yield return null;
        }

        transform.position = target;

        yield return null;
    }

    public void MoveBalloon()
    {
        balloonPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));

        // 물풍선 이동 시 터지는 시간 초기화
        balloonLogic.enableTime = 0;

        StartCoroutine(lerpCoroutine(balloonPos, nextPos, lerpTime * 0.3f));
    }

}
