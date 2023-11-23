using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class monsterAi : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 2.0f;

    List<Vector2> hitPoints = new List<Vector2>();

    Vector2 upPoint;
    Vector2 downPoint;
    Vector2 leftPoint;
    Vector2 rightPoint;
    LayerMask layerMask;
    public Animator anim;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object");
        // 시작 시 초기 위치 설정
        anim = GetComponent<Animator>();
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        
        //이동 애니메이션
        //if (transform.position.y > GetRandomPosition().y)
        //{
        //    anim.SetBool("is_up", true);
        //    anim.SetBool("is_down", false);
        //}
        //else if (transform.position.y < GetRandomPosition().y)
        //{
        //    anim.SetBool("is_down", true);
        //    anim.SetBool("is_up", false);
        //}
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // 만약 목표 위치에 도달했다면, 다른 랜덤 위치를 선택
        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
        AIRay();
    }

    Vector2 GetRandomPosition()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        if(upPoint != currentPos)
        {
            int ranPos = Random.Range(0, 4);
            if(ranPos == 0)
            {
                
            }
            else if(ranPos == 1)
            {
                 
            }
            else if (ranPos == 2)
            {

            }
            else if (ranPos == 3)
            {

            }
            

        }
        
        

    }
    void AIRay()
    {   
        hitPoints.Clear();
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

}
