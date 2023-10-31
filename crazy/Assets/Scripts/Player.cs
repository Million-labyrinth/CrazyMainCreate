
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
//김인섭 왔다감2
public class Player : MonoBehaviour
{
    public int bombPower;
    public int bombPowerMax = 10;
    public int bombRange;
    public int bombRangeMax = 7;
    public float playerSpeed;
    public float playerSpeedMax = 7f;
    public float playerHealth;
    public float playerMaxHealth = 2f;
    public string basicBubble;
    public bool useShield = false;
    public bool useniddle = false;

    int playerAballonIndex = 0; // 물풍선 오브젝트 풀 사용할 때 필요한 playerAballonIndex 변수
    public int playerAcountIndex = 0; // 물풍선을 생성할 때, 플레이어가 생성한 물풍선의 개수를 체크할 때 필요한 변수
    public bool playerAmakeBalloon; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수

    public ObjectManager objectManager;
    GameObject[] WaterBalloon; // 오브젝트 풀을 가져오기 위한 변수

    CircleCollider2D collider;

    public Item item;
    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한


    bool hStay; // Horizontal 키 다운
    bool vStay; // Vertical 키 다운
    public GameObject pushBlock; // 인식된 밀 수 있는 블럭 저장용 변수
    public float nextPushTime; // 블럭 밀기 최대(설정) 쿨타임
    public float curPushTime; // 블럭 밀기 현재(충전) 쿨타임

    Rigidbody2D rigid;
    Animator anim;


    public GameObject Shieldeffect;

    Vector2 moveVec; // 플레이어가 움직이는 방향
    Vector2 rayDir; // Ray 방향
    public SpriteRenderer playerRenderer; //스프라이트 활성화 비활성화

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();

        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;

