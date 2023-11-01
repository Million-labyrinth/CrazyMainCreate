using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    public float lerpTime = 1.0f;

    Vector2 boxPos;
    Vector2 nextPos;

    public GameObject upScanObject; // upRay 에 인식되는 오브젝트 변수
    public GameObject downScanObject; // downRay 에 인식되는 오브젝트 변수
    public GameObject leftScanObject; // leftRay 에 인식되는 오브젝트 변수
    public GameObject rightScanObject; // rightRay 에 인식되는 오브젝트 변수

    public SpriteRenderer BoxRenderer;

    void Awake()
    {
        BoxRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        BoxRay();
        colliderRay();
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

    public void MoveBox(string direction)
    {
        boxPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));

        switch(direction) {
            case "Up":
                nextPos = boxPos + new Vector2 (0, 1); 
                break;
            case "Down":
                nextPos = boxPos + new Vector2(0, -1);
                break;
            case "Left":
                nextPos = boxPos + new Vector2(-1, 0);
                break;
            case "Right":
                nextPos = boxPos + new Vector2(1, 0);
                break;
        }

        StartCoroutine(
            lerpCoroutine(boxPos, nextPos, lerpTime * 0.2f));
    }

    void BoxRay()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B"));

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
    void colliderRay()
    {
        // Ray
        Debug.DrawRay(transform.position - new Vector3(0.35f, 0.65f, 0), Vector3.right * 0.7f, new Color(1, 1, 1));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position - new Vector3(0.35f, 0.65f, 0), Vector3.right, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        if (downRayHit.collider != null)
        {
            GameObject downObj = downRayHit.collider.gameObject;
            SpriteRenderer orderInLayer = downObj.GetComponent<SpriteRenderer>();
            int objOrder = orderInLayer.sortingOrder;
            BoxRenderer.sortingOrder = orderInLayer.sortingOrder - 1;
            // orderInLayer.sortingOrder += playerRenderer.sortingOrder;
            objOrder = orderInLayer.sortingOrder;
            Debug.Log(downObj.name);
        }
        else if (downRayHit.collider == null)
        {
            BoxRenderer.sortingOrder = 13;
        }
    }
}
