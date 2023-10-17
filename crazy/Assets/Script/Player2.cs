
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player2 : MonoBehaviour
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

    int playerBballonIndex = 10; // 물풍선 오브젝트 풀 사용할 때 필요한 playerㅠballonIndex 변수
    public int playerBcountIndex = 0; // 물풍선을 생성할 때, 플레이어가 생성한 물풍선의 개수를 체크할 때 필요한 변수
    public bool playerBmakeBalloon; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수
    public ObjectManager objectManager;

    public Item2 item2;

    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한

    RaycastHit2D rayHit; // 물풍선 충돌 판정에 필요한 Ray
    Vector2 moveVec; // 플레이어가 움직이는 방향
    Vector2 dirVec; // Ray 방향
    Vector2 PlusVec; // 물풍선 충돌 판정Ray 시작지점
    Vector2 MakeVec; // 물풍선 생성 판정 Ray 시작지점
    GameObject scanObject; // Ray 로 스캔한 오브젝트

    Rigidbody2D rigid;
    CircleCollider2D collider;


    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;

        playerBmakeBalloon = true;

        PlusVec = new Vector2(0, 1.3f);
        dirVec = Vector2.up;
        rayHit = Physics2D.Raycast(rigid.position + PlusVec, dirVec, 0.4f, LayerMask.GetMask("Balloon B"));
    }

    void Update() {

        Move();
        Skill();
        Ray();
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
        hAxis = Input.GetAxisRaw("2PH");
        vAxis = Input.GetAxisRaw("2PV");

        bool hDown = Input.GetButtonDown("2PH");
        bool vDown = Input.GetButtonDown("2PV");
        bool hUp = Input.GetButtonUp("2PH");
        bool vUp = Input.GetButtonUp("2PV");


        if (vDown)
        {
            isHorizonMove = false;
        }
        else if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vUp || hUp)
        {
            isHorizonMove = hAxis != 0;
        }

        // Ray 방향
        if (vDown && vAxis == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && vAxis == -1)
        {
            dirVec = Vector3.down;
        }
        else if (hDown && hAxis == -1)
        {
            dirVec = Vector3.left;
        }
        else if (hDown && hAxis == 1)
        {
            dirVec = Vector3.right;
        }
    }

    void Ray()
    {
        // 시작점 및 방향 설정
        if (moveVec.x == 0 && moveVec.y == 1)
        {
            PlusVec = new Vector2(0, 1.45f);
            MakeVec = new Vector2(0, -0.6f);
            dirVec = Vector2.up;
        }
        else if (moveVec.x == 0 && moveVec.y == -1)
        {
            PlusVec = new Vector2(0, -1.45f);
            MakeVec = new Vector2(0, 0.6f);
            dirVec = Vector2.down;
        }
        else if (moveVec.x == -1 && moveVec.y == 0)
        {
            PlusVec = new Vector2(-1.45f, 0);
            MakeVec = new Vector2(0.6f, 0);
            dirVec = Vector2.left;
        }
        else if (moveVec.x == 1 && moveVec.y == 0)
        {
            PlusVec = new Vector2(1.45f, 0);
            MakeVec = new Vector2(-0.6f, 0);
            dirVec = Vector2.right;
        }

        // Ray (물풍선 충돌 판정)
        Debug.DrawRay(rigid.position + PlusVec, dirVec * 0.4f, new Color(0, 0, 1));
        rayHit = Physics2D.Raycast(rigid.position + PlusVec, dirVec, 0.4f, LayerMask.GetMask("Balloon B"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            scanObject.gameObject.layer = 11;
        }
        else
        {
            scanObject = null;

        }

        // 물풍선을 겹치게 생성 못하게 만들 때 필요한 Ray
        Debug.DrawRay(rigid.position + MakeVec, dirVec * 1.2f, new Color(1, 0, 0));
        RaycastHit2D rayHitForMake = Physics2D.Raycast(rigid.position + MakeVec, dirVec, 1.2f, LayerMask.GetMask("Balloon A") | LayerMask.GetMask("Balloon B") | LayerMask.GetMask("Balloon Hard A") | LayerMask.GetMask("Balloon Hard B"));

        if (rayHitForMake.collider != null)
        {
            playerBmakeBalloon = false;
        }
        else
        {
            playerBmakeBalloon = true;
        }
    }
    void Skill()
    {  
        if(Input.GetKeyDown(KeyCode.LeftShift))
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

        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerHealth == 0f)//왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            // 0번째 활성화된 아이템을 사용
            if (item2.Activeitem.Length > 0 && item2.Activeitem[0] != null)
            {
                string itemName = item2.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
                UnityEngine.Debug.Log("플레이어A가" + itemName + "아이템을 사용함");
                // 0번째 아이템을 사용하려면 아래와 같이 호출
                item2.ActiveUseItem(item2.Activeitem[0].name);

                // 1번째 아이템을 0번째로 끌어올림
                if (item2.Activeitem.Length > 1 && item2.Activeitem[1] != null)
                {
                    item2.Activeitem[0] = item2.Activeitem[1];
                    item2.Activeitem[1] = null;
                }
            }

            else
            {
                UnityEngine.Debug.Log("활성화된 아이템 없음");
            }

    }
    //플레이어가 먹은 아이템 저장배열

    //플레이어 상태 스크립트(행동가능, 물풍선 같힌상태, 죽음)

    void CountDown()
    {
        playerBcountIndex--;
    }
    void MakeBalloon(string Power)
    {
        // 포지션
        Vector3 MoveVec = transform.position;
        MoveVec = new Vector3((float)Math.Round(MoveVec.x), (float)Math.Round(MoveVec.y), MoveVec.z); //소수점 버림


        GameObject[] WaterBalloon;
        WaterBalloon = objectManager.GetPool(Power);

        if (playerBmakeBalloon && playerBcountIndex < bombPower)
        {
            Debug.Log("LeftShift");
            if (!WaterBalloon[playerBballonIndex].activeInHierarchy)
            {
                WaterBalloon[playerBballonIndex].SetActive(true);
                WaterBalloon[playerBballonIndex].transform.position = MoveVec;

            }

            // playerAballonIndex 가 20 을 넘어가지 않게 10으로 초기화
            if (playerBballonIndex == 19)
            {
                playerBballonIndex = 10;
            }
            else
            {
                playerBballonIndex++;
            }

            playerBcountIndex++;
            Invoke("CountDown", 3f);
        }
    }

    //아이템 먹었을때 스탯 값 증감
    void OnTriggerEnter2D(Collider2D collision)
    {
        string iname = collision.gameObject.name;

        UnityEngine.Debug.Log("플레이어가 오브젝트에 닿음");

        // 플레이어가 물풍선 안에 있을 시, 물풍선 생성 불가능하게 변경
        if (collision.gameObject.tag == "Balloon")
        {
            /*if () { niddle 사용하지 않았을때의 조건문
               Invoke("DeatTime", 5);
           }*/
            Invoke("DeatTime", 5);
        }

        if (collision.gameObject.CompareTag("powerItem"))
        {
            if (bombPower < bombPowerMax)
            {
                item2.PowerAdd(iname);
            }
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
           UnityEngine.Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {

            if (playerSpeed < playerSpeedMax)
            {
                item2.SpeedAdd(iname);
            }
            UnityEngine.Debug.Log("스피드 아이템에 닿았음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if (bombRange < bombRangeMax)
            {
                item2.RangeAdd(iname);
            }
            UnityEngine.Debug.Log("사거리 증가 아이템에 닿음");
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item2.SuperMan(iname);
            UnityEngine.Debug.Log("슈퍼맨!!");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

      // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            UnityEngine.Debug.Log("ActiveItem ADD");
            item2.AddActiveItem(collision.gameObject, 0);
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

      
        
    }

    void DeatTime()
    {
        string playername = "B";
        gameManager.Death2(playername);
    }

}