        playerAmakeBalloon = true;
    }

    void Update()
    {
        Move();
        Skill();
        Ray();
        UseItem();
        colliderRay();
    }
    void LateUpdate()
    {
        // 대각선 이동 제한
        moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
        rigid.velocity = moveVec * playerSpeed;
    }

    void Move()
    {
        // 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        hStay = Input.GetButton("Horizontal");
        vStay = Input.GetButton("Vertical");

        if (vDown)
        {
            isHorizonMove = false;
            anim.SetFloat("vAxisRaw", vAxis);
        }
        else if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vUp || hUp)
        {
            isHorizonMove = hAxis != 0;

            if (vUp)
            {
                anim.SetFloat("vAxisRaw", vAxis);
            }
        }
        if (anim.GetInteger("hAxisRaw") != hAxis)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)hAxis);
        }
        else if (anim.GetInteger("vAxisRaw") != vAxis)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)vAxis);
        }
        else
            anim.SetBool("isChange", false);


        // 상자 밀기용 Ray 방향 설정
        if (moveVec.y > 0)
        {
            rayDir = Vector2.up;
        }
        else if (moveVec.y < 0)
        {
            rayDir = Vector2.down;
        }
        else if (moveVec.x > 0)
        {
            rayDir = Vector2.right;
        }
        else if (moveVec.x < 0)
        {
            rayDir = Vector2.left;
        }

    }

    void Ray()
    {

        // 물풍선을 겹치게 생성 못하게 만들 때 필요한 Ray
        Collider2D forMake = Physics2D.OverlapCircle(rigid.position - new Vector2(0, 0.1f), 0.45f, LayerMask.GetMask("Balloon A") | LayerMask.GetMask("Balloon B"));

        if (forMake != null)
        {
            playerAmakeBalloon = false;
        }
        else
        {
            playerAmakeBalloon = true;
        }

        // 밀 수 있는 상자 Ray
        // if (hStay || vStay)
        // {
        //     Debug.DrawRay(rigid.position - new Vector2(0, 0.1f), rayDir * 0.7f, new Color(1, 0, 0));
        //     RaycastHit2D pushRay = Physics2D.Raycast(rigid.position - new Vector2(0, 0.1f), rayDir, 0.7f, LayerMask.GetMask("MoveBlock"));

        //     if (pushRay.collider != null)
        //     {
        //         pushBlock = pushRay.collider.gameObject;
        //         Debug.Log(pushBlock.name);

        //         curPushTime += Time.deltaTime;

        //         if (curPushTime > nextPushTime)
        //         {
        //             if (rayDir == Vector2.up)
        //             {
        //                 pushBlock.transform.position += new Vector3(0, 1, 0);

        //             }
        //             else if (rayDir == Vector2.down)
        //             {
        //                 pushBlock.transform.position += new Vector3(0, -1, 0);

        //             }
        //             else if (rayDir == Vector2.left)
        //             {
        //                 pushBlock.transform.position += new Vector3(-1, 0, 0);

        //             }
        //             else if (rayDir == Vector2.right)
        //             {
        //                 pushBlock.transform.position += new Vector3(1, 0, 0);
        //             }

        //             // 시간 초기화
        //             curPushTime = 0;
        //         }
        //     }
        //     else
        //     {
        //         pushBlock = null;
        //     }
        // }
        // else
        // {
        //     // 키 다운 해제 시 시간 초기화
        //     curPushTime = 0;
        // }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f);
        // 물풍선 생성 판정
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.1f), 0.45f);
    }


    void Skill()
    {
        // 물풍선 풀 가져오기
        switch (bombRange)
        {
            case 1:
                MakeBalloon("WaterBalloon1");
                break;
            case 2:
                MakeBalloon("WaterBalloon2");
                break;
            case 3:
                MakeBalloon("WaterBalloon3");
                break;
            case 4:
                MakeBalloon("WaterBalloon4");
                break;
            case 5:
                MakeBalloon("WaterBalloon5");
                break;
            case 6:
                MakeBalloon("WaterBalloon6");
                break;
            case 7:
                MakeBalloon("WaterBalloon7");
                break;
        }
    }
    void UseItem()
    {
        UseShield();
        UseNiddle();
    }

    void UseShield()
    {
        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.RightControl) && gameManager.plA == true)
        { //왼쪽컨트롤키를 누르고 플레이어의 상태가 true인 경우에만 실행
          // 0번째 활성화된 아이템을 사용
            if (item.Activeitem[0].name.Contains("shield"))
            {
                Debug.Log("쉴드 사용");
                Shieldeffect.SetActive(true);
                useShield = true;
                //인보크를 이용하여 쉴드를 3초뒤에 꺼지게 함
                Invoke("stopShield", 2f);
            }
            string itemName = item.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
            UnityEngine.Debug.Log("플레이어A가" + itemName + "아이템을 사용함");
            // 0번째 아이템을 사용하려면 아래와 같이 호출
            item.ActiveUseItem(item.Activeitem[0].name);
        }
    }
    void UseNiddle()
    {
        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.RightControl) && playerHealth == 0f)
        { //왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            if (item.Activeitem[0].name.Contains("niddle"))
            {
                Debug.Log("바늘 사용");
                useniddle = true;
                Debug.Log(useniddle);
            }
            string itemName = item.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
            UnityEngine.Debug.Log("플레이어A가" + itemName + "아이템을 사용함");
            item.ActiveUseItem(item.Activeitem[0].name);
        }

    }
    //쉴드 멈추는 코드
    private void stopShield()
    {
        useShield = false;
        Shieldeffect.SetActive(false);
    }
    //플레이어가 먹은 아이템 저장배열


    //플레이어 상태 스크립트(행동가능, 물풍선 같힌상태, 죽음)

    void CountDown()
    {
        playerAcountIndex--;
    }
    void MakeBalloon(string Power)
    {
        // 포지션
        Vector3 MoveVec = transform.position;
        MoveVec = new Vector3((float)Math.Round(MoveVec.x), (float)Math.Round(MoveVec.y), MoveVec.z); //소수점 버림

        WaterBalloon = objectManager.GetPool(Power);

        if (Input.GetKeyDown(KeyCode.RightShift) && playerAmakeBalloon && playerAcountIndex < bombPower)
        {
            Debug.Log("RightShift");
            if (!WaterBalloon[playerAballonIndex].activeInHierarchy)
            {
                WaterBalloon[playerAballonIndex].SetActive(true);
                WaterBalloon[playerAballonIndex].transform.position = MoveVec;

            }

            // playerAballonIndex 가 10 을 넘어가지 않게 0으로 초기화
            if (playerAballonIndex == 9)
            {
                playerAballonIndex = 0;
            }
            else
            {
                playerAballonIndex++;
            }

            playerAcountIndex++;
            Invoke("CountDown", 3f);
        }
    }

    //아이템 먹었을때 스탯 값 증감
    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater" || collision.gameObject.tag == "BalloonCollider")
        {

            if (useShield == true)
            {
                //DeathTime(); 실행안됨
            }
            else
            {
                DeathTime();
                
            }

            Debug.Log(collision.name);

        }


        string iname = collision.gameObject.name;

        UnityEngine.Debug.Log("플레이어가 오브젝트에 닿음");

        // 플레이어가 물풍선 위에 있을 시
        if (collision.gameObject.tag == "Balloon")
        {
           


        }

        if (collision.gameObject.CompareTag("powerItem"))
        {
            if (bombPower < bombPowerMax)
            {
                item.PowerAdd(iname);
            }
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
            UnityEngine.Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {
            if (playerSpeed < playerSpeedMax)
            {
                item.SpeedAdd(iname);
            }
            UnityEngine.Debug.Log("스피드 아이템에 닿았음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if (bombRange < bombRangeMax)
            {
                item.RangeAdd(iname);
            }
            UnityEngine.Debug.Log("사거리 증가 아이템에 닿음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item.SuperMan(iname);
            UnityEngine.Debug.Log("슈퍼맨!!");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

        // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            UnityEngine.Debug.Log("ActiveItem ADD");
            item.AddActiveItem(collision.gameObject, 0);
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        playerRenderer.enabled = true;

        // 물풍선 밖으로 나가면 트리거 비활성화
        if(other.gameObject.layer  == 8)
        {
            Collider2D col = other.gameObject.GetComponent<Collider2D>();
            col.isTrigger = false;
        }

    }

    void DeathTime()
    {
        Debug.Log("플레이어가 데미지를 입음");
        anim.SetBool("isDamage", true);
        anim.SetBool("isDying", false);
        playerSpeed = 0.8f;
        string playername = "A";
        gameManager.Death(playername);
        Invoke("DeadTime", 4f);
    }

    void DeadTime()
    {
        anim.SetBool("isDead", true);
        anim.SetBool("isDamage", false);
        playerSpeed = 0f;

    }


    void colliderRay()
    {
        // Ray
        Debug.DrawRay(transform.position - new Vector3(0.35f, 0.55f, 0), Vector3.right * 0.7f, new Color(1, 1, 1));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position - new Vector3(0.35f, 0.55f, 0), Vector3.right * 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        if (downRayHit.collider != null)
        {
            GameObject downObj = downRayHit.collider.gameObject;
            SpriteRenderer orderInLayer = downObj.GetComponent<SpriteRenderer>();
            int objOrder = orderInLayer.sortingOrder;
            playerRenderer.sortingOrder = orderInLayer.sortingOrder - 1;
            // orderInLayer.sortingOrder += playerRenderer.sortingOrder;
            objOrder = orderInLayer.sortingOrder;
        }
        else if (downRayHit.collider == null)
        {
            playerRenderer.sortingOrder = 13;
        }
    }
}
