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

    //Vector2 upPoint;
    //Vector2 downPoint;
    //Vector2 leftPoint;
    //Vector2 rightPoint;
    LayerMask layerMask;
    public Animator anim;

    bool scanedUp;
    bool scanedDown;
    bool scanedLeft;
    bool scanedRight;
    public List<string> canDir;
    bool addUpDir;
    bool addDownDir;
    bool addLeftDir;
    bool addRightDir;
    Vector3 enemyDir;

    void Awake()
    {
        //AIRay();
        //targetPosition = GetRandomPosition();
        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Balloon") | LayerMask.GetMask("Water");
        // 시작 시 초기 위치 설정
        anim = GetComponent<Animator>();

        scanedUp = false;
        scanedDown = false;
        scanedLeft = false;
        scanedRight = false;
        canDir = new List<string>();
        addUpDir = false;
        addDownDir = false;
        addLeftDir = false;
        addRightDir = false;
        StartCoroutine(AIDirection("Up"));
        StartCoroutine(AIDirection("Down"));
        StartCoroutine(AIDirection("Left"));
        StartCoroutine(AIDirection("Right"));
    }

    void Update()
    {
        //AIRay();

        // 현재 위치에서 목표 위치로 이동
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //if (transform.position == targetPosition)
        //{
        //    targetPosition = GetRandomPosition();
        //}
        AIUpRay();
        AIDownRay();
        AILeftRay();
        AIRightRay();

        transform.position += enemyDir * 2f * Time.deltaTime;
    }

    //Vector2 GetRandomPosition()
    //{
    //    Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
    //    if (upPoint != currentPos)
    //    {
    //        targetPoint.Add(upPoint);
    //    }
    //    if (downPoint != currentPos)
    //    {
    //        targetPoint.Add(downPoint);
    //    }
    //    if (leftPoint != currentPos)
    //    {
    //        targetPoint.Add(leftPoint);
    //    }
    //    if (rightPoint != currentPos)
    //    {
    //        targetPoint.Add(rightPoint);
    //    }
    //    if (targetPoint.Count > 0)
    //    {
    //        int random = Random.Range(0, targetPoint.Count);
    //        return targetPoint[random];
    //    }

    //    return currentPos;
    //}
    //void AIRay()
    //{
    //    targetPoint.Clear();
    //    // 충돌 체크
    //    Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
    //    Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
    //    Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
    //    Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
    //    RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, layerMask);
    //    RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, layerMask);
    //    RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, layerMask);
    //    RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, layerMask);


    //    if (upRayHit.collider == null)
    //    {
    //        upPoint = transform.position + Vector3.up;
    //    }
    //    else
    //    {
    //        upPoint = transform.position;
    //    }
    //    if (downRayHit.collider == null)
    //    {
    //        downPoint = transform.position + Vector3.down;
    //    }
    //    else
    //    {
    //        downPoint = transform.position;
    //    }
    //    if (leftRayHit.collider == null)
    //    {
    //        leftPoint = transform.position + Vector3.left;
    //    }
    //    else
    //    {
    //        leftPoint = transform.position;
    //    }
    //    if (rightRayHit.collider == null)
    //    {
    //        rightPoint = transform.position + Vector3.right;
    //    }
    //    else
    //    {
    //        rightPoint = transform.position;
    //    }

    //}
    void AIUpRay()
    {
        Collider2D upRayHit = Physics2D.OverlapBox(transform.position + new Vector3(0, 0.5f), new Vector2(1f, 0.25f), 0, layerMask);

        if (upRayHit != null)
        {
            scanedUp = true;
            addUpDir = false;

            // List 에 Up 이 있으면 삭제
            if (canDir.Contains("Up"))
            {
                canDir.Remove("Up");
            }

            // 위로 가는데 벽에 가로 막혔을 때 addDownDir 값이 true 여서 방향전환이 안되는 오류 방지
            if (enemyDir == new Vector3(0, 1) && addDownDir)
            {
                addDownDir = false;
            }
        }
        else
        {
            // 앞에 아무것도 없을 시, List 에 Up 방향 추가
            scanedUp = false;
            if (!addUpDir && scanedDown)
            {
                StartCoroutine(AIDirection("Up"));
            }
        }
    }

    void AIDownRay()
    {
        Collider2D downRayHit = Physics2D.OverlapBox(transform.position + new Vector3(0, -0.5f), new Vector2(1f, 0.25f), 0, layerMask);

        if (downRayHit != null)
        {
            scanedDown = true;
            addDownDir = false;

            if (canDir.Contains("Down"))
            {
                canDir.Remove("Down");
            }

            if (enemyDir == new Vector3(0, -1) && addUpDir)
            {
                addUpDir = false;
            }
        }
        else
        {
            scanedDown = false;
            if (!addDownDir && scanedUp)
            {
                StartCoroutine(AIDirection("Down"));
            }
        }
    }
    void AILeftRay()
    {
        Collider2D leftRayHit = Physics2D.OverlapBox(transform.position + new Vector3(-0.5f, 0), new Vector2(0.25f, 1f), 0, layerMask);

        if (leftRayHit != null)
        {
            scanedLeft = true;
            addLeftDir = false;

            if (canDir.Contains("Left"))
            {
                canDir.Remove("Left");
            }

            if (enemyDir == new Vector3(-1, 0) && addRightDir)
            {
                addRightDir = false;
            }
        }
        else
        {
            scanedLeft = false;
            if (!addLeftDir && scanedRight)
            {
                StartCoroutine(AIDirection("Left"));
            }
        }
    }
    void AIRightRay()
    {
        Collider2D rightRayHit = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 0), new Vector2(0.25f, 1f), 0, layerMask);

        if (rightRayHit != null)
        {
            scanedRight = true;
            addRightDir = false;

            if (canDir.Contains("Right"))
            {
                canDir.Remove("Right");
            }

            if (enemyDir == new Vector3(1, 0) && addLeftDir)
            {
                addLeftDir = false;
            }
        }
        else
        {
            scanedRight = false;
            if (!addRightDir && scanedLeft)
            {
                StartCoroutine(AIDirection("Right"));
            }
        }
    }

    IEnumerator AIDirection(string dir)
    {
        switch (dir)
        {
            case "Up":
                if (!canDir.Contains("Up"))
                {
                    canDir.Add("Up");
                }
                addUpDir = true;
                break;
            case "Down":
                if (!canDir.Contains("Down"))
                {
                    canDir.Add("Down");
                }
                addDownDir = true;
                break;
            case "Left":
                if (!canDir.Contains("Left"))
                {
                    canDir.Add("Left");
                }
                addLeftDir = true;
                break;
            case "Right":
                if (!canDir.Contains("Right"))
                {
                    canDir.Add("Right");
                }
                addRightDir = true;
                break;
        }

        yield return null;
        StartCoroutine("AIMove");
    }

    // 갈 수 있는 방향을 List에 저장해서 랜덤 숫자를 돌려서 방향 선택
    IEnumerator AIMove()
    {
        int max = canDir.Count;
        int ran = Random.Range(0, max); ;

        switch (canDir[ran])
        {
            case "Up":
                enemyDir = new Vector3(0, 1);
                //각 방향에 따른 애니메이션 실행
                anim.SetBool("is_up", true);
                anim.SetBool("is_down", false);
                anim.SetBool("is_right", false);
                anim.SetBool("is_left", false);
                break;
            case "Down":
                enemyDir = new Vector3(0, -1);
                anim.SetBool("is_up", false);
                anim.SetBool("is_down", true);
                anim.SetBool("is_right", false);
                anim.SetBool("is_left", false);
                break;
            case "Left":
                enemyDir = new Vector3(-1, 0);
                anim.SetBool("is_up", false);
                anim.SetBool("is_down", false);
                anim.SetBool("is_right", false);
                anim.SetBool("is_left", true);
                break;
            case "Right":
                enemyDir = new Vector3(1, 0);
                anim.SetBool("is_up", false);
                anim.SetBool("is_down", false);
                anim.SetBool("is_right", true);
                anim.SetBool("is_left", false);
                break;
        }

        yield return new WaitForSeconds(0.5f);
    }

    // Ray 눈에 보이게 하는 용도
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5f), new Vector2(1f, 0.25f));
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -0.5f), new Vector2(1f, 0.25f));
        Gizmos.DrawWireCube(transform.position + new Vector3(-0.5f, 0), new Vector2(0.25f, 1f));
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0), new Vector2(0.25f, 1f));

    }
    //void FlipSprite(bool flipX)
    //{
    //    // 스프라이트를 뒤집는 코드
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.flipX = flipX;
    //}

}
