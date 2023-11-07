using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushBalloon : MonoBehaviour
{
    public float lerpTime = 1.0f;
    LayerMask layerMask;

    Vector2 balloonPos;
    Vector2 nextPos;

    public GameObject balloon;
    Balloon balloonLogic;

    void Awake()
    {
        balloonLogic = balloon.GetComponent<Balloon>();

        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Water") | LayerMask.GetMask("Balloon");
    }

    void Update()
    {
        balloonPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));
    }

    IEnumerator lerpCoroutine(Vector3 current, Vector3 target, float time)
    {
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

    public void MoveBalloon(string direction)
    {
        switch (direction)
        {
            case "Up":
                //while(balloonLogic.upScanObject == null)
                //{
                //    nextPos = balloonPos + new Vector2(0, 1);
                //}
                break;
            case "Down":
                nextPos = balloonPos + new Vector2(0, -1);
                break;
            case "Left":
                nextPos = balloonPos + new Vector2(-1, 0);
                break;
            case "Right":
                nextPos = balloonPos + new Vector2(1, 0);
                break;
        }

        StartCoroutine(
            lerpCoroutine(balloonPos, nextPos, lerpTime * 0.2f));
    }
}
