using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpringItem : MonoBehaviour
{
    public float lerpTime = 1.0f;
    LayerMask layerMask;
    public Rigidbody2D rigid;

    Vector2 playerPos;
    Vector2 nextPos;
    float distanceUp;
    float distanceDown;
    float distanceLeft;
    float distanceRight;

    public GameObject upScanObject;
    public GameObject downScanObject;
    public GameObject leftScanObject;
    public GameObject rightScanObject;

    RaycastHit2D upRayHit;
    RaycastHit2D downRayHit;
    RaycastHit2D leftRayHit;
    RaycastHit2D rightRayHit;

    float upRayDistance;
    float downRayDistance;
    float leftRayDistance;
    float rightRayDistance;

    void Awake()
    { 
        rigid = GetComponent<Rigidbody2D>();

        layerMask = LayerMask.GetMask("Grass") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Default");
    }

    void OnEnable()
    {
        upRayDistance = 15f;
        downRayDistance = 15f;
        leftRayDistance = 15f;
        rightRayDistance = 15f;
    }

    void Update()
    {
        PlayerSpringRay();
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

    public void MovePlayer(string direction)
    {
        playerPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));

        switch (direction)
        {
            case "Up":
                nextPos = playerPos + new Vector2(0, distanceUp - 1);
                break;
            case "Down":
                nextPos = playerPos - new Vector2(0, distanceDown - 1);
                break;
            case "Left":
                nextPos = playerPos - new Vector2(distanceLeft - 1, 0);
                break;
            case "Right":
                nextPos = playerPos + new Vector2(distanceRight - 1, 0);
                break;
        }

        StartCoroutine(lerpCoroutine(playerPos, nextPos, lerpTime * 0.2f));
    }

    void PlayerRay()
    {
        // Ray
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.7f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        if (upRayHit.collider != null)
        {
            upScanObject = upRayHit.collider.gameObject;
        }
        else
        {
            upScanObject = null;
        }
        if (downRayHit.collider != null)
        {
            downScanObject = downRayHit.collider.gameObject;
        }
        else
        {
            downScanObject = null;
        }
        if (leftRayHit.collider != null)
        {
            leftScanObject = leftRayHit.collider.gameObject;
        }
        else
        {
            leftScanObject = null;
        }
        if (rightRayHit.collider != null)
        {
            rightScanObject = rightRayHit.collider.gameObject;
        }
        else
        {
            rightScanObject = null;
        }
    }

    void PlayerSpringRay()
    {
        // Ray 실행 조건
        // Player 바로 앞에 오브젝트가 없으면 Ray를 안쏨 (오류 방지)
        if (upScanObject != null)
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * upRayDistance, new Color(1, 1, 0));
            upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, upRayDistance, layerMask);
        }
        if (downScanObject != null)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * downRayDistance, new Color(1, 1, 0));
            downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, downRayDistance, layerMask);
        }
        if (leftScanObject != null)
        {
            Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * leftRayDistance, new Color(1, 1, 0));
            leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, leftRayDistance, layerMask);

        }
        if (rightScanObject != null)
        {
            Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * rightRayDistance, new Color(1, 1, 0));
            rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, rightRayDistance, layerMask);
        }



        // 인식 오브젝트 거리 구하기
        if (upRayHit.collider != null)
        {
            upScanObject = upRayHit.collider.gameObject;
            Vector3 upObjPos = upScanObject.transform.position;
            Vector3 balloonPos = transform.position;

            if (upObjPos.y >= 0 && balloonPos.y >= 0)
            {
                distanceUp = upObjPos.y - balloonPos.y;
            }
            else if (upObjPos.y < 0 && balloonPos.y < 0)
            {
                distanceUp = Mathf.Abs(balloonPos.y) - Mathf.Abs(upObjPos.y);
            }
            else if (upObjPos.y >= 0 && balloonPos.y < 0)
            {
                distanceUp = Mathf.Abs(balloonPos.y) + Mathf.Abs(upObjPos.y);
            }

            upRayDistance = distanceUp;
        }
        else
        {
            upScanObject = null;
            upRayDistance = 15f;
            distanceUp = 15;
        }

        if (downRayHit.collider != null)
        {
            downScanObject = downRayHit.collider.gameObject;
            Vector3 downObjPos = downScanObject.transform.position;
            Vector3 balloonPos = transform.position;

            if (downObjPos.y >= 0 && balloonPos.y >= 0)
            {
                distanceDown = balloonPos.y - downObjPos.y;
            }
            else if (downObjPos.y < 0 && balloonPos.y < 0)
            {
                distanceDown = Mathf.Abs(downObjPos.y) - Mathf.Abs(balloonPos.y);
            }
            else if (downObjPos.y < 0 && balloonPos.y >= 0)
            {
                distanceDown = Mathf.Abs(balloonPos.y) + Mathf.Abs(downObjPos.y);
            }

            downRayDistance = distanceDown;
        }
        else
        {
            downScanObject = null;
            downRayDistance = 15f;
            distanceDown = 15;
        }

        if (leftRayHit.collider != null)
        {
            leftScanObject = leftRayHit.collider.gameObject;
            Vector3 leftObjPos = leftScanObject.transform.position;
            Vector3 balloonPos = transform.position;

            if (leftObjPos.x >= 0 && balloonPos.x >= 0)
            {
                distanceLeft = balloonPos.x - leftObjPos.x;
            }
            else if (leftObjPos.x < 0 && balloonPos.x < 0)
            {
                distanceLeft = Mathf.Abs(leftObjPos.x) - Mathf.Abs(balloonPos.x);
            }
            else if (leftObjPos.x < 0 && balloonPos.x >= 0)
            {
                distanceLeft = Mathf.Abs(balloonPos.x) + Mathf.Abs(leftObjPos.x);
            }

            leftRayDistance = distanceLeft;
        }
        else
        {
            leftScanObject = null;
            leftRayDistance = 15f;
            distanceLeft = 15f;
        }

        if (rightRayHit.collider != null)
        {
            rightScanObject = rightRayHit.collider.gameObject;
            Vector3 rightObjPos = rightScanObject.transform.position;
            Vector3 balloonPos = transform.position;

            if (rightObjPos.x >= 0 && balloonPos.x >= 0)
            {
                distanceRight = rightObjPos.x - balloonPos.x;
            }
            else if (rightObjPos.x < 0 && balloonPos.x < 0)
            {
                distanceRight = Mathf.Abs(balloonPos.x) - Mathf.Abs(rightObjPos.x);
            }
            else if (rightObjPos.x >= 0 && balloonPos.x < 0)
            {
                distanceRight = Mathf.Abs(balloonPos.x) + Mathf.Abs(rightObjPos.x);
            }

            rightRayDistance = distanceRight;
        }
        else
        {
            rightScanObject = null;
            rightRayDistance = 15f;
            distanceRight = 15;
        }
    }
}
