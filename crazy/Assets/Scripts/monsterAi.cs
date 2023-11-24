using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class monsterAi : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 2.0f;

    List<Vector2> targetPoint = new List<Vector2>();

    Vector2 upPoint;
    Vector2 downPoint;
    Vector2 leftPoint;
    Vector2 rightPoint;
    LayerMask layerMask;
    public Animator anim;

    void Awake()
    {
        AIRay();
        targetPosition = GetRandomPosition();
        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object");
        // 시작 시 초기 위치 설정
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AIRay();
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
        SetAnimation();
    }

    Vector2 GetRandomPosition()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        if (upPoint != currentPos)
        {
            targetPoint.Add(upPoint);
        }
        if (downPoint != currentPos)
        {
            targetPoint.Add(downPoint);
        }
        if (leftPoint != currentPos)
        {
            targetPoint.Add(leftPoint);
        }
        if (rightPoint != currentPos)
        {
            targetPoint.Add(rightPoint);
        }
        if(targetPoint.Count > 0)
        {
            int random = Random.Range(0, targetPoint.Count);
            return targetPoint[random];
        }

        return currentPos;
    }
    void AIRay()
    {   
        targetPoint.Clear();
        // 충돌 체크
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, layerMask);
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, layerMask);
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, layerMask);
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, layerMask);

        if (upRayHit.collider == null)
        {
            upPoint = transform.position + Vector3.up;
        }
        else
        {
            upPoint = transform.position;
        }
        if(downRayHit.collider == null)
        {
            downPoint = transform.position + Vector3.down;
        }
        else
        {
            downPoint = transform.position;
        }
        if (leftRayHit.collider == null)
        {
            leftPoint = transform.position + Vector3.left;
        }
        else
        {
            leftPoint = transform.position;
        }
        if (rightRayHit.collider == null)
        {
            rightPoint = transform.position + Vector3.right;
        }
        else
        {
            rightPoint = transform.position;
        }

    }
    void SetAnimation()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        if (targetPosition.y > currentPos.y)
        {
            anim.SetBool("is_up", true);
            anim.SetBool("is_down", false);
            anim.SetBool("E_right", false);
            anim.SetBool("E_left", false);
        }
        else if (targetPosition.y < currentPos.y)
        {
            anim.SetBool("is_up", false);
            anim.SetBool("is_down", true);
            anim.SetBool("E_right", false);
            anim.SetBool("E_left", false);
        }
        else if (targetPosition.x < currentPos.x)
        {
            anim.SetBool("is_up", false);
            anim.SetBool("is_down", false);
            anim.SetBool("E_right", false);
            anim.SetBool("E_left", true);
            //FlipSprite(true); // 왼쪽으로 이동할 때 스프라이트를 뒤집음
        }
        else if (targetPosition.x > currentPos.x)
        {
            anim.SetBool("is_up", false);
            anim.SetBool("is_down", false);
            anim.SetBool("E_right", true);
            anim.SetBool("E_left", false);
            //FlipSprite(false); // 오른쪽으로 이동할 때 스프라이트를 원래대로
        }
    }
    void FlipSprite(bool flipX)
    {
        // 스프라이트를 뒤집는 코드
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = flipX;
    }

}
