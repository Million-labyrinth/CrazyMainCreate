using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class monsterAi : MonoBehaviour
{
    private float xMin = -8.0f;
    private float xMax = 8.0f;
    private float yMin = -7.0f;
    private float yMax = 7.0f;
    private Vector3 targetPosition;
    private float moveSpeed = 2.0f;

    List<Vector2> hitPoints = new List<Vector2>();

    Vector2 uphitPoint;
    Vector2 downhitPoint;
    Vector2 lefthitPoint;
    Vector2 righthitPoint;
    LayerMask layerMask;
    public Animator anim;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object");
        // ���� �� �ʱ� ��ġ ����
        anim = GetComponent<Animator>();
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        
        //�̵� �ִϸ��̼�
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
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // ���� ��ǥ ��ġ�� �����ߴٸ�, �ٸ� ���� ��ġ�� ����
        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
        AIRay();
    }

    Vector2 GetRandomPosition()
    {
        Vector2 farthestPoint = Vector2.zero;
        Vector2 secondFarthestPoint = Vector2.zero;

        hitPoints.Sort((a, b) => Vector2.Distance(transform.position, b).CompareTo(Vector2.Distance(transform.position, a)));

        // ���� �� ���� �� ��°�� �� ���� ����
        if (hitPoints.Count >= 1)
        {
            farthestPoint = hitPoints[0];
        }
        if (hitPoints.Count >= 2)
        {
            secondFarthestPoint = hitPoints[1];
        }

        Vector2 selectedPoint;

        if (Random.Range(0, 2) == 0)
        {
            selectedPoint = farthestPoint;
        }
        else
        {
            selectedPoint = secondFarthestPoint;
        }

        return selectedPoint;
    }
    void AIRay()
    {
        hitPoints.Clear();
        // �浹 üũ
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), Vector3.up * 13f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), Vector3.down * 13f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), Vector3.left * 15f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), Vector3.right * 15f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.up, 13f, layerMask);
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.down, 13f, layerMask);
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.left, 15f, layerMask);
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.right, 15f, layerMask);

        if (upRayHit.collider != null)
        {
            uphitPoint = upRayHit.point;
            uphitPoint.y -= 1.0f;
            hitPoints.Add(uphitPoint);
        }
        else if(downRayHit.collider != null)
        {
            downhitPoint = downRayHit.point;
            downhitPoint.y += 1.0f;
            hitPoints.Add(downhitPoint);
        }
        else if(leftRayHit.collider != null)
        {
            lefthitPoint = leftRayHit.point;
            lefthitPoint.x += 1.0f;
            hitPoints.Add(lefthitPoint);
        }
        else if(rightRayHit.collider != null)
        {
            righthitPoint = rightRayHit.point;
            righthitPoint.y -= 1.0f;
            hitPoints.Add(righthitPoint);
        }


    }

}
