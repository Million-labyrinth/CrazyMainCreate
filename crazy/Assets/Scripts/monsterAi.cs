using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

    public bool isDie;
    
    public GameManager gameManager;
    public PVEManager pveManager;

    void Awake()
    {
        //AIRay();
        //targetPosition = GetRandomPosition();
        layerMask = LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Balloon") | LayerMask.GetMask("Water");
        // ���� �� �ʱ� ��ġ ����
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

        gameManager = FindAnyObjectByType<GameManager>();
        pveManager = FindObjectOfType<PVEManager>();

        isDie = false;
    }

    void Start()
    {
        AIUpRay();
        AIDownRay();
        AILeftRay();
        AIRightRay();
    }

    void Update()
    {
        //AIRay();

        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //if (transform.position == targetPosition)
        //{
        //    targetPosition = GetRandomPosition();
        //}
        if(gameManager.startedGame && !gameManager.isFinishGame && !isDie)
        {
            AIUpRay();
            AIDownRay();
            AILeftRay();
            AIRightRay();
            anim.SetBool("start", true);

            transform.position += enemyDir * 2f * Time.deltaTime;

            if (scanedUp && scanedDown && scanedLeft && scanedRight)
            {
                anim.SetBool("allBlocked", true);
                enemyDir = Vector3.zero;
            }
            else
            {
                anim.SetBool("allBlocked", false);
            }
        } else if(gameManager.isFinishGame)
        {
            anim.SetBool("allBlocked", true);
        }

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
    //    // �浹 üũ
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

            // List �� Up �� ������ ����
            if (canDir.Contains("Up"))
            {
                canDir.Remove("Up");
            }

            // ���� ���µ� ���� ���� ������ �� addDownDir ���� true ���� ������ȯ�� �ȵǴ� ���� ����
            if (enemyDir == new Vector3(0, 1) && addDownDir)
            {
                addDownDir = false;
            }
        }
        else
        {
            // �տ� �ƹ��͵� ���� ��, List �� Up ���� �߰�
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

    // �� �� �ִ� ������ List�� �����ؼ� ���� ���ڸ� ������ ���� ����
    IEnumerator AIMove()
    {
        int max = canDir.Count;
        int ran = Random.Range(0, max); ;

        switch (canDir[ran])
        {
            case "Up":
                enemyDir = new Vector3(0, 1);
                //�� ���⿡ ���� �ִϸ��̼� ����
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

    // Ray ���� ���̰� �ϴ� �뵵
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
    //    // ��������Ʈ�� ������ �ڵ�
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.flipX = flipX;
    //}

    IEnumerator Die()
    {
        yield return null;
        isDie = true;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
        pveManager.enemyCount--;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater" || collision.gameObject.tag == "hitCollider")
        {
            // ��� �ִϸ��̼� �߰� �ʿ�
            StartCoroutine("Die");

        }
    }

}
